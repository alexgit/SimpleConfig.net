using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Configuration;
using System.Xml;

namespace DynamicConfig
{
    public class DynamicConfigSection : DynamicObject
    {
        private XmlNode xmlNode;
        private static IXmlDeserializer xmlDeserializer = new DefaultXmlDeserializer();
        private static IPluralChecker pluralChecker = new DefaultPluralChecker();

        private const string xmlHeaderTag = @"<?xml version=""1.0"" encoding=""utf-8""?>";
        
        public DynamicConfigSection(XmlNode xmlNode) 
        {
            this.xmlNode = xmlNode;
        }
                
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;
            if (char.IsUpper(name[0]))
            {
                if (IsCollection(name)) 
                { 
                    result = GetSection(name).GetChildren(); 
                } 
                else 
                { 
                    result = GetSection(name); 
                } 
            }
            else 
            {
                result = GetAttribute(name);
            } 
            return true;
        }        

        public T As<T>() where T : class
        {
            return xmlDeserializer.Deserialize<T>(xmlHeaderTag + xmlNode.OuterXml);
        }        

        public void SetDeserializer(IXmlDeserializer des)
        {
            xmlDeserializer = des;
        }

        public void SetPluralChecker(IPluralChecker pc)
        {
            pluralChecker = pc;
        }

        private IEnumerable<DynamicConfigSection> GetChildren()
        {
            foreach (XmlNode n in xmlNode.ChildNodes)
            {
                yield return new DynamicConfigSection(n);
            }            
        }

        private bool IsCollection(string elementName)
        {
            return IsPlural(elementName) && IsCollection(FirstChild(elementName));
        }

        private bool IsCollection(XmlNode node)
        {
            if (node.FirstChild == null || node.ChildNodes.Count < 2)
                return false;

            var nameOfFirstChild = node.FirstChild.Name;
            for (int i = 1; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].Name != nameOfFirstChild)
                    return false;
            }

            return true;
        }

        private bool IsPlural(string elementName)
        {
            return pluralChecker.IsPlural(elementName);
        }        

        private DynamicConfigSection GetSection(string elementName) 
        {
            XmlNode found = FirstChild(elementName);

            if (found == null)
                throw new ArgumentException(string.Format("Subsection {0} does not exist under section {1}", elementName, xmlNode.Name));

            return new DynamicConfigSection(found);
        }

        private XmlNode FirstChild(string elementName) 
        {
            XmlNode found = null;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (string.Compare(node.Name, elementName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    found = node;
                    break;
                }
            }

            return found;
        }

        private object GetAttribute(string attributeName) 
        {
            var stringValue = GetStringValue(attributeName);

            return new DynamicAttribute(stringValue);
        }

        private string GetStringValue(string attributeName) 
        {
            var attribute = xmlNode.Attributes[attributeName];

            if (attribute == null)
                throw new ArgumentException(string.Format("Attribute {0} does not exist in element {1}", attributeName, xmlNode.Name));

            return attribute.Value;
        }        
    }               
}
