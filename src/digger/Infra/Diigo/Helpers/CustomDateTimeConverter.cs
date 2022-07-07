using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Digger.Infra.Diigo.Helpers
{
    public class CustomDateTimeConverter : DateTimeConverterBase
    {

         private const string Format = "yyy/MM/dd HH:mm K";
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var s = reader.Value.ToString();
            DateTime result;
            //if (DateTime.TryParseExact(s, Format, CultureInfo.InvariantCulture,DateTimeStyles.None, out result))
            if (DateTime.TryParse(s,  out result))
            {
                return result;
            }

            return DateTime.Now;


        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}
