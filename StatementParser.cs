using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace ReadPDF
{
    /// <summary>
    /// Creator: AbstractStatementParser
    /// ConcreteCreator1: UkParser
    /// ConcreteCreator2: SaParser
    /// 
    /// Abstract Product: Statement
    /// Concrete Product A: UkStatement
    /// Concrete Product B: SaStatement
    /// </summary>
    public abstract class StatementParser
    {
        public string StatementsSourceFolder { get; set; }
        public string OutputCsvFilePath { get; set; }
        public const string ConfigFileName = "Config.txt";
        public List<ConfigEntry> ConfigEntries { get; set; }

        public List<Statement> Statements = new List<Statement>();

        public StatementParser(string statementsSourceFolder, string outputCsvFilePath)
        {
            StatementsSourceFolder = statementsSourceFolder;
            OutputCsvFilePath = outputCsvFilePath;
        }

        public abstract void ParseInputFolder();


        public abstract void WriteToCsv();

        public int GetMonthNumberFromName(string monthname)
        {
            return DateTime.ParseExact(monthname, "MMM", CultureInfo.CurrentCulture).Month;
        }

        public abstract void RenameToSimpleDateFormat();

        public void SetConfigEntries(string configFilePath)
        {
            ConfigEntries = new List<ConfigEntry>();
            var lines = File.ReadLines(configFilePath);
            var isFirstLine = true;
            foreach (string line in lines)
            {
                if (!isFirstLine)
                {
                    ConfigEntry entry = new ConfigEntry();
                    var fields = line.Split('\t');
                    if (fields.Length > 0)
                    {
                        Enum.TryParse(fields[0], out AmountTypeEnum amountType);
                        entry.AmountType = amountType;
                    }
                    if (fields.Length > 1)
                    {
                        entry.SearchString = fields[1];
                    }
                    if (fields.Length > 2)
                    {
                        entry.PreSearchString = fields[2];
                    }
                    ConfigEntries.Add(entry);
                }
                isFirstLine = false;
            }
        }
    }
}
