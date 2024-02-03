using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReadPDF
{
    public class SaStatement : Statement
    {
        public SaStatement(string statementFilePath)
        {
            StatementFilePath = statementFilePath;
        }

        public override void ParseStatement(List<ConfigEntry> configEntries)
        {
            var fileText = GetText(StatementFilePath);
            int statementDatePos = fileText.IndexOf("Statement Date :");
            if (statementDatePos > 0)
            {//Trim the first part of the statement, which contains dates, which might assign an incorrect date to the first entry.
                fileText = fileText.Substring(statementDatePos + 30);
            }

            var dateMatch = Regex.Match(fileText, @"[0-9]{2}\s(Jan |Feb |Mar |Apr |May |Jun |Jul |Aug |Sep |Oct |Nov |Dec )");
            StatementEntries = new List<StatementEntry>();
            while (dateMatch.Success)
            {
                if (dateMatch.Index < (fileText.Length + 1))
                {
                    var cropped = fileText.Substring(dateMatch.Index);      //There is no startPos for regex (except in .NET Core) so this is a hack for that. We are trying to find the next date (after this one) so we can find the start and end of the date line.

                    //We now need to find the next date, so we can work out the end of the line
                    var match2 = Regex.Match(cropped, @"[0-9]*[.][0-9][0-9]");
                    var endPos = match2.Success ? match2.Index + match2.Value.Length : cropped.Length;
                    var dateLine = cropped.Substring(0, endPos);

                    var dateLineLen = dateLine.Length;
                    var isRefund = false;
                    if (dateLine.Length + 3 <= cropped.Length && cropped.Substring(dateLine.Length, 3).ToLower() == " cr")
                    {
                        isRefund = true;
                    }

                    var matches = String.Join("|", configEntries.Select(x => x.SearchString).ToArray());
                    string regexStr = "(" + matches + ")";

                    while (!String.IsNullOrEmpty(dateLine))
                    {//There can be multiple items per date entry, so process them one at a time and crop the string to remove each one as we go.
                        var descriptionMatch = Regex.Match(dateLine, regexStr);
                        var entry = new StatementEntry();
                        if (descriptionMatch.Success)
                        {
                            dateLine = dateLine.Substring(descriptionMatch.Index);      //crop away any other irrelevant entries not matched.

                            entry.SetFromInput(AmountTypeEnum.Expense, dateMatch.Value, descriptionMatch.Value, ref dateLine);
                            if (isRefund)
                            {
                                entry.Amount = "-" + entry.Amount;
                            }
                            Console.WriteLine(entry.DateStr + " " + entry.Description + " " + entry.Amount);
                            StatementEntries.Add(entry);
                            
                        }
                        else
                        {
                            dateLine = null;
                        }
                    }
                    fileText = fileText.Substring(dateLineLen + dateMatch.Index);
                    dateMatch = Regex.Match(fileText, @"[0-9]{2}\s(Jan |Feb |Mar |Apr |May |Jun |Jul |Aug |Sep |Oct |Nov |Dec )");
                }
            }
        }

    }
}
