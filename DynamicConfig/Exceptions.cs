using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicConfig
{
    public class AttributeNotFoundException : Exception 
    {
        public AttributeNotFoundException(string message) : base(message) 
        { 
        }
    }

    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(string message) :  base(message)
        {
        }
    }
}
