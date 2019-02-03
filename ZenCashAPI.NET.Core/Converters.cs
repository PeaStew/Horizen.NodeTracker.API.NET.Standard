using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCashAPI.NET.Core
{
    public static class Converters
    {
        public static string GetTimeRep(Int64 milliseconds)
        {
            var ts = TimeSpan.FromMilliseconds(milliseconds).ToString("g").Split(':');
            var codes = new[] { "s", "m", "h", "d " };
            var cnt = 0;
            var ret = string.Empty;
            for (var i = ts.Length - 1; i > -1; i--)
            {
                if (ts[i] == "0" || ts[i] == "00") continue;
                ret = ts[i] + codes[cnt] + ret;
                cnt++;
            }
            return ret;
        }
    }
}
