param(
	[Switch]$WriteHello
)

if($WriteHello)
{
	Write-Output "Hello World!"
	pwd
}

$PSScriptRoot