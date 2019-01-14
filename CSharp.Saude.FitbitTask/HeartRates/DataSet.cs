namespace CSharp.Saude.FitbitTask.HeartRates
{
    using System;

    public class DataSet
    {
        public string time { get; set; }
        public string value { get; set; }

        public Data.Models.ExamDecimal CastToExamDecimal(Guid cardioGroupId, DateTime collectionDate)
        {
            var exam = new Data.Models.ExamDecimal
            {
                CollectionDate = collectionDate,
                DecimalValue = decimal.Parse(value),
                GroupId = cardioGroupId,
            };

            return exam;
        }
    }
}
