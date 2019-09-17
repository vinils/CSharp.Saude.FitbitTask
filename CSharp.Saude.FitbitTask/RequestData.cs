namespace CSharp.Saude.FitbitTask
{
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class RequestData
    {
        public static Info ENVIRONMENT_VARIABLES = Info.ENVIRONMENT_VARIABLES;
        private static readonly string FITBIT_URI = "https://api.fitbit.com";
        //private static int limit_count = 0;
        public static int LIMIT_COUNT
        {
            get => ENVIRONMENT_VARIABLES.RequestLimitCount;
            private set
            {
                switch (value)
                {
                    case 0:
                        ENVIRONMENT_VARIABLES.RequestLimitStart = null;
                        ENVIRONMENT_VARIABLES.RequestLimitCount = value;
                        break;
                    case 1:
                    case var x when (x >= ENVIRONMENT_VARIABLES.RequestLimitMax + 1):
                        ENVIRONMENT_VARIABLES.RequestLimitStart = DateTime.Now;
                        ENVIRONMENT_VARIABLES.RequestLimitCount = 1;
                        break;
                    default:
                        ENVIRONMENT_VARIABLES.RequestLimitCount = value;
                        break;
                }

                Console.WriteLine("ENVIRONMENT_VARIABLES - RequestLimitCount:{0} RequestLimitStart:{1}",
                                  ENVIRONMENT_VARIABLES.RequestLimitCount,
                                  ENVIRONMENT_VARIABLES.RequestLimitStart);
            }
        }

        private static IRestResponse<T> ExecuteRequest<T>(Func<IRestResponse<T>> execute) where T : new()
        {
            IRestResponse<T> ret;
            if (LIMIT_COUNT >= ENVIRONMENT_VARIABLES.RequestLimitMax)
            {
                ENVIRONMENT_VARIABLES.SaveJson();
                var limitTime = ENVIRONMENT_VARIABLES.RequestLimitStart.Value.AddHours(1);
                //var timeToWait = new TimeSpan(1, 0, 0);
                var timeToWait = limitTime - DateTime.Now;
                var isPositive = timeToWait == timeToWait.Duration();

                if (isPositive)
                {
                    System.Threading.Thread.Sleep(timeToWait);
                }

                LIMIT_COUNT = 0;
            }

            ret = execute();
            LIMIT_COUNT += 1;

            return ret;
        }

        public static Sleeps.Sleep.SonoIdsStructure SonoIds = new Sleeps.Sleep.SonoIdsStructure()
        {
            Totals = new Sleeps.Sleep.SonoTotals()
            {
                TimeInBed = new OnlyId("08BC140E-7D67-4283-A9EA-02D5F468AA24"),
                Asleep = new OnlyId("F1725DF1-1D22-4CD7-94F9-8FDD26FA4965"),
                Efficiency = new OnlyId("6497347A-22BA-44D7-B500-9B3967293E18"),
                Duration = new OnlyId("6F8BB4C6-7420-4CA5-B029-B0F58D794C5D"),
                FallAsleep = new OnlyId("E0B0A866-AA36-4597-B1D9-C94F7825DDE3"),
                AfterWakeup = new OnlyId("D33348BA-D90A-4D38-A600-E1971131AFA7"),
                Awake = new OnlyId("0C209B5D-7F7A-46D8-99A0-FACE6B280E68"),
            },
            Summary = new Sleeps.Summary.SummaryIds()
            {
                Asleep = new OnlyId("AB0DEE1F-73E8-4160-9BE7-DD43560567D5"),
                AsleepCount = new OnlyId("523C5B9E-EFF7-4AD3-8DF6-FCC00FDB2D9E"),
                Awake = new OnlyId("88E463A0-46CF-4D8C-9BAD-0CC5A1C4ABD3"),
                AwakeCount = new OnlyId("0F97B7A9-7C66-439E-9A93-28BE0F48FD6A"),
                Deep = new OnlyId("FB3A393B-1AAF-4047-ACC1-7A1D6D3AF6CE"),
                DeepCount = new OnlyId("5AC43A3A-C66C-459C-BB16-6D66B9B9DC9D"),
                Light = new OnlyId("EE8BC452-A04A-4320-BBFC-BB66384E0253"),
                LightCount = new OnlyId("4ECE3413-517D-4F39-A764-3B07FA3EDA47"),
                Rem = new OnlyId("DB707970-DBE6-4752-8523-3CF918D34084"),
                RemCount = new OnlyId("46CA76B0-1724-4B86-B369-1B5D87446ECC"),
                Restless = new OnlyId("7B6136C6-03CD-47B6-A668-A6A49D48824E"),
                RestlessCount = new OnlyId("F95BE23B-406F-4A89-B838-317F4B154A42"),
                Wake = new OnlyId("B837B0A9-DB6E-4321-B817-277ABBCB4B51"),
                WakeCount = new OnlyId("1C430C3C-CC9F-4FD6-8A04-762CDCDCADC4"),
            }
        };

        public static HeartRates.Response HeartRate(string accessToken, DateTime date)
        {
            var client = new RestClient(FITBIT_URI + "/1/user/-/activities/heart/date/" 
                + date.ToString("yyyy-MM-dd") + "/1d/1sec/time/00:00/23:59.json");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + accessToken);
            var response = ExecuteRequest(() => client.Execute<HeartRates.Response>(request));

            switch(response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine("Error! Unauthorized fitbit HeartRate request");
                    throw new UnauthorizedAccessException();
                case (HttpStatusCode)429:
                    ENVIRONMENT_VARIABLES.RequestLimitCount = ENVIRONMENT_VARIABLES.RequestLimitMax;
                    Console.WriteLine("Error! Too many requests fitbit HeartRate");
                    return HeartRate(accessToken, date);
            }

            return response.Data;
        }
        public static Sleeps.Response Sleep(string accessToken)
        {
            var beforeDate = DateTime.Today.AddDays(1);
            var url = FITBIT_URI + "/1.2/user/-/sleep/list.json?limit=100&sort=desc&beforeDate="
                + beforeDate.ToString("yyyy-MM-dd") + "&offset=0";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + accessToken);

            var response = ExecuteRequest(() => client.Execute<Sleeps.Response>(request));
            Console.WriteLine("sleep.response.status: {0}", response.StatusCode);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine("Error! Unauthorized fitbit Sleep request");
                    throw new UnauthorizedAccessException();
                case (HttpStatusCode)429:
                    ENVIRONMENT_VARIABLES.RequestLimitCount = ENVIRONMENT_VARIABLES.RequestLimitMax;
                    Console.WriteLine("Error! Too many requests fitbit Sleep");
                    Sleep(accessToken);
                    break;
            }

            Console.WriteLine("sleep.response.data: {0}", response.Data);
            return response.Data;
        }

        public static void ForEachHearRate(string accessToken, DateTime startDate, Action<List<Data.Models.DataDecimal>, DateTime> callBack, DateTime? endDate, int incrementDays = 1)
        {
            var cardioGroupId = new Guid("C0EFE267-E8ED-4B79-A125-DB15ABC0780D");
            var endDate2 = endDate ?? DateTime.Now;

            startDate.ForEach((date) => {
                Console.WriteLine("HeartRate: {0} - Last Run: {1}", date.ToString(), DateTime.Now);
                Console.WriteLine("Days left: {0} - Hours left: {1}",
                    Convert.ToInt32((endDate2 - date).TotalDays),
                    Convert.ToInt32((endDate2 - date).TotalDays / ENVIRONMENT_VARIABLES.RequestLimitMax));
                Console.WriteLine("Expected end: {0}",
                                  ENVIRONMENT_VARIABLES.RequestLimitStart.Value.AddHours(Convert.ToInt32((endDate2 - date).TotalDays / ENVIRONMENT_VARIABLES.RequestLimitMax)));

                var heartRateData = HeartRate(accessToken, date);
                callBack(heartRateData.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date), date);
            }, endDate2, incrementDays);
        }
        public static void Run(string accessToken, DateTime startDate, Action<List<Data.Models.Data>, DateTime> callBack, DateTime? endDate, int? callBackBreaks = null)
        {
            if (!callBackBreaks.HasValue)
                callBackBreaks = ENVIRONMENT_VARIABLES.RequestLimitMax;

            var mappedDatas = new List<Data.Models.Data>();

            try
            {
                var sleepData = Sleep(accessToken);
                mappedDatas.AddRange(sleepData.CastToDataDecimal(SonoIds));

                ForEachHearRate(accessToken, startDate, (heartRates, date) => {
                    mappedDatas.AddRange(heartRates);
                    if (ENVIRONMENT_VARIABLES.RequestLimitCount % callBackBreaks == 0 
                    && ENVIRONMENT_VARIABLES.RequestLimitCount != 0)
                    {
                        callBack(mappedDatas, date);
                        mappedDatas = new List<Data.Models.Data>();
                    }
                }, endDate);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ENVIRONMENT_VARIABLES.SaveJson();

                if (mappedDatas.Any())
                    callBack(mappedDatas, DateTime.Now);
            }
        }
    }
}
