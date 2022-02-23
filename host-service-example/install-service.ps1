$path = $MyInvocation.MyCommand.Path
if (!$path) { $path = $psISE.CurrentFile.Fullpath }
if ($path) { $path = Split-Path $path -Parent }
Set-Location $path
$output = "${path}\build"
if (!(Test-Path $output -PathType Container)) {
    New-Item -ItemType Directory -Force -Path $output
}
# clear previous releases
Write-Host $output
Remove-Item "${output}\*" -Recurse -Force

& dotnet publish "service-example.csproj" -c Release -o $output -p:PublishSingleFile=false --self-contained true -p:PublishReadyToRun=true -r win10-x64

$executable = "${output}\service-example.exe"
$arguments = "/helloworld"
if (!(Test-Path $executable -PathType Leaf)) {
    Write-Error "$executable does not exist. did the build fail?" -ErrorAction Stop
}

$serviceName = "BasicService"

if ((Get-Service $serviceName -ErrorAction SilentlyContinue)) {
    Write-Host "Removing service: ${serviceName} (${executable})"
    sc.exe delete $serviceName
}

$params = @{
    Name           = $serviceName
    # arguments here only take effect when the service is set to auto start
    BinaryPathName = "`"${executable}`" ${arguments}"
    DisplayName    = "Basic Service"
    StartupType    = "Manual"
    Description    = "A basic example service for projects using the Rainway SDK"
}

New-Service @params -ErrorAction Stop

$data = (whoami /all)
foreach ($line in $data) {
    if ($line -match "SID") {
        #search for the first occurence of "SID"
        [int]$index = $data.indexof($line) + 2 #skip title + delimiter
        break
    }
}

$currentUser = $($data[$index].split(' ')[0]).Trim()
if ([string]::IsNullOrEmpty($currentUser)) {
    Write-Error "'whoami' failed to return a user" -ErrorAction Stop
}
$currentSID = $($data[$index].split(' ')[1]).Trim()
if ([string]::IsNullOrEmpty($currentSID)) {
    Write-Error "'whoami' failed to return a SID" -ErrorAction Stop
}

Write-Output "Determined current user to be SID: $currentSID"

$securityDescriptor = ([String] (sc.exe sdshow "${serviceName}")).Trim()

if ([string]::IsNullOrEmpty($currentSID)) {
    Write-Error "'sc' failed to return the SDDL for service '${serviceName}'" -ErrorAction Stop
}

$sIndex = $securityDescriptor.IndexOf("S:")

if ([int]$sIndex -le 0) {
    Write-Error "Unable to locate descriptor index" -ErrorAction Stop
}

# see https://docs.microsoft.com/en-us/windows/win32/secauthz/security-descriptor-string-format for more info
$sddlMod = "$($securityDescriptor.Substring(0,$sIndex))(A;;LCRPWP;;;${currentSID})$($securityDescriptor.Substring($sIndex))"

sc.exe sdset "$serviceName" "$sddlMod"
Write-Host "User '${currentUser}' can now start service '${serviceName}' without elevation"
