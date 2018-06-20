using Newtonsoft.Json;
using TzMazeScraper.DbContext.Dtos;
using TzMazeScraper.Models;

namespace TzMazeScraper.Mappers
{
    public static class DbDtoMapper
    {
        public static ShowDbDto MapToShowDto(Show show)
        {
            return new ShowDbDto
            {
                Id = show.Id,
                JsonObject = JsonConvert.SerializeObject(show)
            };
        }
    }
}
