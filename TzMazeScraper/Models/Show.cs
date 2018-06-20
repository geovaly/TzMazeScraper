using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TzMazeScraper.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Cast> Cast { get; set; }
    }
}
