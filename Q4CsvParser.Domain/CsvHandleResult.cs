namespace Q4CsvParser.Domain
{
    public class CsvHandleResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public CsvTable ParsedCsvContent { get; set; }
    }
}
