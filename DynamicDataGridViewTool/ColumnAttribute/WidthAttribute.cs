using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataGridViewTool.ColumnAttribute
{
    /// <summary>
    /// 如果width设置为0，则根据字符串的长度来设置列宽
    /// </summary>
    
    public class WidthAttribute : Attribute
    {
        public int Width { get; set; }
        public WidthAttribute(int width)
        {
            this.Width = width;
        }
    }
}
