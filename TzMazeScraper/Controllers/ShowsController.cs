using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TzMazeScraper.DbContext;
using TzMazeScraper.Models;
using TzMazeScraper.Settings;
using TzMazeScraper.Utils;

namespace TzMazeScraper.Controllers
{
    [Route("api/[controller]")]
    public class ShowsController : Controller
    {
        private readonly TzMazeContext _dbContext;
        private readonly PageSettings _pageSettings;

        public ShowsController(TzMazeContext dbContext, IOptions<PageSettings> pageOptions)
        {
            _dbContext = dbContext;
            _pageSettings = pageOptions.Value;
        }

        [HttpGet]
        public async Task<IList<Show>> Get([FromQuery]int page = PagingUtil.FirstPageNumber)
        {
            if (page < PagingUtil.FirstPageNumber)
            {
                return new Show[0];
            }

            var showDbDtos = await _dbContext.Show
                    .Page(_pageSettings.PageSize, page)
                    .AsNoTracking()
                    .ToListAsync();

            var result = showDbDtos
                .Select(x => JsonConvert.DeserializeObject<Show>(x.JsonObject))
                .ToList();

            return result;
        }

    }
}
