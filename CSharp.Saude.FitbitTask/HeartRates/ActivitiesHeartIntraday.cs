namespace CSharp.Saude.FitbitTask.HeartRates
{
    using System;
    using System.Collections.Generic;

    public class ActivitiesHeartIntraday
    {
        public List<DataSet> dataset { get; set; }

        public List<Data.Models.ExamDecimal> CastToExamDecimal(Guid cardioGroupId, DateTime date)
        {
            var mappedExams = new List<Data.Models.ExamDecimal>();

            foreach (var data in dataset)
            {
                var collectionDate = DateTime
                    .Parse(date.ToString("yyyy-MM-dd") + "T" + data.time);

                var exam = data.CastToExamDecimal(cardioGroupId, collectionDate);

                mappedExams.Add(exam);
            }

            return mappedExams;
        }
    }
}
