using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sharpenter.ResumeParser.ResumeProcessor.Helpers
{
    static class StringHelper
    {
        public static String preserveAlphaNumeric(String str)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -,.:]");
            str = rgx.Replace(str, "");
            str = str.Trim();
            return str;
        }
    }
}
