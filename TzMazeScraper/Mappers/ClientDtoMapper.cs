using System.Collections.Generic;
using System.Linq;
using TzMazeScraper.Clients.Dtos;
using TzMazeScraper.Models;

namespace TzMazeScraper.Mappers
{
    public static class ClientDtoMapper
    {
        public static Show MapToShow(ShowDto showDto, IList<CastDto> castDtos)
        {
            return new Show
            {
                Id = showDto.Id,
                Name = showDto.Name,
                Cast = castDtos.Select(MapToCast)
                    .OrderByDescending(x => x.Birthday.HasValue)
                    .ThenByDescending(x => x.Birthday)
                    .ToList()
            };
        }

        private static Cast MapToCast(CastDto castDto)
        {
            return new Cast
            {
                Id = castDto.Person.Id,
                Birthday = castDto.Person.Birthday,
                Name = castDto.Person.Name
            };
        }
    }
}
