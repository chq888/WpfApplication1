using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApplication1.Utility
{

    public enum DateExtractValue
    {
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second
    }


    public static class DateTimeExtensions
    {

        public static string GetString(this DateTime dt, string format = "M/d/yyyy")
        {
            return dt.ToString(format);
        }

        public static string GetShortDateString(this DateTime dt)
        {
            return dt.ToShortDateString();
        }

        /// <summary>
        /// Extracts the datetime value.
        /// </summary>
        public static DateTime ExtractValue(this DateTime dt, DateExtractValue extractValueTo)
        {
            if (extractValueTo == DateExtractValue.Year)
                return new DateTime(dt.Year, 0, 0);
            else if (extractValueTo == DateExtractValue.Month)
                return new DateTime(dt.Year, dt.Month, 0);
            else if (extractValueTo == DateExtractValue.Day)
                return new DateTime(dt.Year, dt.Month, dt.Day);
            else if (extractValueTo == DateExtractValue.Hour)
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            else if (extractValueTo == DateExtractValue.Minute)
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            else
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

    }


    /// <summary>
    /// Convert helper
    /// </summary>
    public static class ConversionHelper
    {

        public const string STRING_TYPE = "string";
        public const string SKIP_DATETIME_PARTS = ":ss";

        public static readonly List<string> DATE_FORMATS = new List<string>()
                {
                    "yyyy/M/d",
                    "yyyy/M/d HH:mm",
                    "yyyy/M/d hh:mm tt",
                    "yyyy/M/d H:mm",
                    "yyyy/M/d h:mm tt",
                    "yyyy/M/d HH:mm:ss",
                    "yyyy/M/d h:mm:ss tt",
                    "yyyy/M/d hh:mm:ss tt",
                    "yyyy/MM/dd",
                    "yyyy/MM/dd HH:mm",
                    "yyyy/MM/dd hh:mm tt",
                    "yyyy/MM/dd H:mm",
                    "yyyy/MM/dd h:mm tt",
                    "yyyy/MM/dd HH:mm:ss",
                    "yyyy/MM/d h:mm:ss tt",
                    "yyyy/MM/d hh:mm:ss tt",
                    "M/d/yyyy",
                    "M/d/yyyy HH:mm",
                    "M/d/yyyy hh:mm tt",
                    "M/d/yyyy H:mm",
                    "M/d/yyyy h:mm tt",
                    "M/d/yyyy HH:mm:ss",
                    "M/d/yyyy h:mm:ss tt", // 2/22/2017 8:58:12 PM
                    "M/d/yyyy hh:mm:ss tt",
                    "MM/dd/yyyy",
                    "MM/dd/yyyy HH:mm",
                    "MM/dd/yyyy hh:mm tt",
                    "MM/dd/yyyy H:mm",
                    "MM/dd/yyyy h:mm tt",
                    "MM/dd/yyyy HH:mm:ss",
                    "MM/d/yyyy h:mm:ss tt",
                    "MM/d/yyyy hh:mm:ss tt",
                    "yyyy-M-d",
                    "yyyy-M-d HH:mm",
                    "yyyy-M-d hh:mm tt",
                    "yyyy-M-d H:mm",
                    "yyyy-M-d h:mm tt",
                    "yyyy-M-d HH:mm:ss",
                    "yyyy-M-d h:mm:ss tt",
                    "yyyy-M-d hh:mm:ss tt",
                    "yyyy-MM-dd",
                    "yyyy-MM-dd HH:mm",
                    "yyyy-MM-dd hh:mm tt",
                    "yyyy-MM-dd H:mm",
                    "yyyy-MM-dd h:mm tt",
                    "yyyy-MM-dd HH:mm:ss",
                    "yyyy-MM-d h:mm:ss tt",
                    "yyyy-MM-d hh:mm:ss tt",
                    "M-d-yyyy",
                    "M-d-yyyy HH:mm",
                    "M-d-yyyy hh:mm tt",
                    "M-d-yyyy H:mm",
                    "M-d-yyyy h:mm tt",
                    "M-d-yyyy HH:mm:ss",
                    "M-d-yyyy h:mm:ss tt",
                    "M-d-yyyy hh:mm:ss tt",
                    "MM-dd-yyyy",
                    "MM-dd-yyyy HH:mm",
                    "MM-dd-yyyy hh:mm tt",
                    "MM-dd-yyyy H:mm",
                    "MM-dd-yyyy h:mm tt",
                    "MM-dd-yyyy HH:mm:ss",
                    "MM-d-yyyy h:mm:ss tt",
                    "MM-d-yyyy hh:mm:ss tt"
                };

        public static readonly List<string> TIME_FORMATS = new List<string>()
                {
                    "HH:mm",
                    "hh:mm tt",
                    "H:mm",
                    "h:mm tt",
                    "HH:mm:ss",
                    "h:mm:ss tt",
                    "hh:mm:ss tt"
                };

        /// <summary>
        /// Get only date value string as 2017/1/1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetDateTimeValue(string value, string toFormat)
        {
            return GetDateTimeValue(value, GetDateTimeFormat(value), toFormat, DateExtractValue.Day);
        }

        /// <summary>
        /// Get only date value string as 2017/1/1
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetDateTimeValue(string value, string fromFormat, string toFormat)
        {
            return GetDateTimeValue(value, fromFormat, toFormat, DateExtractValue.Day);
        }

        /// <summary>
        /// Gets the date time value.
        /// </summary>
        public static string GetDateTimeValue(string value, string toFormat, DateExtractValue extractValueTo)
        {
            return GetDateTimeValue(value, toFormat, toFormat, extractValueTo);
        }

        /// <summary>
        /// Gets the date time value.
        /// </summary>
        public static string GetDateTimeValue(string value, string fromFormat, string toFormat, DateExtractValue extractValueTo)
        {
            try
            {
                // cannot convert time to datetime
                if (TIME_FORMATS.Contains(fromFormat) && DATE_FORMATS.Contains(toFormat))
                {
                    return string.Empty;
                }

                var dateValue = ConvertDateTimeValue(value, fromFormat);
                if (dateValue == null)
                {
                    return string.Empty;
                }

                return dateValue.Value.ExtractValue(extractValueTo).ToString(toFormat);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts the date time value.
        /// </summary>
        private static DateTime? ConvertDateTimeValue(string value, string fromFormat)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            try
            {

                return DateTime.ParseExact(value, fromFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get DateTime format as MM/dd/yyyy
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDateTimeFormat(string value)
        {
            return GetDateTimeFormat(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get DateTime format as MM/dd/yyyy
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDateTimeFormat(string value, CultureInfo culture)
        {
            string format = string.Empty;
            try
            {
                DateTime dateTime;
                bool isValid = false;
                for (int i = 0; i < DATE_FORMATS.Count; i++)
                {
                    isValid = DateTime.TryParseExact(value,
                    DATE_FORMATS[i],
                    culture,
                    DateTimeStyles.None,
                    out dateTime);
                    if (isValid)
                    {
                        format = DATE_FORMATS[i];
                        break;
                    }
                }

                if (!isValid)
                {
                    for (int i = 0; i < TIME_FORMATS.Count; i++)
                    {
                        isValid = DateTime.TryParseExact(value,
                        TIME_FORMATS[i],
                        culture,
                        DateTimeStyles.None,
                        out dateTime);
                        if (isValid)
                        {
                            format = TIME_FORMATS[i];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return format;
        }

    }

}
