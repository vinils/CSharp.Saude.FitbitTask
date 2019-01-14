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

        public List<Data.Models.ExamDecimal> CastToExamDecimal(DateTime collectionDate, SummaryIds summaryIds)
        {
            var mappedExams = new List<Data.Models.ExamDecimal>();
            mappedExams.AddRange(SummaryData.CastToExamDecimal(this.deep, collectionDate, summaryIds.Deep, summaryIds.DeepCount));
            mappedExams.AddRange(SummaryData.CastToExamDecimal(this.light, collectionDate, summaryIds.Light, summaryIds.LightCount));
            mappedExams.AddRange(SummaryData.CastToExamDecimal(this.rem, collectionDate, summaryIds.Rem, summaryIds.RemCount));
            mappedExams.AddRange(SummaryData.CastToExamDecimal(this.wake, collectionDate, summaryIds.Wake, summaryIds.WakeCount));

            mappedExams.AddRange(SummaryData.CastToExamDecimal(this.asleep, collectionDate, summaryIds.Asleep, summaryIds.AsleepCount));
            mappedExams.AddRange(SummaryData.CastToExamDecimal(this.awake, collectionDate, summaryIds.Awake, summaryIds.AwakeCount));
            mappedExams.AddRange(SummaryData.CastToExamDecimal(this.restless, collectionDate, summaryIds.Restless, summaryIds.RestlessCount));
            return mappedExams;
        }
    }
}
