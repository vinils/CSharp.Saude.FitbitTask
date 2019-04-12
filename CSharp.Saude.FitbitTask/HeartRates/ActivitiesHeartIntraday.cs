namespace CSharp.Saude.FitbitTask.HeartRates
{
    using System;
    using System.Collections.Generic;

    public class ActivitiesHeartIntraday
    {
        public List<DataSet> dataset { get; set; }

        public List<Data.Models.DataDecimal> CastToDataDecimal(Guid cardioGroupId, DateTime date)
        {
            var mappedDatas = new List<Data.Models.DataDecimal>();

            foreach (var dt in dataset)
            {
                var collectionDate = DateTime
                    .Parse(date.ToString("yyyy-MM-dd") + "T" + dt.time);

                var data = dt.CastToDataDecimal(cardioGroupId, collectionDate);

                mappedDatas.Add(data);
            }

            return mappedDatas;
        }
    }
}
