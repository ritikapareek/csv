using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Q4CsvParser.Web.Models
{
    public class FileUploadModel
    {
        [Display(Name = "Please select to upload a CSV file")]
        public HttpPostedFileBase File { get; set; }
        [Display(Name = "Does the file contain a header?")]
        public bool ContainsHeader { get; set; }
    }
}