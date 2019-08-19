#run build in release

mkdir c:\Tasks\
cp .\bin\Release\*.* c:\Tasks\
echo '.\CSharp.Saude.FitbitTask.exe' > c:\Tasks\run.ps1
echo '$Env:DOCKER_HOST = "tcp://192.168.15.35:2375"' >> c:\Tasks\run.ps1
echo 'docker exec df3bb739f433 powershell -command "DTExec.exe /f .\import.dtsx"' >> c:\Tasks\run.ps1
echo "docker exec df3bb739f433 powershell -command 'sqlcmd -Q ""EXEC [DataDW].[dbo].[SP_Reload]""'" >> c:\Tasks\run.ps1
