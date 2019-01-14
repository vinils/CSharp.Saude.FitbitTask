namespace CSharp.Saude.FitbitTask.HeartRates
{
    using RestSharp.Deserializers;

    public class Response
    {
        [DeserializeAs(Name = "activities-heart-intraday")]
        public ActivitiesHeartIntraday ActivitiesHeartIntradays { get; set; }
    }
}
