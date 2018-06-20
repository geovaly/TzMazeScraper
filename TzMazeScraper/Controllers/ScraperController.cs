using Microsoft.AspNetCore.Mvc;
using TzMazeScraper.Services;

namespace TzMazeScraper.Controllers
{
    [Route("api/[controller]")]
    public class ScraperController : Controller
    {
        private readonly ScraperRunner _scraperRunner;

        public ScraperController(ScraperRunner scraperRunner)
        {
            _scraperRunner = scraperRunner;
        }

        [HttpPost]
        [Route("run")]
        public bool Run([FromQuery] bool reset = false)
        {
            return _scraperRunner.Run(reset);
        }

        [HttpPost]
        [Route("cancel")]
        public bool Cancel()
        {
            return _scraperRunner.Cancel();
        }

        [HttpGet]
        [Route("isRunning")]
        public bool IsRunning()
        {
            return _scraperRunner.IsRunning;
        }
    }
}
