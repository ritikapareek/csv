using System;
using Q4CsvParser.Contracts;
using Q4CsvParser.Domain;

namespace Q4CsvParser.Web.Core
{
    /// <summary>
    /// This file must be unit tested.
    /// </summary>
    public class ParsingService : IParsingService
    {
        /// <summary>
        /// Accepts a string with the contents of the csv file in it and should return a parsed csv file.
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="containsHeader"></param>
        /// <returns></returns>
        public CsvTable ParseCsv(string fileContent, bool containsHeader)
        {
            try
            {
                var csvTable = new CsvTable();

                // Checking for end of line \n character and carraige return \r in the blob of string
                string[] lines = fileContent.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.RemoveEmptyEntries
                );

                for (int i = 0; i < lines.Length; i++)
                {
                    var row = lines[i];

                    // header row
                    if (i == 0 && containsHeader)
                    {
                        csvTable.HeaderRow = ParseRow(row);
                    }
                    else
                    {
                        csvTable.Rows.Add(ParseRow(row));
                    }

                }

                return csvTable;
            }
            catch(Exception e)
            {
                // TODO log this
                Console.WriteLine("ParseCsv: " + e.Message);
                return null;
            }
        }


        /// <summary>
        /// Accepts a string with the contents of the CSV row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public CsvRow ParseRow(string line)
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
