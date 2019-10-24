using Q4CsvParser.Domain;

namespace Q4CsvParser.Contracts
{
    public interface IParsingService
    {
        /// <summary>
        /// Accepts a string with the contents of the csv file in it and should return a parsed csv file.
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="containsHeader"></param>
        /// <returns></returns>
        CsvTable ParseCsv(string fileContent, bool containsHeader);

        /// <summary>
        /// Accepts a string with the contents of the CSV row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        CsvRow ParseRow(string row);
    }
}
