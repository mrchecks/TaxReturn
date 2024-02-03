using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ReadPDF
{
    public class UkParser : StatementParser
    {
        public UkParser(string statementsSourceFolder, string outputCsvFilePath) : base(statementsSourceFolder, outputCsvFilePath)
        {
        }

        public new const string ConfigFileName = "UkConfig.txt";

        public override void ParseInputFolder()
        {
            var sortedFiles = Directory.GetFiles(StatementsSourceFolder).OrderBy(f => f);
            Statements = new List<Statement>();
            foreach (var file in sortedFiles)
            {
                Statement statement = new UkStatement(file);
                statement.ParseStatement(ConfigEntries);
                Statements.Add(statement);
            }
        }

        public override void RenameToSimpleDateFormat()
        {
            var sortedFiles = Directory.GetFiles(StatementsSourceFolder).OrderBy(f => f);
            foreach (var filePath in sortedFiles)
            {
                string fileName = Path.GetFileName(filePath);
                if (fileName?.Length > 11)
                {//fd statement 1234 01022021  => Jan 2021
                    var tokens = fileName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    if (tokens.Length > 0)
                    {
                        string dateStr = tokens[tokens.Length - 1];     //01022021
                        dateStr = dateStr.Substring(0, dateStr.IndexOf("."));
                        string yearStr = dateStr.Substring(dateStr.Length - 4);
                        string monthStr = dateStr.Substring(2, 2);

                        if (monthStr.Length == 2 && yearStr.Length == 4)
                        {
                            string newName = yearStr + "-" + monthStr + ".pdf";
                            string directory = Path.GetDirectoryName(filePath);
                            string newFilePath = Path.Combine(directory, newName);
                            System.IO.File.Move(filePath, newFilePath);
                        }
                    }
                }
            }
        }

        public override void WriteToCsv()
        {
            if (File.Exists(OutputCsvFilePath))
            {
                File.Delete(OutputCsvFilePath);
            }
            Encoding utf8WithBom = new UTF8Encoding(true);
            File.Create(OutputCsvFilePath).Dispose();

            using (TextWriter sw = new StreamWriter(OutputCsvFilePath, false, utf8WithBom))
            {
                sw.WriteLine("Date,Description,Expenses,Income,Mortgage Interest");
                foreach (Statement statement in Statements)
                {
                    foreach (StatementEntry entry in statement.StatementEntries)
                    {
                        var csvLine = entry.DateStr + "," + entry.Description + ",";
                        if (entry.AmountType == AmountTypeEnum.Expense)
                            csvLine += entry.Amount + ",,";
                        else if (entry.AmountType == AmountTypeEnum.Income)
                            csvLine += "," + entry.Amount + ",";
                        else if (entry.AmountType == AmountTypeEnum.MortgageInterest)
                            csvLine += ",," + entry.Amount;
                        sw.WriteLine(csvLine);
                    }
                    sw.WriteLine(",,,,");
                }
                sw.Flush();
                sw.Close();
            }
        }
    }
}
