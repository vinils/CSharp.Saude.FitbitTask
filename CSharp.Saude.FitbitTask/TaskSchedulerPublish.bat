#run build in release template

mkdir c:\Tasks\

cp .\bin\Release\*.* c:\Tasks\
cp .\TaskScheduler.xml c:\Tasks\

<<<<<<< HEAD
echo .\CSharp.Saude.FitbitTask.exe > c:\Tasks\run.bat
echo set DOCKER_HOST=tcp://192.168.15.35:2375 >> c:\Tasks\run.bat
echo docker exec 5648909f78a8 powershell -command "DTExec.exe /f .\import.dtsx" >> c:\Tasks\run.bat
echo docker exec 5648909f78a8 powershell -command 'sqlcmd -Q "EXEC [DataDW].[dbo].[SP_Reload]"' >> c:\Tasks\run.bat
=======
echo 'c:\Tasks\CSharp.Saude.FitbitTask.exe' > c:\Tasks\run.ps1
echo '$Env:DOCKER_HOST = "tcp://192.168.15.35:2375"' >> c:\Tasks\run.ps1
echo 'docker exec df3bb739f433 powershell -command "DTExec.exe /f .\import.dtsx"' >> c:\Tasks\run.ps1
echo "docker exec df3bb739f433 powershell -command 'sqlcmd -Q ""EXEC [DataDW].[dbo].[SP_Reload]""'" >> c:\Tasks\run.ps1
>>>>>>> parent of e058c0d... improving taskschdule

echo "(schtasks /query /TN ""Fitbit Task"") -or (schtasks /create /xml "".\TaskScheduler.xml"" /tn ""Fitbit Task"" /ru w19temp1\administrator /rp *)" >> c:\Tasks\run.ps1
