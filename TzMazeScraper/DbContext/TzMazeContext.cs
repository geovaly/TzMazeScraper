using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TzMazeScraper.DbContext.Dtos;


namespace TzMazeScraper.DbContext
{
    public class TzMazeContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TzMazeContext(DbContextOptions<TzMazeContext> options)
            : base(options)
        {
        }

        public DbSet<ShowDbDto> Show { get; set; }

        public async Task DeleteAll()
        {
            await Database.ExecuteSqlCommandAsync("DELETE FROM [Show]");
        }
    }
}
