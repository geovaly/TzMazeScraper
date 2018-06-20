using System;
using Newtonsoft.Json;
using TzMazeScraper.JsonConverters;

namespace TzMazeScraper.Clients.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(TzMazeDatetimeConverter))]
        public DateTime? Birthday { get; set; }
    }
}
