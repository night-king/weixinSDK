/*--------------------------------------------------------------------------
 *https://github.com/jonathanconway/XmlToDynamic
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace Deepleo.Weixin.SDK
{
    public class DynamicElement : DynamicObject
    {
        private IDictionary<string, string> attributes = new Dictionary<string, string>();

        internal IDictionary<string, dynamic> SubElements = new Dictionary<string, dynamic>();

        public string this[string key]
        {
            get { return attributes[key]; }
            set { attributes[key] = value; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = SubElements[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SubElements[binder.Name] = value as dynamic;
            return true;
        }

        public override string ToString()
        {
            return Value;
        }

        public string Value { get; set; }
    }
    public static class XElementExtensions
    {
        private static PluralizationService pluralizationService;

        static XElementExtensions()
        {
            pluralizationService = PluralizationService.CreateService(CultureInfo.CurrentCulture);
        }

        public static dynamic ToDynamic(this XElement element)
        {
            var item = new DynamicElement();

            // Add sub-elements
            if (element.HasElements)
            {
                var uniqueElements = element.Elements().Where(el => element.Elements().Count(el2 => el2.Name.LocalName.Equals(el.Name.LocalName)) == 1);
                var repeatedElements = element.Elements().Except(uniqueElements);

                foreach (var repeatedElementGroup in repeatedElements.GroupBy(re => re.Name.LocalName).OrderBy(el => el.Key))
                {
                    var list = new List<dynamic>();
                    foreach (var repeatedElement in repeatedElementGroup)
                        list.Add(ToDynamic(repeatedElement));

                    item.SubElements
                        .Add(pluralizationService.Pluralize(repeatedElementGroup.Key), list);
                }

                foreach (var uniqueElement in uniqueElements.OrderBy(el => el.Name.LocalName))
                {
                    item.SubElements
                        .Add(uniqueElement.Name.LocalName, ToDynamic(uniqueElement));
                }
            }

            // Add attributes, if any
            if (element.Attributes().Any())
            {
                foreach (var attribute in element.Attributes())
                    item[attribute.Name.LocalName] = attribute.Value;
            }

            // Add value
            item.Value = element.Value;

            return item;
        }
    }
}
