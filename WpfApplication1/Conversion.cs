using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public static class StringExtensions
    {

        public static string[] SplitSafe(this string value, string separator)
        {
            if (string.IsNullOrEmpty(value))
                return new string[0];

            // do not use separator.IsEmpty() here because whitespace like " " is a valid separator.
            // an empty separator "" returns array with value.
            if (separator == null)
            {
                separator = "|";

                if (value.IndexOf(separator) < 0)
                {
                    if (value.IndexOf(';') > -1)
                    {
                        separator = ";";
                    }
                    else if (value.IndexOf(',') > -1)
                    {
                        separator = ",";
                    }
                    else if (value.IndexOf(Environment.NewLine) > -1)
                    {
                        separator = Environment.NewLine;
                    }
                }
            }

            return value.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }

}
