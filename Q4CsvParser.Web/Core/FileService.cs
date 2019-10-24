using System;
using System.IO;
using System.Web;
using Q4CsvParser.Contracts;

namespace Q4CsvParser.Web.Core
{
    /// <summary>
    /// This file does not need to be unit testable.
    /// Bonus Task:
    /// - Make this file unit testable using the adapter pattern for your file system interactions
    /// - Unit test this file
    /// </summary>
    public class FileService : IFileService
    {
        // TODO This was not creating a app_data folder in the root directory of the project. It was creating a app_data in web, so created a 
        // app_data folder in web, find out why is this not creating a app_data in project root.
        private const string UploadFilePath = "~/App_Data/uploads/";

        /// <summary>
        /// This file takes the file from the HttpPostedFileBase and saves the file to the appData folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The file path in the appData folder the file was saved to</returns>
        public string StoreFile(HttpPostedFileBase file)
        {
            var filePath = Path.Combine(HttpContext.Current.Server.MapPath(UploadFilePath), file.FileName);

            try
            {
                var fileInfo = new FileInfo(filePath);
            }
            catch(Exception e)
            {
                // TODO log this exception
                Console.WriteLine(e.Message);
                return null;
            }

            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                file.InputStream.CopyTo(fileStream);
            }

            return filePath;
        }

        /// <summary>
        /// This function takes in the filePath of a csv file stored in the app data folder and return the string content
        /// of that file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>The contents of the file in a string</returns>
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
