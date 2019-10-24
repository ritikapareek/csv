using System.Web;
using Q4CsvParser.Contracts;
using Q4CsvParser.Domain;

namespace Q4CsvParser.Web.Core
{
    /// <summary>
    /// This file does not need to be unit tested. 
    /// For bonus marks, it can be integration tested in the project Q4CsvParser.Core.Test.Integration.
    /// </summary>
    public class CsvFileHandler : ICsvFileHandler
    {
        private readonly IParsingService _parsingService;
        private readonly IValidationService _validationService;
        private readonly IFileService _fileService;

        public CsvFileHandler(IParsingService parsingService, IValidationService validationService,
            IFileService fileService)
        {
            _parsingService = parsingService;
            _validationService = validationService;
            _fileService = fileService;
        }

        /// <summary>
        /// Takes in an input HttpPostedFileBase and returns a parsed CsvTable object
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="containsHeader"></param>
        /// <returns></returns>
        public CsvHandleResult ParseCsvFile(HttpPostedFileBase inputFile, bool containsHeader)
        {
            var result = new CsvHandleResult();

            if (!_validationService.IsCsvFile(inputFile.FileName))
            {
                result.ErrorMessage =
                    $"Selected file, {inputFile.FileName}, does not have supported format CSV. Nothing has been uploaded";
                return result;
            }

            var uploadedFilePath = _fileService.StoreFile(inputFile);
            if (string.IsNullOrWhiteSpace(uploadedFilePath))
            {
                result.ErrorMessage = "File failed to save to server";
                return result;
            }

            var fileContent = _fileService.ReadFile(uploadedFilePath);
            if (string.IsNullOrWhiteSpace(fileContent))
            {
                result.ErrorMessage = "File had no content";
                return result;
            }

            var parsedFileContent = _parsingService.ParseCsv(fileContent, containsHeader);
            if (parsedFileContent == null)
            {
                result.ErrorMessage = "Failed to parse file content";
                return result;
            }

            result.Success = true;
            result.ParsedCsvContent = parsedFileContent;
            return result;
        }
    }
}
