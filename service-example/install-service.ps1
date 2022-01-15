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
    Remove-Service -Name $serviceName -ErrorAction Stop
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

$securityDescriptor = ([String] (sc sdshow "${serviceName}")).Trim()

if ([string]::IsNullOrEmpty($currentSID)) {
    Write-Error "'sc' failed to return the SDDL for service '${serviceName}'" -ErrorAction Stop
}

$sIndex = $securityDescriptor.IndexOf("S:")

if ([int]$sIndex -le 0) {
    Write-Error "Unable to locate descriptor index" -ErrorAction Stop
}

$sddlMod = "$($securityDescriptor.Substring(0,$sIndex))(A;;LCRPWP;;;${currentSID})$($securityDescriptor.Substring($sIndex))"

Set-Service -Name $serviceName -SecurityDescriptorSddl "$sddlMod" -ErrorAction Stop
Write-Host "User '${currentUser}' can now start service '${serviceName}' without elevation"
