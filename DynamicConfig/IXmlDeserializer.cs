﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicConfig
{
    public interface IXmlDeserializer
    {
        T Deserialize<T>(string xml);

        dynamic Deserialize(string xml, Type type);
    }
}
