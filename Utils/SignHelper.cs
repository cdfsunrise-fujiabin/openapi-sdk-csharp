using System.Collections.Generic;
using System.Text;

namespace openapi_sdk.Utils
{
    public class SignHelper
    {
        public static string Sign(SortedDictionary<string, string> sortedDict)
        {
            if (sortedDict == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in sortedDict)
            {
                sb = sb.AppendFormat("{0}={1}&", item.Key, item.Value);
            }

            return sb.ToString().TrimEnd('&');
        }
    }
}