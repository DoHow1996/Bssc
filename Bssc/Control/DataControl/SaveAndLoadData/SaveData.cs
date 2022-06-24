using DynamicDataGridViewTool.ColumnAttribute;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Control.DataControl.SaveAndLoadData
{
    public class SaveData
    {

        public static void Export2Excel<T>(T t,string fileName)
        {
            Type ts = t.GetType();
            string path = GetDirectory(Assembly.GetExecutingAssembly().Location) + ts.Name + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(path)))
            {
                PropertyInfo[] propertyInfos = ts.GetProperties();
                foreach (PropertyInfo item in propertyInfos)
                {
                    HeaderTextAttribute attr = (HeaderTextAttribute)item.GetCustomAttribute(typeof(HeaderTextAttribute), true);
                    ExcelWorksheet sheet = package.Workbook.Worksheets.Add(attr.HeaderText);
                    var obj = item.GetValue(t, null) as IEnumerable<object>;
                    var list = obj.ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        Type ts1 = list[i].GetType();
                        PropertyInfo[] propertyInfos1 = ts1.GetProperties();

                        int columnIndex = 1;
                        foreach (var item1 in propertyInfos1)
                        {

                            if (i == 0)
                            {
                                HeaderTextAttribute attr1 = (HeaderTextAttribute)item.GetCustomAttribute(typeof(HeaderTextAttribute), true);
                                sheet.Cells[1, columnIndex].Value = "" + attr1.HeaderText;
                            }

                            var obj1 = item1.GetValue(list[i], null);
                            sheet.Cells[i + 2, columnIndex].Value = "" + obj1;
                            columnIndex++;
                        }
                    }
                }
                package.Save();
            }

        }
        private static string GetDirectory(string str)
        {

            string[] strs = str.Split('\\');

            string path = "";

            for (int i = 0; i < strs.Length - 1; i++)
            {
                path += strs[i] + '\\';
            }

            return path;

        }
    }
}
