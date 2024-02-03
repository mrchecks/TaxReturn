using System;
using System.Text.RegularExpressions;

namespace ReadPDF
{
    public enum AmountTypeEnum
    {
        Expense = 1,
        Income,
        MortgageInterest
    }

    public class StatementEntry
    {
        public string DateStr { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }

        public AmountTypeEnum AmountType { get; set; }

        public StatementEntry()
        {
        }

        public StatementEntry(string dateStr, string description, string amount, AmountTypeEnum amountType)
        {
            DateStr = dateStr;
            Description = description;
            Amount = amount;
            AmountType = amountType;
        }

        private bool IsNumericChar(char c)
        {
            return (c == ',' || ((int)c >= 48 && (int)c <= 57));
        }

        public void SetFromInput(AmountTypeEnum amountType, string dateString, string description, ref string dateLine, string preSearchString = null)
        {
            Match amount;
            string amountStr;
            if (!String.IsNullOrEmpty(preSearchString))
            {
                var match = Regex.Match(dateLine, preSearchString + @"[0-9]*[.][0-9][0-9]");
                amount = Regex.Match(match.Value, @"[0-9]*[.][0-9][0-9]");
                amountStr = amount.Value;
                int i = amount.Index - 1;
                while (IsNumericChar(dateLine[i]))
                {
                    amountStr = dateLine[i] != ',' ? dateLine[i] + amountStr : amountStr;
                    i--;
                }
                dateLine = dateLine.Substring(match.Index);
            }
            else
            {
                amount = Regex.Match(dateLine, @"[0-9]*[.][0-9][0-9]");
                amountStr = amount.Value;
                int i = amount.Index - 1;
                while (IsNumericChar(dateLine[i]))
                {
                    amountStr = dateLine[i] != ',' ? dateLine[i] + amountStr : amountStr;
                    i--;
                }

                dateLine = dateLine.Substring(amount.Index);
            }
            DateStr = dateString;
            Description = description.Replace("\n", " ");
            Amount = amountStr;
            AmountType = amountType;
        }
    }
}
