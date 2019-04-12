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

        public List<Data.Models.DataDecimal> CastToDataDecimal(SonoIdsStructure sonoIds)
        {
            var mappedDatas = new List<Data.Models.DataDecimal>();
            var collectionDate = DateTime
                .Parse(this.startTime);

            mappedDatas.Add(new Data.Models.DataDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = timeInBed,
                GroupId = sonoIds.Totals.TimeInBed.Id,
            });

            mappedDatas.Add(new Data.Models.DataDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesAsleep,
                GroupId = sonoIds.Totals.Asleep.Id,
            });

            mappedDatas.Add(new Data.Models.DataDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = efficiency,
                GroupId = sonoIds.Totals.Efficiency.Id,
            });

            mappedDatas.Add(new Data.Models.DataDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = duration,
                GroupId = sonoIds.Totals.Duration.Id,
            });

            mappedDatas.Add(new Data.Models.DataDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesToFallAsleep,
                GroupId = sonoIds.Totals.FallAsleep.Id,
            });

            mappedDatas.Add(new Data.Models.DataDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesAfterWakeup,
                GroupId = sonoIds.Totals.AfterWakeup.Id,
            });

            mappedDatas.Add(new Data.Models.DataDecimal()
            {
                CollectionDate = collectionDate,
                DecimalValue = minutesAwake,
                GroupId = sonoIds.Totals.Awake.Id,
            });


            mappedDatas.AddRange(this.levels.summary.CastToDataDecimal(collectionDate, sonoIds.Summary));

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

            return mappedDatas;


        }
    }
}
