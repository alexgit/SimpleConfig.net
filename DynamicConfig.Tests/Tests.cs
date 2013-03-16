using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml;

namespace DynamicConfig.Tests
{
    [TestFixture]
    public class Tests
    {
        XmlDocument doc;
        XmlNode rootNode;
        
        [SetUp]
        public void Setup() 
        {
            doc = new XmlDocument();
            rootNode = doc.CreateElement("root");
            doc.AppendChild(rootNode);
        }
  
        [Test]
        public void Constcuts() 
        {                                  
            dynamic config = null;            
            Assert.DoesNotThrow(() => { 
                config = new DynamicConfigSection(rootNode);
            });
            Assert.IsNotNull(config);
        }
        
        [Test]
        public void Check_node_exists()
        {
            rootNode.AppendChild(doc.CreateElement("article"));

            dynamic config = new DynamicConfigSection(rootNode);
            Assert.IsNotNull(config.Article);
        }

        [Test]
        public void Throws_element_not_found_exception_when_no_matching_element_exists()
        {
            rootNode.AppendChild(doc.CreateElement("article"));

            dynamic config = new DynamicConfigSection(rootNode);
            Assert.Throws<ElementNotFoundException>(() =>
            {
                var attr = config.Article.Settings;
            });
        }

        [Test]
        public void Throws_attribute_not_found_exception_when_no_matching_attribute_exists()
        {
            rootNode.AppendChild(doc.CreateElement("article"));

            dynamic config = new DynamicConfigSection(rootNode);
            Assert.Throws<AttributeNotFoundException>(() => {
                var attr = config.Article.title;
            });
        }

        [Test]
        public void Does_not_throw_argument_not_found_exception_when_attribute_exists()
        {
            var articleNode = doc.CreateElement("article");
            articleNode.Attributes.Append(doc.CreateAttribute("title"));
            rootNode.AppendChild(articleNode);
            
            dynamic config = new DynamicConfigSection(rootNode);
            Assert.DoesNotThrow(() =>
            {
                var attr = config.Article.title;
            });
        }
    }
}
