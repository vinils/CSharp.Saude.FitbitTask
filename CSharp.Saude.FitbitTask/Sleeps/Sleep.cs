namespace CSharp.Saude.FitbitTask.Sleeps
{
    using System;
    using System.Collections.Generic;

    public class Sleep
    {
        public struct SonoTotals
        {
            public OnlyId TimeInBed;
            public OnlyId Asleep;
            public OnlyId Efficiency;
            public OnlyId Duration;
            public OnlyId FallAsleep;
            public OnlyId AfterWakeup;
            public OnlyId Awake;
        }

        public struct SonoIdsStructure
        {
            public SonoTotals Totals;
            public Summary.SummaryIds Summary;
        }

        public string dateOfSleep { get; set; }
        public Int64 duration { get; set; }
        public int efficiency { get; set; }
        public string endTime { get; set; }
        public Int16 infoCode { get; set; }
        public Level levels { get; set; }
        public Int16 minutesAfterWakeup { get; set; }
        public int minutesAsleep { get; set; }
        public int minutesAwake { get; set; }
        public int minutesToFallAsleep { get; set; }
        public int timeInBed { get; set; }
        public string startTime { get; set; }
        public string type { get; set; }

        public List<Data.Models.ExamDecimal> CastToExamDecimal(SonoIdsStructure sonoIds)
        {
            var mappedExams = new List<Data.Models.ExamDecimal>();
            var collectionDate = DateTime
                .Parse(this.startTime);

            mappedExams.Add(new Data.Models.ExamDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = timeInBed,
                GroupId = sonoIds.Totals.TimeInBed.Id,
            });

            mappedExams.Add(new Data.Models.ExamDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesAsleep,
                GroupId = sonoIds.Totals.Asleep.Id,
            });

            mappedExams.Add(new Data.Models.ExamDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = efficiency,
                GroupId = sonoIds.Totals.Efficiency.Id,
            });

            mappedExams.Add(new Data.Models.ExamDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = duration,
                GroupId = sonoIds.Totals.Duration.Id,
            });

            mappedExams.Add(new Data.Models.ExamDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesToFallAsleep,
                GroupId = sonoIds.Totals.FallAsleep.Id,
            });

            mappedExams.Add(new Data.Models.ExamDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesAfterWakeup,
                GroupId = sonoIds.Totals.AfterWakeup.Id,
            });

            mappedExams.Add(new Data.Models.ExamDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesAwake,
                GroupId = sonoIds.Totals.Awake.Id,
            });


            mappedExams.AddRange(this.levels.summary.CastToExamDecimal(collectionDate, sonoIds.Summary));

            //foreach(var data in sleep.levels.data)
            //{
            //    switch(data.level)
            //    {
            //        case "asleep":
            //            break;
            //        case "restless":
            //            break;
            //        default:
            //            break;

            //    }
            //}

            return mappedExams;


        }
    }
}
