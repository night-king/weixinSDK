using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deepleo.Weixin.SDK.Entities
{
    public class menu
    {
        public List<Button> button { set; get; }
        public List<MenuItem> sub_button { set; get; }
    }
}
