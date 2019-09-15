namespace CSharp.Saude.FitbitTask
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    public class Info
    {
        private static readonly string filePath = Directory.GetCurrentDirectory() + "\\info.json";
        public static readonly Info ENVIRONMENT_VARIABLES = new Info();

        static Info()
        {
            var json = LoadJson();

            if (json != null)
            {
                ENVIRONMENT_VARIABLES.MyEmail = json.MyEmail;
                ENVIRONMENT_VARIABLES.MyEmailPassword = json.MyEmailPassword;
                ENVIRONMENT_VARIABLES.DataUriService = json.DataUriService;
                ENVIRONMENT_VARIABLES.ClientId = json.ClientId;
                ENVIRONMENT_VARIABLES.ClientSecret = json.ClientSecret;
                ENVIRONMENT_VARIABLES.Code = json.Code;
                ENVIRONMENT_VARIABLES.AccessToken = json.AccessToken;
                ENVIRONMENT_VARIABLES.RefreshToken = json.RefreshToken;
                ENVIRONMENT_VARIABLES.ExperisIn = json.ExperisIn;
                ENVIRONMENT_VARIABLES.RequestLimitMax = json.RequestLimitMax;
                ENVIRONMENT_VARIABLES.RequestLimitCount = json.RequestLimitCount;
                ENVIRONMENT_VARIABLES.RequestLimitStart = json.RequestLimitStart;
                if(!ENVIRONMENT_VARIABLES.StartDate.HasValue)
                    ENVIRONMENT_VARIABLES.StartDate = json.StartDate;
                if (!ENVIRONMENT_VARIABLES.EndDate.HasValue)
                    ENVIRONMENT_VARIABLES.EndDate = json.EndDate;
            }
            else
            {
                ENVIRONMENT_VARIABLES.SaveJson();
            }
        }

        public string MyEmail
        {
            get => Environment.GetEnvironmentVariable("MY_EMAIL") ?? "myemail@email.com";
            set => Environment.SetEnvironmentVariable("MY_EMAIL", value);
        }
        public string MyEmailPassword
        {
            get => Environment.GetEnvironmentVariable("MY_EMAIL_PASSWORD") ?? "mypassword";
            set => Environment.SetEnvironmentVariable("MY_EMAIL_PASSWORD", value);
        }
        public string DataUriService
        {
            get => Environment.GetEnvironmentVariable("DATA_URI_SERVICE") ?? "http://192.168.15.35:8002/odata/v4";
            set => Environment.SetEnvironmentVariable("DATA_URI_SERVICE", value);
        }
        public string ClientId
        {
            get => Environment.GetEnvironmentVariable("CLIENT_ID") ?? "33EDS6";
            set => Environment.SetEnvironmentVariable("CLIENT_ID", value);
        }
        public string ClientSecret
        {
            get => Environment.GetEnvironmentVariable("CLIENT_SECRET") ?? "897a98sd7f9a8s7df98a7s9df87as9df87";
            set => Environment.SetEnvironmentVariable("CLIENT_SECRET", value);
        }
        public string Code
        {
            get => Environment.GetEnvironmentVariable("CODE") ?? "9789a7sd89f7a9sd87fa9s8d7f9a8s7df9a8ds7";
            set => Environment.SetEnvironmentVariable("CODE", value);
        }
        public string AccessToken
        {
            get => Environment.GetEnvironmentVariable("ACESS_TOKEN") ?? "eyJADSFasdFasdFASDFasdfOiIyMkQ5UEIiLCJzdWIiOiI2SlJTQ0YiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyc29jIHJhY3QgcnNldCBybG9jIHJASDFasdfasdfasdfasdfasdFasdfaSdfaSdfasdfEepYXQiOjE1NjY3NDY4OTJ9.C6DwPT-2y2Nt-cCaWy9wx75jZPBg28AxAsbCJdIJf0U";
            set => Environment.SetEnvironmentVariable("ACESS_TOKEN", value);
        }
        public string RefreshToken
        {
            get => Environment.GetEnvironmentVariable("REFRESH_TOKEN") ?? "7a698sd76f5a6s4d58a7sd5g98adf68sg";
            set => Environment.SetEnvironmentVariable("REFRESH_TOKEN", value);
        }
        public int ExperisIn
        {
            get => Environment.GetEnvironmentVariable("EXPERIS_IN").ParseOrNull<int>() ?? 31536000;
            set => Environment.SetEnvironmentVariable("EXPERIS_IN", value.ToString());
        }
        public int RequestLimitMax
        {
            get => Environment.GetEnvironmentVariable("REQUEST_LIMIT_MAX").ParseOrNull<int>() ?? 149;
            set => Environment.SetEnvironmentVariable("REQUEST_LIMIT_MAX", value.ToString());
        }
        public int RequestLimitCount
        {
            get => Environment.GetEnvironmentVariable("REQUEST_LIMIT_COUNT").ParseOrNull<int>() ?? 0;
            set => Environment.SetEnvironmentVariable("REQUEST_LIMIT_COUNT", value.ToString());
        }
        public DateTime? RequestLimitStart
        {
            get => Environment.GetEnvironmentVariable("REQUEST_LIMIT_START").ParseOrNull<DateTime>() ?? DateTime.Now;
            set => Environment.SetEnvironmentVariable("REQUEST_LIMIT_START", value.ToString());
        }
        public DateTime? StartDate
        {
            get => Environment.GetEnvironmentVariable("START_DATE").ParseOrNull<DateTime>() ?? DateTime.Now.AddDays(-7);
            set => Environment.SetEnvironmentVariable("START_DATE", value.ToString());
        }
        public DateTime? EndDate
        {
            get => Environment.GetEnvironmentVariable("END_DATE").ParseOrNull<DateTime>() ?? DateTime.Now;
            set => Environment.SetEnvironmentVariable("END_DATE", value.ToString());
        }

        public static Info LoadJson()
            => !File.Exists(filePath) ? null : JsonConvert.DeserializeObject<Info>(File.ReadAllText(filePath));

        private Info()
        { }

        public void SaveJson()
        {
            var json = JsonConvert.SerializeObject(this);
            Console.WriteLine("Saving enviroments json at {0}", filePath);
            File.WriteAllText(filePath, json);
        }
    }

}
