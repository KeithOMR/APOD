using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APOD
{
    public static class Constants
    {
        public static string ApodUrl { get { return "https://api.nasa.gov/planetary/apod?"; } }

        public static string ApiKey { get { return "api_key=Replace_Me"; } }

        public const int ApodDataDays = 20;
    }
}
