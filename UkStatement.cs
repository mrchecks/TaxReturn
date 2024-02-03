using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReadPDF
{
    public class UkStatement : Statement
    {
        public UkStatement(string statementFilePath)
        {
            StatementFilePath = statementFilePath;
        }

        public override void ParseStatement(List<ConfigEntry> configEntries)
        {
            var fileText = GetText(StatementFilePath);
            var datePattern = @"[0-9]{2}\s(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s[0-9]{2}";
            var dateMatch = Regex.Match(fileText, datePattern);
            StatementEntries = new List<StatementEntry>();

            while (dateMatch.Success)
            {
                if (dateMatch.Index < (fileText.Length + 1))
                {
                    var cropped = fileText.Substring(dateMatch.Index + 1);
                    //We now need to find the next date, so we can work out the end of the line
                    var match = Regex.Match(cropped, datePattern);
                    var endPos = match.Success ? match.Index : cropped.Length;
                    var dateLine = fileText.Substring(dateMatch.Index, (endPos - dateMatch.Index) + dateMatch.Index + 1);
                    var matches = "(" + String.Join("|", configEntries.Select(x => x.SearchString).ToArray()) + ")";

                    var dateLineLen = dateLine.Length;
                    while (!String.IsNullOrEmpty(dateLine))
                    {//There can be multiple items per date entry, so process them one at a time and crop the string to remove each one as we go.
                        var descriptionMatch = Regex.Match(dateLine, matches);

                        var entry = new StatementEntry();
                        if (descriptionMatch.Success)
                        {
                            dateLine = dateLine.Substring(descriptionMatch.Index);      //crop away any other irrelevant entries not matched.
                            
                            var configEntry = configEntries.FirstOrDefault(x => x.SearchString == descriptionMatch.Value);
                            if (configEntry != null)
                            {
                                entry.SetFromInput(configEntry.AmountType, dateMatch.Value, descriptionMatch.Value, ref dateLine, configEntry.PreSearchString);
                            }
                            StatementEntries.Add(entry);
                            Console.WriteLine(entry.DateStr + " " + entry.Description + " " + entry.Amount);
                        }
                        else
                        {
                            dateLine = null;
                        }
                    }

                    fileText = fileText.Substring(dateLineLen + dateMatch.Index);
                    dateMatch = Regex.Match(fileText, datePattern);
                }
            }
        }
    }
}
