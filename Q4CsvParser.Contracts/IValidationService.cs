namespace Q4CsvParser.Contracts
{
    public interface IValidationService
    {
        /// <summary>
        /// Takes in a file name and determines whether it is a csv file or not.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool IsCsvFile(string filename);
    }
}
