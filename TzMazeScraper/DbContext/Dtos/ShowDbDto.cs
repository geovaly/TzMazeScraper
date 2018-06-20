using System.ComponentModel.DataAnnotations;

namespace TzMazeScraper.DbContext.Dtos
{
    public class ShowDbDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string JsonObject { get; set; }
    }
}
