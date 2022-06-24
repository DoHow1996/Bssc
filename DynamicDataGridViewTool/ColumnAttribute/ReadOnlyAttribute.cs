using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataGridViewTool.ColumnAttribute
{
    public class ReadOnlyAttribute : Attribute
    {
        public bool IsReadOnly { get; set; }
        public ReadOnlyAttribute(bool isReadOnly)
        {
            this.IsReadOnly = isReadOnly;
        }
    }
}
