namespace CSharp.Saude.FitbitTask.Sleeps
{
    using System.Collections.Generic;

    public class Level
    {
        public List<Infos> data { get; set; }
        public List<Infos> shortData { get; set; }
        public Summary summary { get; set; }
    }
}
