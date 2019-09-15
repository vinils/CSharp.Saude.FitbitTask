using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Saude.FitbitTask
{
    public static class Extensions
    {
        public static void ForEachDate(DateTime startDate, Action<DateTime> callBack, DateTime? endDate = null, int incrementDays = 1)
        {
            endDate = endDate ?? DateTime.Now;

            if (startDate >= endDate)
                return;

            for (var date = startDate; date <= endDate; date = date.AddDays(incrementDays))
                callBack(date);
        }

        public static void ForEach(this DateTime startDate, Action<DateTime> callBack, DateTime? endDate = null, int incrementDays = 1)
            => ForEachDate(startDate, callBack, endDate, incrementDays);

        public static T? ParseOrNull<T>(this string valueAsString)
            where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            try
            {
                return (T)Convert.ChangeType(valueAsString, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
