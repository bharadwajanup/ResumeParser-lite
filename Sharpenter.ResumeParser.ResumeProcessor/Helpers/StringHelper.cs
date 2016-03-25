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
            Regex rgx = new Regex(@"[^A-Za-z0-9~!#$^&*()_+|`\-=\\{}:"">?<\[\];',./ ]");
            str = rgx.Replace(str, "");
            str = str.Trim();
            return str;
        }
    }
}
