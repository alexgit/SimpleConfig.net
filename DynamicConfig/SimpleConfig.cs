using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DynamicConfig
{
    public sealed class SimpleConfig : DynamicConfiguration
    {
        public SimpleConfig(string sectionName) : base(sectionName, Assembly.GetCallingAssembly())
        {            
        }

        public SimpleConfig(string sectionName, IXmlDeserializer xmlDeserializer) : base(sectionName, Assembly.GetCallingAssembly(), xmlDeserializer)
        {                        
        }

        public SimpleConfig(string sectionName, IPluralChecker pluralChecker) : base(sectionName, Assembly.GetCallingAssembly(), pluralChecker)
        {            
        }

        public SimpleConfig(string sectionName, IXmlDeserializer xmlDeserializer, IPluralChecker pluralChecker)
            : base(sectionName, Assembly.GetCallingAssembly(), xmlDeserializer, pluralChecker)
        {
        }
    }
}
