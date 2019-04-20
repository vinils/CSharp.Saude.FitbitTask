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

        public static void Run(Info info)
        {
            var datas = RequestData.Test(info);
            DataBulkInsert(info.DataUriService, datas);
        }

        public static void Main(string[] args)
        {
            Info info = Info.Load();
            try
            {
                //RequestToken.Test(info);
                Run(info);
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

        public static void LoadAllHeartRate(Info info)
        {
            var date = new DateTime(2016, 1, 1);
            var mappedDatas = new List<global::Data.Models.Data>();
            while (date <= DateTime.Today)
            {
                for (var timewait = 1; timewait <= 140; timewait++)
                {
                    for (var timeinsert = 1; timeinsert <= 5; timeinsert++)
                    {
                        var cardioGroupId = new Guid("C0EFE267-E8ED-4B79-A125-DB15ABC0780D");
                        var heartRateData = RequestData.HeartRate(info.AcessToken, date);

                        if (heartRateData.ActivitiesHeartIntradays != null)
                        {
                            mappedDatas.AddRange(heartRateData.ActivitiesHeartIntradays.CastToDataDecimal(cardioGroupId, date));
                        }

                        System.Diagnostics.Debug.WriteLine(date);
                        date = date.AddDays(1);
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
