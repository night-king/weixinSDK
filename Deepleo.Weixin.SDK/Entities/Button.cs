using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deepleo.Weixin.SDK.Entities
{
    public class Button
    {
        public virtual string type
        {
            set;
            get;
        }
        public virtual string name
        {
            set;
            get;
        }
        public virtual string key
        {
            set;
            get;
        }
        public virtual string url
        {
            set;
            get;
        }
    }
}
