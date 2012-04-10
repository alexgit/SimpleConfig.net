using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Dynamic;
using System.Reflection;

namespace DynamicConfig
{
    public class DynamicConfiguration : DynamicObject
    {
        private DynamicConfigSection config;
        
        public DynamicConfiguration(string sectionName, Assembly configuringAssembly) 
        {
            config = ConfigurationManager.GetSection(sectionName) as DynamicConfigSection;
            config.SetAssemblyWithConfigTypes(configuringAssembly);
        }

        public DynamicConfiguration(string sectionName, Assembly configuringAssembly, IXmlDeserializer xmlDeserializer) : this(sectionName, configuringAssembly)
        {            
            config.SetDeserializer(xmlDeserializer);
        }

        public DynamicConfiguration(string sectionName, Assembly configuringAssembly, IPluralChecker pluralChecker) : this(sectionName, configuringAssembly)
        {
            config.SetPluralChecker(pluralChecker);
        }

        public DynamicConfiguration(string sectionName, Assembly configuringAssembly, IXmlDeserializer xmlDeserializer, IPluralChecker pluralChecker) : this(sectionName, configuringAssembly)
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
