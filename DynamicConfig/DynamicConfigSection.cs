using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Configuration;
using System.Xml;
using System.Reflection;

namespace DynamicConfig
{
    public class DynamicConfigSection : DynamicObject
    {
        private XmlNode xmlNode;
        private static IXmlDeserializer xmlDeserializer = new DefaultXmlDeserializer();
        private static IPluralChecker pluralChecker = new DefaultPluralChecker();
        private static Assembly assemblyWithConfigTypes;

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
                var subSection = GetSection(name);
                if (IsCollection(name)) 
                {
                    result = subSection.GetChildren();
                } 
                else 
                {
                    result = subSection;
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

        public void SetAssemblyWithConfigTypes(Assembly assembly)
        {
            assemblyWithConfigTypes = assembly;
        }

        public dynamic Value 
        {
            get
            {
                if(xmlNode.HasChildNodes)
                {
                    var typeToUse = GetTypeWithName(xmlNode.Name);

                    if (typeToUse == null)
                    {
                        return xmlNode.InnerText;
                    }

                    return xmlDeserializer.Deserialize(xmlHeaderTag + xmlNode.OuterXml, typeToUse);
                }

                return new DynamicAttribute(xmlNode.InnerText).Value;
            }
        }

        private Type GetTypeWithName(string xmlNodeName)
        {
            return assemblyWithConfigTypes.GetTypes()
                .FirstOrDefault(type => string.Compare(type.Name, xmlNodeName, ignoreCase: true) == 0);
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
                throw new ElementNotFoundException(string.Format("Subsection {0} does not exist under section {1}", elementName, xmlNode.Name));

            return new DynamicConfigSection(found);
        }

        private XmlNode FirstChild(string elementName) 
        {
            XmlNode found = null;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (string.Compare(node.Name, elementName, ignoreCase: true) == 0)
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
                throw new AttributeNotFoundException(string.Format("Attribute {0} does not exist in element {1}", attributeName, xmlNode.Name));

            return attribute.Value;
        }        
    }               
}
