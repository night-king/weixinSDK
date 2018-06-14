using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deepleo.Weixin.SDK.Helpers
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 将Datetime转换成时间戳（1970-1-1 00:00:00至target的总秒数）
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime target)
        {
            return (target.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
