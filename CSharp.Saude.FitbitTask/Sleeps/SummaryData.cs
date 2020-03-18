namespace CSharp.Saude.FitbitTask.Sleeps
{
    using System;
    using System.Collections.Generic;

    public class SummaryData
    {
        public int count { get; set; }
        public int minutes { get; set; }

        public static List<global::Data.Models.DataDecimal> CastToDataDecimal(SummaryData data, DateTime collectionDate, Guid groupIdData, Guid grouIdCount)
        {
            var mappedDatas = new List<global::Data.Models.DataDecimal>();
            if (data != null)
            {
                mappedDatas.Add(new global::Data.Models.DataDecimal()
                {
                    CollectionDate = collectionDate,
                    DecimalValue = data.minutes,
                    GroupId = groupIdData
                });

                mappedDatas.Add(new global::Data.Models.DataDecimal()
                {
                    CollectionDate = collectionDate,
                    DecimalValue = data.count,
                    GroupId = grouIdCount
                });
            }
            return mappedDatas;
        }

        public static List<global::Data.Models.DataDecimal> CastToDataDecimal(SummaryData data, DateTime collectionDate, IId groupIdData, IId grouIdCount)
        {
            return CastToDataDecimal(data, collectionDate, groupIdData.Id, grouIdCount.Id);
        }
    }
}
