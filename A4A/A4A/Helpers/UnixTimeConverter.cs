using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers
{
    public static class UnixTime
    {
        public static Int64 ToUnixTime(this DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }
        public static DateTime ToDateTime(this Int64 timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timeStamp);
        }
    }
}