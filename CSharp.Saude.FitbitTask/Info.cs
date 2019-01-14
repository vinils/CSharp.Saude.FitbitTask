namespace CSharp.Saude.FitbitTask
{
    using System.IO;
    using Newtonsoft.Json;

    public class Info
    {
        private static readonly string filePath = Directory.GetCurrentDirectory() + "\\info.json";

        public string MyEmail { get; set; }
        public string MyEmailPassword { get; set; }
        public string DataUriService { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Code { get; set; }
        public string AcessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExperisIn { get; set; }

        public static Info Load()
            => JsonConvert.DeserializeObject<Info>(File.ReadAllText(filePath));

        public void Update()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, json);
        }
    }
}
