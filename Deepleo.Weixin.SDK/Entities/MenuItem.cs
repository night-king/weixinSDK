using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deepleo.Weixin.SDK.Entities
{
    public class MenuItem
    {
        public virtual string name
        {
            set;
            get;
        }
        public List<Button> sub_button
        {
            set;
            get;
        }
    }
}
