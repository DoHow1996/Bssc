using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class SupportNumView
    {
        [HeaderText("支座型号")]
        public string zhizuoxinghao{ get; set; }
        [HeaderText("总数量")]
        public int zongshuliang{ get; set; }
        [HeaderText("钢箱梁数量")]
        public int ganhgxiangliangshuliang{ get; set; }
        [HeaderText("混凝土数量")]
        public int hunningtushuliang{ get; set; }
    }
}
