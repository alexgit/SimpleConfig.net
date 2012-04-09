using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Configuration;
using System.ComponentModel;

namespace DynamicConfig
{
    public class DynamicAttribute : DynamicObject
    {
        private string stringValue;

        public DynamicAttribute(string stringValue)
        {
            this.stringValue = stringValue;
        }

        public override string ToString()
        {
            return stringValue;
        }

        public T As<T>() where T : struct
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter == null)
                throw new ArgumentException(string.Format("Type {0} does not appear to have a converter to turn the string into an instance.", typeof(T)));

            return (T)converter.ConvertFromString(stringValue);
        }

        public dynamic Value
        {            
            get 
            {
                int intResult;
                if (int.TryParse(stringValue, out intResult)) { return intResult; }

                long longResult;
                if (long.TryParse(stringValue, out longResult)) { return longResult; }

                double doubleResult;
                if (double.TryParse(stringValue, out doubleResult)) { return doubleResult; }

                bool boolResult;
                if (bool.TryParse(stringValue, out boolResult)) { return boolResult; }
                //TODO: replace with a bool parser that can also recongnise yes/no, on/off

                DateTime dateResult;
                if (DateTime.TryParse(stringValue, out dateResult)) { return dateResult; }

                return stringValue;
            }
        }
    }
}
