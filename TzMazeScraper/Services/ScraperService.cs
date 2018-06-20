using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TzMazeScraper.Clients;
using TzMazeScraper.Clients.Dtos;
using TzMazeScraper.DbContext;
using TzMazeScraper.Mappers;
using TzMazeScraper.Models;

namespace TzMazeScraper.Services
{
    public class ScraperService
    {
        private readonly TzMazeClient _client;
        private readonly TzMazeContext _dbContext;

        public ScraperService(TzMazeClient client, TzMazeContext dbContext)
        {
            _client = client;
            _dbContext = dbContext;
        }

        public async Task Run(bool reset, CancellationToken cancellationToken, Action<Show> showAddedAction = null)
        {
            if (reset)
            {
                await _dbContext.DeleteAll();
            }

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _dbContext.ChangeTracker.LazyLoadingEnabled = false;

            var page = TzMazeClient.FirstPageNumber;

            while (true)
            {
                var showDtos = await _client.ListShows(page, cancellationToken);
                if (!showDtos.Any()) break;
                foreach (var showDto in showDtos)
                {
                    await HandleShowDto(showDto, reset, cancellationToken, showAddedAction);
                }

                page++;
            }
        }

        private async Task HandleShowDto(
            ShowDto showDto,
            bool reset,
            CancellationToken cancellationToken,
            Action<Show> showAddedAction)
        {
            var addShow = reset || await _dbContext.Show.AllAsync(x => x.Id != showDto.Id, cancellationToken);
            if (!addShow) return;
            var castDtos = await _client.ListCast(showDto.Id, cancellationToken);
            var show = ClientDtoMapper.MapToShow(showDto, castDtos);
            var showDbDto = DbDtoMapper.MapToShowDto(show);
            _dbContext.Show.Add(showDbDto);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _dbContext.Entry(showDbDto).State = EntityState.Detached;
            showAddedAction?.Invoke(show);
        }
    }
}
