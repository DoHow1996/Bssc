using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Control.Tools
{
    public class GeneralTool
    {

        public static string GetDLLPath()
        {
            string str = Assembly.GetExecutingAssembly().Location;
            string[] strs = str.Split('\\');
            string path = "";
            for (int i = 0; i < strs.Length - 1; i++)
            {
                path += strs[i] + "\\";
            }
            return path;
        }

        public static double GetNum(string str)
        {
            double d = 0;
            try
            {
                d = Convert.ToDouble(str);
                return d;
            }
            catch (Exception)
            {
                return d;
            }
        }



    }
}
