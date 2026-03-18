using Microsoft.AspNetCore.Mvc;
using StructuraFlow.Models;
using StructuraFlow.Services;
using System.Diagnostics;

namespace StructuraFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExcelReader _reader;
        private readonly IValidator _validator;
        private readonly JsonExporter _jsonExporter;
        private readonly StructuralDbContext _context;

        public HomeController(ILogger<HomeController> logger, IExcelReader reader,IValidator validator, JsonExporter jsonExporter, StructuralDbContext context)
        {
            _logger = logger;
            _reader = reader;
            _validator = validator;
            _jsonExporter = jsonExporter;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Upload(IFormFile columnsFile, IFormFile beamsFile, IFormFile slabsFile)
        {
            var columns = _reader.ReadColumns(columnsFile.OpenReadStream());
            var beams = _reader.ReadBeams(beamsFile.OpenReadStream());
            var slabs = _reader.ReadSlabs(slabsFile.OpenReadStream());

            var config = _context.ValidationRules.FirstOrDefault();
            if (config == null) config = new ValidationRule(); // defaults

            var errors = _validator.Validate(columns, beams, slabs, config);

            var filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "error.json");
            var json2 = _jsonExporter.ExportToJsonErrors(errors, filePath2);

            ViewBag.Errors = errors;
            ViewBag.Columns = columns;
            ViewBag.Beams = beams;
            ViewBag.Slabs = slabs;

            // Save JSON file
            // Create output model
            var output = new OutputModel
            {
                Columns = columns,
                Beams = beams,
                Slabs = slabs
            };
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "output.json");
            var json = _jsonExporter.ExportToJson(output, filePath);

            ViewBag.Errors = errors;
            ViewBag.Json = json;

            return View("Result");
        }


        [HttpGet]
        public IActionResult DownloadErrors()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "error.json");

            if (!System.IO.File.Exists(filePath))
                return NotFound("Validation errors file not found.");

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Return file for download
            return File(fileBytes, "application/json", "error.json");
        }
    }
}
