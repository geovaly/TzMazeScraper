using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TzMazeScraper.JsonConverters;

namespace TzMazeScraper.Models
{
    public class Cast
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(TzMazeDatetimeConverter))]
        public DateTime? Birthday { get; set; }
    }
}
