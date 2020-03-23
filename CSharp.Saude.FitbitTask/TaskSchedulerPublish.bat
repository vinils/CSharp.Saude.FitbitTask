::run build in release template

echo schtasks /query /TN "Fitbit Task" ^>NUL 2^>^&1 ^|^| schtasks /create /xml ".\TaskScheduler.xml" /tn "Fitbit Task" /ru %%USERDOMAIN%%\%%USERNAME%% /rp %%1 >> c:\Tasks\run.bat
