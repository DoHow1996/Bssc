using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataGridViewTool.ColumnAttribute
{
    public class VisibleAttribute : Attribute

    {
        public VisibleAttribute(bool isVisible)
        {
            this.IsVisible = isVisible;
        }

        public bool IsVisible { get; set; }

    }
}
