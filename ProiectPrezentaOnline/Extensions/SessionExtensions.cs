using System.Text.Json;

namespace ProiectPrezentaOnline.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession sesiuneCurenta, string cheieObiect, object obiectNeserializat)
        {
            string obiectSerializat = JsonSerializer.Serialize(obiectNeserializat);
            sesiuneCurenta.SetString(cheieObiect, obiectSerializat);
        }

        public static T? GetObject<T>(this ISession sesiuneCurenta, string cheieObiect)
        {
            string? obiectSerializat = sesiuneCurenta.GetString(cheieObiect);
            return obiectSerializat == null ? default : JsonSerializer.Deserialize<T>(obiectSerializat);
        }
    }
}
