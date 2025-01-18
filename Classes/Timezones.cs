using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969_WGU_TallisJordan.Classes
{
    public static class TimezoneHelper
    {
        private static readonly TimeZoneInfo EasternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public static DateTime ConvertUtcToEastern(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, EasternZone);
        }

        public static DateTime ConvertLocalToEastern(DateTime localDateTime)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, localZone);
            return ConvertUtcToEastern(utcDateTime);
        }

        public static DateTime ConvertEasternToLocal(DateTime easternDateTime)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(easternDateTime, EasternZone);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localZone);
        }
    }
}
