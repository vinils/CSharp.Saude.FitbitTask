::run build in release template

mkdir c:\Tasks\

copy .\bin\Release\*.* c:\Tasks\
copy .\TaskScheduler.xml c:\Tasks\

echo .\CSharp.Saude.FitbitTask.exe > c:\Tasks\run.bat
echo set DOCKER_HOST=tcp://192.168.15.35:2375 >> c:\Tasks\run.bat
echo docker exec 5648909f78a8 powershell -command "DTExec.exe /f .\import.dtsx" >> c:\Tasks\run.bat
echo docker exec 5648909f78a8 powershell -command 'sqlcmd -Q "EXEC [DataDW].[dbo].[SP_Reload]"' >> c:\Tasks\run.bat

echo schtasks /query /TN "Fitbit Task" ^>NUL 2^>^&1 ^|^| schtasks /create /xml ".\TaskScheduler.xml" /tn "Fitbit Task" /ru w19temp1\administrator /rp * >> c:\Tasks\run.bat