namespace CSharp.Saude.FitbitTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Program
    {
        public static bool hasRefreshed = false;
        public static Info ENVIRONMENT_VARIABLES = Info.ENVIRONMENT_VARIABLES;

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

        public static void Main(string[] args)
        {
            try
            {
#if DEBUG
                //couldn't find a better way to set env while debugging :/
                Environment.SetEnvironmentVariable("MY_EMAIL", "vinicius.lourenco@gmail.com");
                Environment.SetEnvironmentVariable("MY_EMAIL_PASSWORD", "Gmail009");
                Environment.SetEnvironmentVariable("DATA_URI_SERVICE", "http://192.168.15.35:8002/odata/v4");
                Environment.SetEnvironmentVariable("CLIENT_ID", "22D9PB");
                Environment.SetEnvironmentVariable("CLIENT_SECRET", "0ace5cca58f4fde90d76c6541eb768c4");
                Environment.SetEnvironmentVariable("CODE", "6ff8c6a25a3fe65e11d770e90313ace287930d80");
                Environment.SetEnvironmentVariable("ACESS_TOKEN", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyMkQ5UEIiLCJzdWIiOiI2SlJTQ0YiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyc29jIHJzZXQgcmFjdCBybG9jIHJ3ZWkgcmhyIHJudXQgcnBybyByc2xlIiwiZXhwIjoxNTY4NTQxNzAyLCJpYXQiOjE1Njg1MTI5MDJ9.mwDgcRd_srmFzh-6j4i-AuHtz07F2oJK29fmrHjvjus");
                Environment.SetEnvironmentVariable("REFRESH_TOKEN", "0ec7949c91d57f8092b39c0058de517d8de77e73966f1a0202c2e0327e11434d");
                Environment.SetEnvironmentVariable("EXPERIS_IN", 31536000.ToString());
                Environment.SetEnvironmentVariable("REQUEST_LIMIT_MAX", 140.ToString());
                Environment.SetEnvironmentVariable("REQUEST_LIMIT_COUNT", 0.ToString());
                Environment.SetEnvironmentVariable("REQUEST_LIMIT_START", null);
                Environment.SetEnvironmentVariable("START_DATE", DateTime.Now.AddDays(-8).ToString());
                Environment.SetEnvironmentVariable("END_DATE", DateTime.Now.ToString());
#endif

                Console.WriteLine("ENVIRONMENT_VARIABLES:");
                var environmentJson = JObject.Parse(JsonConvert.SerializeObject(ENVIRONMENT_VARIABLES));

                foreach (var pair in environmentJson)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }

                Run(ENVIRONMENT_VARIABLES.DataUriService,
                    ENVIRONMENT_VARIABLES.ClientId,
                    ENVIRONMENT_VARIABLES.ClientSecret,
                    ENVIRONMENT_VARIABLES.MyEmail,
                    ENVIRONMENT_VARIABLES.MyEmailPassword,
                    ENVIRONMENT_VARIABLES.EndDate.Value
                    );

                ENVIRONMENT_VARIABLES.StartDate = null;
                ENVIRONMENT_VARIABLES.EndDate = null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ENVIRONMENT_VARIABLES.SaveJson();
                Console.WriteLine("Finished at {0}", DateTime.Now.ToString());
                var environmentJson = JObject.Parse(JsonConvert.SerializeObject(ENVIRONMENT_VARIABLES));

                foreach (var pair in environmentJson)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            }
        }

        private static void Run(string dataUriService, string clientId, string clientSecret, string myEmail, string myEmailPassword, DateTime endDate)
        {
            var startDate = ENVIRONMENT_VARIABLES.StartDate.Value;
            try
            {
#if DEBUG
                RequestToken.Test(clientId, clientSecret, ENVIRONMENT_VARIABLES.ExperisIn);
#endif
                RequestData.Run(ENVIRONMENT_VARIABLES.AccessToken, startDate, (requestDatas, lastExecuteDate) => {
                    Console.WriteLine("DataBulkInsert until {0}", lastExecuteDate);
                    if(requestDatas.Any())
                    {
                        DataBulkInsert(dataUriService, requestDatas);
                        hasRefreshed = false;
                    }
                    ENVIRONMENT_VARIABLES.StartDate = lastExecuteDate;
                }, endDate, 5);
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException && !hasRefreshed)
                {
                    hasRefreshed = true;

                    var requestToken = new RequestToken(clientId, clientSecret);
                    var refreshResponse = requestToken.Refresh(ENVIRONMENT_VARIABLES.RefreshToken);
                    ENVIRONMENT_VARIABLES.AccessToken = refreshResponse.access_token;
                    ENVIRONMENT_VARIABLES.RefreshToken = refreshResponse.refresh_token;

                    Run(dataUriService, clientId, clientSecret, myEmail, myEmailPassword, endDate);
                }
                else
                {
                    Console.WriteLine(ex);
                    Email.Send(
                        myEmail,
                        myEmailPassword,
                        JsonConvert.SerializeObject(ex));
                }
            }
        }

        public static void LoadAllSleep(Info info)
        {
            var mappedDatas = new List<global::Data.Models.Data>();
            var sleepData = RequestData.Sleep(info.AccessToken);
            mappedDatas.AddRange(sleepData.CastToDataDecimal(RequestData.SonoIds));

            DataBulkInsert(info.DataUriService, mappedDatas);
        }

        public static void LoadAllHeartRate(Info info, DateTime? startDate = null)
        {
            startDate = startDate ?? new DateTime(2016, 1, 1);
            var mappedDatas = new List<global::Data.Models.Data>();

            startDate.Value.ForEach((date) =>
            {
                var cardioGroupId = new Guid("C0EFE267-E8ED-4B79-A125-DB15ABC0780D");
                var heartRateData = RequestData.HeartRate(info.AccessToken, date);

                if (heartRateData.ActivitiesHeartIntradays != null)
                {
                    mappedDatas.AddRange(heartRateData.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date));
                }

                System.Diagnostics.Debug.WriteLine(date);

                if (ENVIRONMENT_VARIABLES.RequestLimitCount >= ENVIRONMENT_VARIABLES.RequestLimitMax)
                {
                    DataBulkInsert(info.DataUriService, mappedDatas);
                    mappedDatas = new List<global::Data.Models.Data>();
                }
            });
        }
    }
}
