using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Infrastructure.Extentions
{
    public static class StringExtensions
    {
        public static void IsNotNullArgument(this string name, object argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static string Append(this string value, params string[] append)
        {
            
            return
                String.Concat(
                    value,
                    String.Join("",append));
        }
    }
}
