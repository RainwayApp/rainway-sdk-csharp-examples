$serviceName = "BasicService"
$service = Get-Service -Name $serviceName -ErrorAction Stop
if ($service.Status -eq "Running") {
    $service.Stop()
}
$service.Start($args)