net start WinRM
Set-Item WSMan:\localhost\Client\TrustedHosts -Value raspberrypi
Set-Item WSMan:\localhost\Client\TrustedHosts -Value 192.168.1.96
Enter-PSSession -ComputerName 192.168.1.96 -Credential Administrator
Exit-PSSession

cd 'C:\Users\Sarah\Documents\Visual Studio 2015\Projects\NX595Interface'
dnu publish --out C:\Publish\NX595Interface\ --no-source --runtime dnx-coreclr-win-arm.1.0.0-rc1-final

netsh advfirewall firewall add rule name="DNX Web Server port" dir=in action=allow protocol=TCP localport=595

New-PSDrive R -PSProvider FileSystem -Root "\\192.168.1.113\C$" -Credential Administrator
ls R:\PROGRAMS\NX595Interface\approot

Copy-Item C:\Publish\NX595Interface\approot -Destination R:\PROGRAMS\NX595Interface -Force -Recurse

New-NetIPAddress –InterfaceAlias "Ethernet" –IPv4Address "192.168.1.96" –PrefixLength 24 -DefaultGateway 192.168.1.1

schtasks /create /tn "NX595Interface" /tr C:\Programs\NX595Interface\approot\web.cmd /sc onstart /ru SYSTEM