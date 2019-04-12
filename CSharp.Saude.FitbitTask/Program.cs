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
    }
}
