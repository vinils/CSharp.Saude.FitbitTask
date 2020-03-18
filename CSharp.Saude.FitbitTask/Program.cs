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

        public static void DataBulkInsert(string dataUrlService, ICollection<global::Data.Models.Data> datas)
        {
            try
            {
                var dataUriStr = dataUrlService;
                var dataUri = new Uri(dataUriStr);
                var container = new Default.Container(dataUri);
                //container.Timeout = int.MaxValue;

                var bulkInsert = Default.ExtensionMethods.BulkInsert(container.Datas, datas);

                var task = bulkInsert.ExecuteAsync();
                while (!task.IsCompleted)
                {
                    //  Waiting for command to complete...
                    System.Threading.Thread.Sleep(2000);
                }

                //var result = bulkInsert.EndExecute(task);

                System.Diagnostics.Debug.WriteLine("run called");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Main(string[] args)
        {
            try
            {
#if DEBUG

                //couldn't find a better way to set env while debugging :/
                Environment.SetEnvironmentVariable("MY_EMAIL", "myemail@email.com");
                Environment.SetEnvironmentVariable("MY_EMAIL_PASSWORD", "mypassword");
                Environment.SetEnvironmentVariable("DATA_URI_SERVICE", "http://192.168.15.35:8002/odata/v4");
                Environment.SetEnvironmentVariable("CLIENT_ID", "33EDS6");
                Environment.SetEnvironmentVariable("CLIENT_SECRET", "897a98sd7f9a8s7df98a7s9df87as9df87");
                Environment.SetEnvironmentVariable("CODE", "9789a7sd89f7a9sd87fa9s8d7f9a8s7df9a8ds7");
                Environment.SetEnvironmentVariable("ACESS_TOKEN", "eyJADSFasdFasdFASDFasdfOiIyMkQ5UEIiLCJzdWIiOiI2SlJTQ0YiLCJpc3MiOiJGaXRiaXQiLCJ0eXAiOiJhY2Nlc3NfdG9rZW4iLCJzY29wZXMiOiJyc29jIHJhY3QgcnNldCBybG9jIHJASDFasdfasdfasdfasdfasdFasdfaSdfaSdfasdfEepYXQiOjE1NjY3NDY4OTJ9.C6DwPT-2y2Nt-cCaWy9wx75jZPBg28AxAsbCJdIJf0U");
                Environment.SetEnvironmentVariable("REFRESH_TOKEN", "7a698sd76f5a6s4d58a7sd5g98adf68sg");
                Environment.SetEnvironmentVariable("EXPERIS_IN", 31536000.ToString());
                Environment.SetEnvironmentVariable("REQUEST_LIMIT_MAX", 150.ToString());
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception at program main");
                throw ex;
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
                System.Diagnostics.Debugger.Break();
                RequestToken.Test(clientId, clientSecret, ENVIRONMENT_VARIABLES.ExperisIn);
#endif
                RequestData.Run(ENVIRONMENT_VARIABLES.AccessToken, startDate, (requestDatas, lastExecuteDate) => {
                    if(requestDatas.Any())
                    {
                        Console.WriteLine("DataBulkInsert of {0} registers until {1}", requestDatas.Count(), lastExecuteDate);
                        DataBulkInsert(dataUriService, requestDatas);
                    }
                    hasRefreshed = false;
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
                    ENVIRONMENT_VARIABLES.StartDate = startDate;
                    Console.WriteLine(ex);
                    Email.Send(
                        myEmail,
                        myEmailPassword,
                        JsonConvert.SerializeObject(ex));
                    throw ex;
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
