namespace CSharp.Saude.FitbitTask.HeartRates
{
    using System;

    public class DataSet
    {
        public string time { get; set; }
        public string value { get; set; }

        public global::Data.Models.DataDecimal CastToDataDecimal(Guid cardioGroupId, DateTime collectionDate)
        {
            var data = new global::Data.Models.DataDecimal
            {
                CollectionDate = collectionDate,
                DecimalValue = decimal.Parse(value),
                GroupId = cardioGroupId,
            };

            return data;
        }
    }
}
