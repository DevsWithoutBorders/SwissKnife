using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissKnife.Ext
{
    public static class DateTimeUtil
    {
        /// <summary>
        /// Return a DateTime with Date of the input value and Time 23:59:59
        /// </summary>
        /// <param name="date">DateTime to take Date part From</param>
        /// <returns>DateTime with time 23:59:59</returns>
        public static DateTime MakeEndOfDay(this DateTime date)
        {
            return new System.DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        /// <summary>
        /// Return a DateTime with Date of the input value and Time 00:00:00
        /// </summary>
        /// <param name="date">DateTime to take Date part From</param>
        /// <returns>DateTime with time 00:00:00</returns>
        public static System.DateTime MakeBeginOfDay(this System.DateTime date)
        {
            return new System.DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        /// <summary>
        /// Format for the ToDateTimeStamp method
        /// </summary>
        public enum DateTimeStampFormat
        {
            YearMonthDayHourMinute,
            YearMonthDayHourMinuteSecond,
            YearMonthDayHourMinuteSecondMilllisecond
        }

        /// <summary>
        /// Convert a DateTime to a String DateTimeStamp
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="format">Format to return</param>
        /// <returns>DateTime converted to the requested DateTimeStampFormat</returns>
        public static string ToDateTimeStamp(this DateTime date, DateTimeStampFormat format = DateTimeStampFormat.YearMonthDayHourMinuteSecond)
        {
            switch (format)
            {
                case DateTimeStampFormat.YearMonthDayHourMinute:
                    return date.Year.ToString("0000") + date.Month.ToString("00") + date.Day.ToString("00") + date.Hour.ToString("00") + date.Minute.ToString("00");
                case DateTimeStampFormat.YearMonthDayHourMinuteSecond:
                    return date.Year.ToString("0000") + date.Month.ToString("00") + date.Day.ToString("00") + date.Hour.ToString("00") + date.Minute.ToString("00") + date.Second.ToString("00");
                case DateTimeStampFormat.YearMonthDayHourMinuteSecondMilllisecond:
                    return date.Year.ToString("0000") + date.Month.ToString("00") + date.Day.ToString("00") + date.Hour.ToString("00") + date.Minute.ToString("00") + date.Second.ToString("00") + date.Millisecond.ToString("000");
                default:
                    throw new NotImplementedException("DateTimeStampFormat not impemented yet: " + format.ToString());                    
            }
        }
    }
}
