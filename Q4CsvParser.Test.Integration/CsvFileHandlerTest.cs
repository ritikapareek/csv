using Q4CsvParser.Contracts;
using Q4CsvParser.Domain;
using Q4CsvParser.Web.Core;
using System;
using System.IO;
using Xunit;

namespace Q4CsvParser.Test.Integration
{
    /// <summary>
    /// Bonus Task:
    /// We've provided a few testing files. Integration test the csv file handler using these files.
    /// Feel free to use any testing framework you desire. (i.e. NUnit, XUnit, Microsoft built-in testing framework)
    /// </summary>
    public class CsvFileHandlerTest
    {
        private const string JunkFileName = "junk.txt";
        private const string HeaderBlankLinesFileName = "sample.with.header.blank.lines.csv";
        private const string HeaderFileName = "sample.with.header.csv";
        private const string HeaderMissingFieldsFileName = "sample.with.header.missing.fields.csv";
        private const string NoHeaderThreeRowsFileName = "sample.without.header.3.rows.csv";
        private const string NoHeaderFileName = "sample.without.header.csv";

        private string GetFilePath(string fileName)
        {
            return $@"..\..\TestFiles\{fileName}";
        }
        //TODO integration test the CsvFileHandler here

        [Fact]
        public void TestParseCsvFile()
        {
            ICsvFileHandler _csvFileHandler = new CsvFileHandler(new ParsingService(), new ValidationService(), new FileService());
            string csvFileContent = File.ReadAllText(HeaderFileName);
            CsvTable csvTable = new CsvTable();
            string[] lines = csvFileContent.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.RemoveEmptyEntries
                );

            for (int i = 0; i < lines.Length; i++)
            {
                var row = lines[i];
                if (i == 0)
                {
                    csvTable.HeaderRow = ParseRow(row);
                }
                else
                {
                    csvTable.Rows.Add(ParseRow(row));
                }

            }
            CsvHandleResult csvHandleResult = new CsvHandleResult();
            csvHandleResult.Success = true;
            csvHandleResult.ParsedCsvContent = csvTable;

            // todo mock HttpPostedFileBase
            // Assert(_csvFileHandler.ParseCsvFile

        }

        private CsvRow ParseRow(string line)
        {
            line = line.Trim();
            var row = new CsvRow();
            var splits = line.Split(',');
            for (int i = 0; i < splits.Length; i++)
            {
                row.Columns.Add(new CsvColumn(splits[i]));
            }
            return row;
        }

    }
}
