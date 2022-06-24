using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.Tools
{
    public static class DgvTool
    {

        public static void SetDgvColumnsReadOnlyColor(this DataGridView dgv)
        {
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                if (item.ReadOnly)
                {
                    item.DefaultCellStyle.BackColor = Color.DarkGray;
                }
            }
        }

        public static void SetDgvColumnReadOnlyColor(this DataGridViewColumn dgvc,Color color)
        {
            dgvc.DefaultCellStyle.BackColor = color;
        }

    }
}
