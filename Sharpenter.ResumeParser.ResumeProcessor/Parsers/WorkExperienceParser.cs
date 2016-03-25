using System;
using System.Linq;
using System.Text.RegularExpressions;
using Sharpenter.ResumeParser.Model;
using Sharpenter.ResumeParser.Model.Models;
using Sharpenter.ResumeParser.ResumeProcessor.Helpers;
using System.Collections.Generic;
using System.Reflection;

namespace Sharpenter.ResumeParser.ResumeProcessor.Parsers
{
    public class WorkExperienceParser : IParser
    {
        private static readonly Regex SplitByWhiteSpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);        
        private readonly List<string> _jobLookUp;
        private readonly List<string> _countryLookUp;
        private readonly List<string> _usStatesLookUp;       

        public WorkExperienceParser(IResourceLoader resourceLoader)
        {
            var assembly = Assembly.GetExecutingAssembly();

            _jobLookUp = new List<string>(resourceLoader.Load(assembly, "JobTitles.txt", ','));
            _countryLookUp = new List<string>(resourceLoader.Load(assembly, "Countries.txt", '|'));
            _usStatesLookUp = new List<string>(resourceLoader.Load(assembly, "USStates.txt", ','));            
        }

        public void Parse(Section section, Resume resume)
        {
            resume.Positions = new List<Position>();

            var i = 0;
            //List<int> advanceParse = new List<int>();
            Position currentPosition = null;
            while (i < section.Content.Count)
            {
                var line = section.Content[i];
                var title = FindJobTitle(line);
                var company = FindJobCompany(line);
               
                if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(company))
                {
                    if (currentPosition != null)
                    {
                        var startAndEndDate = DateHelper.ParseStartAndEndDate(line);                        
                        if (startAndEndDate != null)
                        {
                            currentPosition.StartDate = startAndEndDate.Start;
                            currentPosition.EndDate = startAndEndDate.End;
                        }
                        else
                        {
                            currentPosition.Summary.Add(line);
                        }                        
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(company))
                    {
                        if (currentPosition == null || !string.IsNullOrWhiteSpace(currentPosition.Title))
                        {
                            currentPosition = new Position
                            {
                                Title = title
                            };
                            resume.Positions.Add(currentPosition);
                        }
                        else
                            currentPosition.Title = title;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(title))
                        {
                            if (currentPosition == null || !string.IsNullOrWhiteSpace(currentPosition.Company))
                            {
                                currentPosition = new Position
                                {
                                    Company = company
                                };
                                resume.Positions.Add(currentPosition);
                            }
                            else
                                currentPosition.Company = company;

                        }
                        
                    }

                    
                }

                i++;
            }            
        }

        private string FindJobTitle(string line)
        {
            var elements = SplitByWhiteSpaceRegex.Split(line);
            if (elements.Length > 6)
            {
                return string.Empty;
            }

            return _jobLookUp.FirstOrDefault(job => line.IndexOf(job, StringComparison.InvariantCultureIgnoreCase) > -1);
        }

        private string FindJobCompany(string line)
        {
            var words = SplitByWhiteSpaceRegex.Split(line);
            string country = null;
            foreach (var word in words)
            {
                word.Trim();
                if (_countryLookUp.Contains(word))
                {
                    country = word;
                    break;
                }
                else if (_usStatesLookUp.Contains(word))
                {
                    country = word;
                    break;
                }

            }
            // country =
            //       _countryLookUp.FirstOrDefault(
            //         c => line.IndexOf(c, StringComparison.InvariantCultureIgnoreCase) > -1);
            if (country == null)
            {
                return string.Empty;
            }
            else
            {
                return line;
            }
        }
    }
}
