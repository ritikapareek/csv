using System.Web.Mvc;
using log4net;
using Q4CsvParser.Contracts;
using Q4CsvParser.Web.Core;
using Q4CsvParser.Web.Models;
using FileService = Q4CsvParser.Web.Core.FileService;

namespace Q4CsvParser.Web.Controllers
{
    /// <summary>
    /// This file does not need to be unit tested. You shouldn't need to modify this.
    /// Bonus Task:
    /// Use your favourite dependency injection framework (i.e. Autofac, Ninject) to inject all the services.
    /// This project uses MVC5 so make sure you grab the right implementation. 
    /// Bonus Task:
    /// Validate the input to the post function on the client side. You can use javascript or Razor syntax, 
    ///  but don't use any 3rd party code for this.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ICsvFileHandler _csvFileHandler;
        private readonly ILog _logger;

        public HomeController()
        {
            _csvFileHandler = new CsvFileHandler(new ParsingService(), new ValidationService(), new FileService());
            _logger = LogManager.GetLogger("MvcApplication");
        }

        #region [ GETs ]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error(string errorMessage)
        {
            var model = new ErrorModel { ErrorMessage = errorMessage};
            return View(model);
        }
        #endregion

        #region [ POSTs ]
        [HttpPost]
        public ActionResult Index(FileUploadModel fileUploadModel)
        {
            if (fileUploadModel?.File == null || fileUploadModel.File.ContentLength <= 0)
                return HandleError("You need to click Choose File first, then Submit.");

            var result = _csvFileHandler.ParseCsvFile(fileUploadModel.File, fileUploadModel.ContainsHeader);
            if (!result.Success)
                return HandleError(result.ErrorMessage);
            
            return View("FormattedDisplay", new FormattedDisplayModel
            {
                OriginalFileName = fileUploadModel.File.FileName,
                CsvTable = result.ParsedCsvContent
            });
        }

        #endregion

        #region [ Helpers ]
        private ActionResult HandleError(string errorMessage)
        {
            _logger.Error(errorMessage);
            return RedirectToAction("Error", new {errorMessage});
        }
        #endregion
    }
}
