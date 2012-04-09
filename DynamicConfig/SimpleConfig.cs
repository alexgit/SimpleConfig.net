using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicConfig
{
    public sealed class SimpleConfig : DynamicConfiguration
    {
        public SimpleConfig(string sectionName) : base(sectionName)
        {            
        }

        public SimpleConfig(string sectionName, IXmlDeserializer xmlDeserializer) : base(sectionName, xmlDeserializer)
        {                        
        }

        public SimpleConfig(string sectionName, IPluralChecker pluralChecker) : base(sectionName, pluralChecker)
        {            
        }

        public SimpleConfig(string sectionName, IXmlDeserializer xmlDeserializer, IPluralChecker pluralChecker)
            : base(sectionName, xmlDeserializer, pluralChecker)
        {            
        }
    }
}
