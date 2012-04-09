using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Dynamic;

namespace DynamicConfig
{
    public class DynamicConfiguration : DynamicObject
    {
        private DynamicConfigSection config;

        public DynamicConfiguration(string sectionName) 
        {
            config = ConfigurationManager.GetSection(sectionName) as DynamicConfigSection;
        }

        public DynamicConfiguration(string sectionName, IXmlDeserializer xmlDeserializer) : this(sectionName)
        {            
            config.SetDeserializer(xmlDeserializer);
        }

        public DynamicConfiguration(string sectionName, IPluralChecker pluralChecker) : this(sectionName)
        {
            config.SetPluralChecker(pluralChecker);
        }

        public DynamicConfiguration(string sectionName, IXmlDeserializer xmlDeserializer, IPluralChecker pluralChecker) : this(sectionName)
        {
            config.SetDeserializer(xmlDeserializer);
            config.SetPluralChecker(pluralChecker);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return config.TryGetMember(binder, out result);
        }
    }
}
