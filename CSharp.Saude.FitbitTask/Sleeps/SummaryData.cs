namespace CSharp.Saude.FitbitTask.Sleeps
{
    using System;
    using System.Collections.Generic;

    public class SummaryData
    {
        public int count { get; set; }
        public int minutes { get; set; }

        public static List<Data.Models.ExamDecimal> CastToExamDecimal(SummaryData data, DateTime collectionDate, Guid groupIdData, Guid grouIdCount)
        {
            var mappedExams = new List<Data.Models.ExamDecimal>();
            if (data != null)
            {
                mappedExams.Add(new Data.Models.ExamDecimal()
                {
                    CollectionDate = collectionDate,
                    DecimalValue = data.minutes,
                    GroupId = groupIdData
                });

                mappedExams.Add(new Data.Models.ExamDecimal()
                {
                    CollectionDate = collectionDate,
                    DecimalValue = data.count,
                    GroupId = grouIdCount
                });
            }
            return mappedExams;
        }

        public static List<Data.Models.ExamDecimal> CastToExamDecimal(SummaryData data, DateTime collectionDate, IId groupIdData, IId grouIdCount)
        {
            return CastToExamDecimal(data, collectionDate, groupIdData.Id, grouIdCount.Id);
        }
    }
}
