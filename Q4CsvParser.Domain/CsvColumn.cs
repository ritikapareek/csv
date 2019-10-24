namespace Q4CsvParser.Domain
{
    public class CsvColumn
    {
        public string Value { get; }

        public CsvColumn(string value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value;
        }
    }
}
