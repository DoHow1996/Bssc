using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataGridViewTool.ColumnAttribute
{
    
    public class FrozenAttribute : Attribute
    {
        public bool IsFreeze { get; set; }
        public FrozenAttribute(bool isFreeze)
        {
            this.IsFreeze = isFreeze;
        }
    }
}
