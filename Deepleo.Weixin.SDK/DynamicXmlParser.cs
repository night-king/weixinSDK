/*--------------------------------------------------------------------------
 * http://blogs.msdn.com/b/mcsuksoldev/archive/2010/02/04/dynamic-xml-reader-with-c-and-net-4-0.aspx
*--------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Dynamic;

namespace Deepleo.Weixin.SDK
{
    public class DynamicXmlParser : DynamicObject
    {
        XElement element;
        public DynamicXmlParser(string filename)
        {
            element = XElement.Load(filename);
        }

        public DynamicXmlParser(XElement el)
        {
            element = el;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (element == null)
            {
                result = null;
                return false;
            }
            XElement sub = element.Element(binder.Name);
            if (sub == null)
            {
                result = null;
                return false;
            }
            else
            {
                result = new DynamicXmlParser(sub);
                return true;
            }
        }

        public override string ToString()
        {
            return element != null ? element.Value : string.Empty;
        }

        public string this[string attr]
        {
            get
            {
                if (element == null) return string.Empty;
                return element.Attribute(attr).Value;
            }
        }
    }
}
