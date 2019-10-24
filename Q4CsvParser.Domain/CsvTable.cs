using System.Collections.Generic;
using System.Text;

namespace Q4CsvParser.Domain
{
    public class CsvTable
    {
        public CsvRow HeaderRow { get; set; }
        public List<CsvRow> Rows { get; set; }
        
        public CsvTable()
        {
            Rows = new List<CsvRow>();
        }

        /// <summary>
        /// This is provided to make unit testing easier. Now you can create the object and then ToString it.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            if (HeaderRow != null)
                stringBuilder.AppendLine(HeaderRow.ToString());

            foreach (var row in Rows)
            {
                stringBuilder.AppendLine(row.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
