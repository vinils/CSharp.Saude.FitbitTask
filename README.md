# CSharp.Saude.FitbitTask
Load fitbit data task  

git clone --recurse-submodules https://github.com/vinils/CSharp.Saude.FitbitTask.git CSharp.Saude.FitbitTask_ch  
cd CSharp.Saude.FitbitTask  
git submodule update --init --recursive  
git submodule update --remote --recursive  

docker build . -t vinils/csharp-saude-fitbittask  

docker run -it -d 
-e MY_EMAIL="myemail@email.com" 
-e MY_EMAIL_PASSWORD="mypassword" 
-e DATA_URI_SERVICE="http://192.168.15.35:8002/odata/v4" 
-e CLIENT_ID="33EDS6" 
-e CLIENT_SECRET="897a98sd7f9a8s7df98a7s9df87as9df87" 
-e CODE="9789a7sd89f7a9sd87fa9s8d7f9a8s7df9a8ds7" 
-e ACESS_TOKEN="eyJADSFasdFasdFASDFasdfOiIyMkQ5UEIiLCJzdWIiOiI2SlJTQ0YiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyc29jIHJhY3QgcnNldCBybG9jIHJASDFasdfasdfasdfasdfasdFasdfaSdfaSdfasdfEepYXQiOjE1NjY3NDY4OTJ9.C6DwPT-2y2Nt-cCaWy9wx75jZPBg28AxAsbCJdIJf0U" 
-e REFRESH_TOKEN="7a698sd76f5a6s4d58a7sd5g98adf68sg" 
vinils/csharp-saude-fitbittask cmd
<BR>

docker exec -it 7c5ed5aa474d "C:\\app\\CSharp.Saude.FitbitTask\\bin\\Release\\CSharp.Saude.FitbitTask.exe"
<BR>

Optional Parameters: 
-e EXPERIS_IN=31536000 
-e REQUEST_LIMIT_COUNT=0 
-e REQUEST_LIMIT_START="2019-09-14T11:45:45.6103765-03:00" 
-e REQUEST_LIMIT_MAX=149 
-e START_DATE="2019-09-12" 
-e END_DATE="2019-09-14" 
<BR>

