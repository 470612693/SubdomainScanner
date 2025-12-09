using System.Collections.Generic;

namespace SubdomainScanner
{
    public static class StringIEnumerableExt
    {
        public static string Join(this IEnumerable<string> value, string separator)
        {
            return string.Join(separator, value);
        }
    }
}
