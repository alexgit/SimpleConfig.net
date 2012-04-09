using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace DynamicConfig
{
    public class DynamicConfigSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section) 
        {
            return new DynamicConfigSection(section);
        }
    }
}
