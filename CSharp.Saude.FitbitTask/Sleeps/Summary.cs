namespace CSharp.Saude.FitbitTask.Sleeps
{
    using System;
    using System.Collections.Generic;

    public class Summary
    {
        public struct SummaryIds
        {
            public OnlyId Asleep;
            public OnlyId AsleepCount;
            public OnlyId Awake;
            public OnlyId AwakeCount;
            public OnlyId Deep;
            public OnlyId DeepCount;
            public OnlyId Light;
            public OnlyId LightCount;
            public OnlyId Rem;
            public OnlyId RemCount;
            public OnlyId Restless;
            public OnlyId RestlessCount;
            public OnlyId Wake;
            public OnlyId WakeCount;
        }

        public SummaryData deep { get; set; }
        public SummaryData light { get; set; }
        public SummaryData rem { get; set; }
        public SummaryData wake { get; set; }

        public SummaryData asleep { get; set; }
        public SummaryData awake { get; set; }
        public SummaryData restless { get; set; }

        public List<global::Data.Models.DataDecimal> CastToDataDecimal(DateTime collectionDate, SummaryIds summaryIds)
        {
            var mappedDatas = new List<global::Data.Models.DataDecimal>();
            mappedDatas.AddRange(SummaryData.CastToDataDecimal(this.deep, collectionDate, summaryIds.Deep, summaryIds.DeepCount));
            mappedDatas.AddRange(SummaryData.CastToDataDecimal(this.light, collectionDate, summaryIds.Light, summaryIds.LightCount));
            mappedDatas.AddRange(SummaryData.CastToDataDecimal(this.rem, collectionDate, summaryIds.Rem, summaryIds.RemCount));
            mappedDatas.AddRange(SummaryData.CastToDataDecimal(this.wake, collectionDate, summaryIds.Wake, summaryIds.WakeCount));

            mappedDatas.AddRange(SummaryData.CastToDataDecimal(this.asleep, collectionDate, summaryIds.Asleep, summaryIds.AsleepCount));
            mappedDatas.AddRange(SummaryData.CastToDataDecimal(this.awake, collectionDate, summaryIds.Awake, summaryIds.AwakeCount));
            mappedDatas.AddRange(SummaryData.CastToDataDecimal(this.restless, collectionDate, summaryIds.Restless, summaryIds.RestlessCount));
            return mappedDatas;
        }
    }
}
