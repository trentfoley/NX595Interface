# Configure to allow for remote PowerShell sessions
net start WinRM
Set-Item WSMan:\localhost\Client\TrustedHosts -Value raspberrypi
Set-Item WSMan:\localhost\Client\TrustedHosts -Value 192.168.1.96
Enter-PSSession -ComputerName 192.168.1.96 -Credential Administrator
Exit-PSSession

# Publish app to Raspberry Pi
cd 'C:\Users\Sarah\Documents\Visual Studio 2015\Projects\NX595Interface'
dnu publish --out \\raspberrypi\c$\PROGRAMS\NX595Interface --no-source --runtime dnx-coreclr-win-arm.1.0.0-rc2-20221

# On Raspberry Pi, poke hole in firewall
netsh advfirewall firewall add rule name="DNX Web Server port" dir=in action=allow protocol=TCP localport=595

# On Raspberry Pi, configure IP address to a static IP
New-NetIPAddress –InterfaceAlias "Ethernet" –IPv4Address "192.168.1.96" –PrefixLength 24 -DefaultGateway 192.168.1.1

# On Raspberry Pi, configure application to run at system startup
schtasks /create /tn "NX595Interface" /tr C:\Programs\NX595Interface\approot\web.cmd /sc onstart /ru SYSTEM