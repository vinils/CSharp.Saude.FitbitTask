namespace CSharp.Saude.FitbitTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static bool hasRefreshed = false;

        public static void DataBulkInsert(string dataUrlService, ICollection<Data.Models.Data> datas)
        {
            var dataUriStr = dataUrlService;
            var dataUri = new Uri(dataUriStr);
            var container = new Default.Container(dataUri);
            container.Timeout = int.MaxValue;

            //foreach (var p in container.Groups)
            //{
            //    Console.WriteLine("{0} {1} {2}", p.Id, p.Name, p.ParentId);
            //}

            var bulkInsert = Default.ExtensionMethods.BulkInsert(container.Datas, datas);
            bulkInsert.Execute();

            //var group = new Data.Models.Group()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Yo-yo",
            //};

            //container.AddToGroupsV4(group);
            //var dataServiceResponse = container.SaveChanges();
            //foreach (var operationResponse in dataServiceResponse)
            //{
            //    Console.WriteLine("Response: {0}", operationResponse.StatusCode);
            //}
            System.Diagnostics.Debug.WriteLine("run called");

        }

        public static void Run(string dataUriService, string acessToken)
        {
            var datas = RequestData.Test(acessToken);
            DataBulkInsert(dataUriService, datas);
        }

        public static void Main(string[] args)
        {
            //var myEmail = "myemail@email.com";
            var myEmail = Environment.GetEnvironmentVariable("MY_EMAIL");
            //var myEmailPassword = "mypassword";
            var myEmailPassword = Environment.GetEnvironmentVariable("MY_EMAIL_PASSWORD");
            //var DataUriService = "http://192.168.15.35:8002/odata/v4";
            var dataUriService = Environment.GetEnvironmentVariable("DATA_URI_SERVICE");
            //var ClientId = "33EDS6";
            var clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            //var ClientSecret = "897a98sd7f9a8s7df98a7s9df87as9df87";
            var clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
            //var Code = "9789a7sd89f7a9sd87fa9s8d7f9a8s7df9a8ds7";
            var code = Environment.GetEnvironmentVariable("CODE");
            //var AcessToken = "eyJADSFasdFasdFASDFasdfOiIyMkQ5UEIiLCJzdWIiOiI2SlJTQ0YiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyc29jIHJhY3QgcnNldCBybG9jIHJASDFasdfasdfasdfasdfasdFasdfaSdfaSdfasdfEepYXQiOjE1NjY3NDY4OTJ9.C6DwPT-2y2Nt-cCaWy9wx75jZPBg28AxAsbCJdIJf0U";
            var acessToken = Environment.GetEnvironmentVariable("ACESS_TOKEN");
            //var RefreshToken = "7a698sd76f5a6s4d58a7sd5g98adf68sg";
            var refreshToken = Environment.GetEnvironmentVariable("REFRESH_TOKEN");
            //var ExperisIn = 31536000;
            int? experisIn = null;
            if (int.TryParse(Environment.GetEnvironmentVariable("EXPERIS_IN"), out int casted))
                experisIn = casted;

            Info info = Info.Load();
            if (!string.IsNullOrEmpty(myEmail))
                info.MyEmail = myEmail;

            if (!string.IsNullOrEmpty(myEmailPassword))
                info.MyEmailPassword = myEmailPassword;

            if (!string.IsNullOrEmpty(dataUriService))
                info.DataUriService = dataUriService;

            if (!string.IsNullOrEmpty(clientId))
                info.ClientId = clientId;

            if (!string.IsNullOrEmpty(clientSecret))
                info.ClientSecret = clientSecret;

            if (!string.IsNullOrEmpty(code))
                info.Code = code;

            if (!string.IsNullOrEmpty(acessToken))
                info.AcessToken = acessToken;

            if (!string.IsNullOrEmpty(refreshToken))
                info.RefreshToken = refreshToken;

            if (experisIn.HasValue)
                info.ExperisIn = experisIn.Value;

            info.Update();

            try
            {
                //RequestToken.Test(info);
                Run(info.DataUriService, info.AcessToken);
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException && !hasRefreshed)
                {
                    hasRefreshed = true;

                    var requestToken = new RequestToken(info.ClientId, info.ClientSecret);
                    var refreshResponse = requestToken.Refresh(info.RefreshToken);
                    info.AcessToken = refreshResponse.access_token;
                    info.RefreshToken = refreshResponse.refresh_token;
                    info.Update();

                    Main(args);
                }
                else
                {
                    Email.Send(
                        info.MyEmail,
                        info.MyEmailPassword,
                        Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                }
            }

        }

        public static void LoadAllSleep(Info info)
        {
            var mappedDatas = new List<global::Data.Models.Data>();
            var sleepData = RequestData.Sleep(info.AcessToken);
            mappedDatas.AddRange(sleepData.CastToDataDecimal(RequestData.SonoIds));

            DataBulkInsert(info.DataUriService, mappedDatas);
            mappedDatas = new List<global::Data.Models.Data>();
        }

        public static void LoadAllHeartRate(Info info, DateTime? startDate = null)
        {
            startDate = startDate ?? new DateTime(2016, 1, 1);
            var mappedDatas = new List<global::Data.Models.Data>();
            while (startDate <= DateTime.Today)
            {
                for (var timewait = 1; timewait <= 140; timewait++)
                {
                    for (var timeinsert = 1; timeinsert <= 5; timeinsert++)
                    {
                        var cardioGroupId = new Guid("C0EFE267-E8ED-4B79-A125-DB15ABC0780D");
                        var heartRateData = RequestData.HeartRate(info.AcessToken, startDate.Value);

                        if (heartRateData.ActivitiesHeartIntradays != null)
                        {
                            mappedDatas.AddRange(heartRateData.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, startDate.Value));
                        }

                        System.Diagnostics.Debug.WriteLine(startDate);
                        startDate = startDate.Value.AddDays(1);
                    }

                    DataBulkInsert(info.DataUriService, mappedDatas);
                    mappedDatas = new List<global::Data.Models.Data>();
                }

                var timeW = new TimeSpan(1, 0, 0);
                System.Threading.Thread.Sleep(timeW);
            }
        }
    }
}
