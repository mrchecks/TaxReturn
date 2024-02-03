using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ReadPDF
{
    public class SaParser : StatementParser
    {
        public new const string ConfigFileName = "SaConfig.txt";

        public SaParser(string statementsSourceFolder, string outputCsvFilePath) : base(statementsSourceFolder, outputCsvFilePath)
        {
        }
        public override void ParseInputFolder()
        {
            var sortedFiles = Directory.GetFiles(StatementsSourceFolder).OrderBy(f => f);
            Statements = new List<Statement>();
            foreach (var file in sortedFiles)
            {
                Statement statement = new SaStatement(file);
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
                {
                    var tokens = fileName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length > 2)
                    {
                        int month = GetMonthNumberFromName(tokens[1]);
                        string monthStr = month < 10 ? "0" + month.ToString() : month.ToString();
                        string year = tokens[2];
                        if (monthStr.Length == 2 && year.Length == 4)
                        {
                            string newName = year + "-" + monthStr + ".pdf";
                            string directory = Path.GetDirectoryName(filePath);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
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
                sw.WriteLine("Date,Description,Expenses");
                foreach (Statement statement in Statements)
                {
                    foreach (StatementEntry entry in statement.StatementEntries)
                    {
                        var csvLine = entry.DateStr + "," + entry.Description + "," + entry.Amount;
                        sw.WriteLine(csvLine);
                    }
                    sw.WriteLine(",,");
                }
                sw.Flush();
                sw.Close();
            }
        }
    }
}
