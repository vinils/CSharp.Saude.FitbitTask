namespace CSharp.Saude.FitbitTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        public static bool hasRefreshed = false;

        public static void ExamBulkInsert(string dataUrlService, ICollection<Data.Models.Exam> exams)
        {
            var dataUriStr = dataUrlService;
            var dataUri = new Uri(dataUriStr);
            var container = new Default.Container(dataUri);
            container.Timeout = int.MaxValue;

            //foreach (var p in container.Groups)
            //{
            //    Console.WriteLine("{0} {1} {2}", p.Id, p.Name, p.ParentId);
            //}

            var bulkInsert = Default.ExtensionMethods.BulkInsert(container.Exams, exams);
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
        }

        public static void Run(Info info)
        {
            var exams = RequestData.Test(info);
            ExamBulkInsert(info.DataUriService, exams);
        }

        static void Main(string[] args)
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
