using Sharpenter.ResumeParser.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Sharpenter.ResumeParser.InputReader.Plain
{
    public class StringInputReader : InputReaderBase
    {
        protected override bool CanHandle(string location)
        {
            return !location.EndsWith("txt"); 
        }

        protected override IList<string> Handle(string resumeText)
        {
            var lines = new List<string>();
            using (var reader = new StringReader(resumeText))
            {
                string line;
                while ((line = reader.ReadLine())!=null)
                {
                    
                    var lineArray = line.Split('\t');
                    foreach (var l in lineArray)
                    {

                        if (!string.IsNullOrEmpty(preserveAlphaNumeric(l)))
                            lines.Add(l.Trim());
                    }

                }
            }
            return lines;

        }

        public string preserveAlphaNumeric(string str)
        {
            Regex rgx = new Regex(@"[^A-Za-z0-9~!#$^&*()_+|`\-=\\{}:"">?<\[\];',./  ]");
            str = rgx.Replace(str, "");
            str = str.Trim();
            return str;
        }
    }
}
