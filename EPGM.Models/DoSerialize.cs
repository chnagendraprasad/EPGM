using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace EPGM.Models
{
    public static class DoSerialize
    {
        /// <summary>
        /// Reconstruct an object from an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string xml)
        {
            T result;
            var ser = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }
        /// <summary>
        /// Serialize an object into an XML File
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void SerializeToXml<T>(T obj, string fileName)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var ser = new XmlSerializer(typeof(T));
            //Create a FileStream object connected to the target file   
            var fileStream = new FileStream(fileName, FileMode.Create);
            ser.Serialize(fileStream, obj, ns);
            fileStream.Close();

        }
        /// <summary>
        /// Serialize an object into an XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string SerializeToXml<T>(T obj)
        {
            // To cut down on the size of the xml being sent to the database, we'll strip out this extraneous xml.
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var output = new StringWriter(new StringBuilder());
            var ser = new XmlSerializer(typeof(T));
            ser.Serialize(output, obj, ns);
            return output.ToString();

        }
    }
}
