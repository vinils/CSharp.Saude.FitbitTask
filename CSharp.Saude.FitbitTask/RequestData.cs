namespace CSharp.Saude.FitbitTask
{
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public class RequestData
    {
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

        public static HeartRates.Response HeartRate(string acessToken, DateTime date)
        {
            var client = new RestClient("https://api.fitbit.com/1/user/-/activities/heart/date/" + date.ToString("yyyy-MM-dd") + "/1d/1sec/time/00:00/23:59.json");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + acessToken);
            var response = client.Execute<HeartRates.Response>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            return response.Data;
        }

        public static Sleeps.Response Sleep(string acessToken)
        {
            var beforeDate = DateTime.Today.AddDays(1);
            var url = "https://api.fitbit.com/1.2/user/-/sleep/list.json?limit=100&sort=desc&beforeDate="
                + beforeDate.ToString("yyyy-MM-dd") + "&offset=0";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Bearer " + acessToken);

            var response = client.Execute<Sleeps.Response>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            return response.Data;
        }

        public static List<Data.Models.Data> Test(Info info)
        {
            var mappedDatas = new List<Data.Models.Data>();
            var sleepData = Sleep(info.AcessToken);

            mappedDatas.AddRange(sleepData.CastToDataDecimal(SonoIds));


            var cardioGroupId = new Guid("C0EFE267-E8ED-4B79-A125-DB15ABC0780D");
            var date = DateTime.Today;
            var heartRateData = HeartRate(info.AcessToken, date);

            mappedDatas.AddRange(heartRateData.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date));

            var date1 = DateTime.Today.AddDays(-1);
            var heartRateData1 = HeartRate(info.AcessToken, date1);
            mappedDatas.AddRange(heartRateData1.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date1));

            var date2 = DateTime.Today.AddDays(-2);
            var heartRateData2 = HeartRate(info.AcessToken, date2);
            mappedDatas.AddRange(heartRateData2.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date2));

            var date3 = DateTime.Today.AddDays(-3);
            var heartRateData3 = HeartRate(info.AcessToken, date3);
            mappedDatas.AddRange(heartRateData3.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date3));

            var date4 = DateTime.Today.AddDays(-4);
            var heartRateData4 = HeartRate(info.AcessToken, date4);
            mappedDatas.AddRange(heartRateData4.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date4));

            return mappedDatas;
        }
    }
}
