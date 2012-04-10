using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace DynamicConfig
{
    public class DefaultXmlDeserializer : IXmlDeserializer
    {
        public T Deserialize<T>(string xml)
        {
            var xmlReader = XmlReader.Create(new StringReader(xml));
            return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
        }

        public object Deserialize(string xml, Type type) 
        {
            var xmlReader = XmlReader.Create(new StringReader(xml));
            return new XmlSerializer(type).Deserialize(xmlReader);
        }
    }
}
