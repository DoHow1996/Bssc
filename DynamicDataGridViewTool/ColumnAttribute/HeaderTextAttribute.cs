using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataGridViewTool.ColumnAttribute
{
    public class HeaderTextAttribute : Attribute
    {
        public string HeaderText { get; set; }
        public HeaderTextAttribute(string headerText)
        {
            this.HeaderText = headerText;
        }
    }
}
