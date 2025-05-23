using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YoutubeDownloaderMvc.Models;
using YoutubeDownloaderMvc.Service;

namespace YoutubeDownloaderMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public async Task<IActionResult> Download(DownloadModel model)
        {
            var outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");
            var service = new YoutubeService();
            var fileName = await service.DownloadAudioAsync(model.VideoUrl, outputFolder);
            model.FileName = fileName;
            return View("Success", model);
        }

        public IActionResult GetFile(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Downloads", fileName);
            if (!System.IO.File.Exists(path)) return NotFound();
            return File(System.IO.File.ReadAllBytes(path), "application/octet-stream", fileName);
        }
    }
}
