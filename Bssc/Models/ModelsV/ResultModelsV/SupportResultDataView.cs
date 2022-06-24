using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class SupportResultDataView
    {

        [HeaderText("墩号")]
        public string dunhao { get; set; }
        [HeaderText("支座位置")]
        public string zhizuoweizhi{ get; set; }
        [HeaderText("支座型号")]
        public string zhizuoxinghao{ get; set; }
        [HeaderText("支座高度h(mm)")]
        public double zhizuogaoduh{ get; set; }
        [HeaderText("支座上垫石尺寸a(mm)")]
        public double zhizuoshangdianshichicuna { get; set; }
        [HeaderText("支座上垫石尺寸b(mm)")]
        public double zhizuoshangdianshichicunb { get; set; }
        [HeaderText("支座上垫石尺寸c(mm)")]
        public double zhizuoshangdianshichicunc { get; set; }
        [HeaderText("支座上垫石尺寸d(mm)")]
        public double zhizuoshangdianshichicund { get; set; }
        [HeaderText("名义支座系统高度h1(mm)")]
        public double mingyizhizuoxitonggaoduh1 { get; set; }
        [HeaderText("实际支座系统高度h2(mm)")]
        public double shijizhizuoxitonggaoduh2 { get; set; }
        [HeaderText("上垫石厚度(mm)")]
        public double shangdianshihoudu { get; set; }
        [HeaderText("下垫石厚度(mm)")]
        public double xiadianshihoudu { get; set; }
        [HeaderText("垫石混凝土C50(m3)")]
        public double dianshihunningtuC50 { get; set; }


    }
}
