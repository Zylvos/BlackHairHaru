# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/BlackHairHaru/*" -Force -Recurse
dotnet publish "./BlackHairHaru.csproj" -c Release -o "$env:RELOADEDIIMODS/BlackHairHaru" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location