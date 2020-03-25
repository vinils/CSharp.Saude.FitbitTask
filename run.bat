::SET DOCKER_TLS_VERIFY=1
::SET DOCKER_HOST=tcp://192.168.15.147:2376
::SET DOCKER_CERT_PATH=C:\Users\MyUser\Desktop\dckMyHealth1
::SET DOCKER_MACHINE_NAME=dckmyhealth1
::SET COMPOSE_CONVERT_WINDOWS_PATHS=true

docker-machine ssh dckmyhealth1 docker run -it -d --name fitbit-task -e MY_EMAIL="myemail@email.com" -e MY_EMAIL_PASSWORD="mypassword" -e DATA_URI_SERVICE="http://192.168.15.147:8002/odata" -e CLIENT_ID="33EDS6" -e CLIENT_SECRET="897a98sd7f9a8s7df98a7s9df87as9df87" -e CODE="9789a7sd89f7a9sd87fa9s8d7f9a8s7df9a8ds7" -e ACESS_TOKEN="eyJADSFasdFasdFASDFasdfOiIyMkQ5UEIiLCJzdWIiOiI2SlJTQ0YiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyc29jIHJhY3QgcnNldCBybG9jIHJASDFasdfasdfasdfasdfasdFasdfaSdfaSdfasdfEepYXQiOjE1NjY3NDY4OTJ9.C6DwPT-2y2Nt-cCaWy9wx75jZPBg28AxAsbCJdIJf0U" -e REFRESH_TOKEN="7a698sd76f5a6s4d58a7sd5g98adf68sg" -e EXPERIS_IN="31536000" -e REQUEST_LIMIT_MAX="150" -e REQUEST_LIMIT_COUNT="0" -e REQUEST_LIMIT_START="2020-03-22" -e START_DATE="2020-03-15" -e END_DATE="2020-03-22" vinils/csharp-saude-fitbittask
docker-machine ssh dckmyhealth1 docker start fitbit-task
docker-machine ssh dckmyhealth1 docker logs -f fitbit-task

docker-machine ssh dckmyhealth1 docker exec -it data_dbdw bash /DataAnalyze/SQL.DataDW/import-datacontext.sh

schtasks /query /TN "Fitbit Task" >NUL 2>&1 || schtasks /create /xml ".\TaskScheduler.xml" /tn "Fitbit Task" /ru %USERDOMAIN%\%USERNAME% /rp %1 
