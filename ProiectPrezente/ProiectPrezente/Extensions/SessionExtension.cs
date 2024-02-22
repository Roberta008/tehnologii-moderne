using System.Text.Json;

namespace ProiectPrezentaOnline.Extensions
{
    // Clasa de extensie pentru gestionarea obiectelor în sesiunea aplicației
    public static class SessionExtensions
    {
        // Metoda pentru serializarea și setarea unui obiect în sesiune
        public static void SetObject(this ISession sesiuneCurenta, string cheieObiect, object obiectNeserializat)
        {
            // Serializarea obiectului folosind JsonSerializer
            string obiectSerializat = JsonSerializer.Serialize(value: obiectNeserializat);
            // Setarea obiectului serializat în sesiune sub o anumită cheie
            sesiuneCurenta.SetString(key: cheieObiect, value: obiectSerializat);
        }

        // Metoda pentru deserializarea și obținerea unui obiect din sesiune
        public static T? GetObject<T>(this ISession sesiuneCurenta, string cheieObiect)
        {
            // Obținerea obiectului serializat din sesiune
            string? obiectSerializat = sesiuneCurenta.GetString(key: cheieObiect);
            // Deserializarea obiectului folosind JsonSerializer și returnarea rezultatului
            return obiectSerializat == null ? default : JsonSerializer.Deserialize<T>(json: obiectSerializat);
        }
    }
}
