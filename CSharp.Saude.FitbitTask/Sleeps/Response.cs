namespace CSharp.Saude.FitbitTask.Sleeps
{
    using System.Collections.Generic;

    public class Response
    {
        public List<Sleep> sleep { get; set; }

        public List<Data.Models.ExamDecimal> CastToExamDecimal(Sleep.SonoIdsStructure sonoIds)
        {
            var mappedExams = new List<Data.Models.ExamDecimal>();

            foreach (var sleep in sleep)
            {
                mappedExams.AddRange(sleep.CastToExamDecimal(sonoIds));
            }

            return mappedExams;
        }
    }
}
