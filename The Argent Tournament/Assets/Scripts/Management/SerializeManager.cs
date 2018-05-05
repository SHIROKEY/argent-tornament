using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Management
{
    public static class SerializeManager
    {
        public static string Serialize<T>(this T record)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            xml.Serialize(writer, record);
            return writer.ToString();
        }

        public static T Deserialize<T>(this string record)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(record);
            return (T)xml.Deserialize(reader);
        }
    }
}
