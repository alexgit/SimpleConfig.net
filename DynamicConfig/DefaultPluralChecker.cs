using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace DynamicConfig
{
    public class DefaultPluralChecker : IPluralChecker
    {
        private static PluralizationService ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));

        public bool IsPlural(string word)
        {
            return ps.IsPlural(word);
        }
    }
}
