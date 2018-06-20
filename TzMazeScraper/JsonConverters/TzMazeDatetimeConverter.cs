using Newtonsoft.Json.Converters;

namespace TzMazeScraper.JsonConverters
{
    public class TzMazeDatetimeConverter : IsoDateTimeConverter
    {
        public TzMazeDatetimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
