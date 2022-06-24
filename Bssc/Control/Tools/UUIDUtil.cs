using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Control.Tools
{
    public static class UUIDUtil
    {
        public static string[] chars = new string[] { "a", "b", "c", "d", "e", "f",
            "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s",
            "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5",
            "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I",
            "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
            "W", "X", "Y", "Z" };
        public static string Get8UUID()
        {

            ////调用Java提供的生成随机字符串的对象：32位，十六进制，中间包含-
            //string uuid = Guid.NewGuid().ToString("N");

            //string shortBuffer;
            //for (int i = 0; i < 8; i++)
            //{ //分为8组
            //    string str = uuid.Substring(i * 4, i * 4 + 4); //每组4位

            //    int x.tr(str, 16); //输出str在16进制下的表示
            //    int x = 0;
            //    int.Parse(str);
            //    shortBuffer.append(chars[x % 0x3E]); //用该16进制数取模62（十六进制表示为314（14即E）），结果作为索引取出字符
            //}
            return "";//生成8位字符
        }

        public static string Get32UUID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string GetDateTimeID()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyMMddHHmmss");
        }


    }
}
