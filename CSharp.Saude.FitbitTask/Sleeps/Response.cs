namespace CSharp.Saude.FitbitTask.Sleeps
{
    using System.Collections.Generic;

    public class Response
    {
        public List<Sleep> sleep { get; set; }

        public List<global::Data.Models.DataDecimal> CastToDataDecimal(Sleep.SonoIdsStructure sonoIds)
        {
            var mappedDatas = new List<global::Data.Models.DataDecimal>();

            foreach (var sleep in sleep)
            {
                mappedDatas.AddRange(sleep.CastToDataDecimal(sonoIds));
            }

            return mappedDatas;
        }
    }
}
