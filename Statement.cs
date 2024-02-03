using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace ReadPDF
{
    public abstract class Statement
    {
        public List<StatementEntry> StatementEntries { get; set; }
        public string StatementFilePath { get; set; }

        public abstract void ParseStatement(List<ConfigEntry> configEntries);

        public string GetText(string fileName)
        {
            var currentPageText = "";

            if (File.Exists(fileName))
            {
                using (PdfReader pdfReader = new PdfReader(fileName))
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    for (int page = 1; page <= pdfDocument.GetNumberOfPages(); page++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                        currentPageText += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page), strategy);
                        char c = '\t';
                        int i = currentPageText.IndexOf(c);
                    }
                }
            }
            return currentPageText;
        }
    }
}
