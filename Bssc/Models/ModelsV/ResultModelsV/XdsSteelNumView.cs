using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class XdsSteelNumView
    {

        [HeaderText("支座型号")]
        public string zhizuoxinghao { get; set; }
        [HeaderText("下垫石尺寸c(mm)")]
        public double xiadianshichicunc { get; set; }
        [HeaderText("下垫石尺寸d(mm)")]
        public double xiadianshichicund { get; set; }
        [HeaderText("垫石尺寸")]
        public string dianshichicun { get; set; }
        [HeaderText("垫石个数")]
        public double dianshigeshu { get; set; }
        [HeaderText("m值")]
        public double mzhi { get; set; }
        [HeaderText("n值")]
        public double nzhi { get; set; }
        [HeaderText("1#钢筋长度(mm)")]
        public double n1gangjingchangdu { get; set; }
        [HeaderText("2#钢筋长度(mm)")]
        public double n2gangjingchangdu { get; set; }
        [HeaderText("3#钢筋长度(mm)")]
        public double n3gangjingchangdu { get; set; }
        [HeaderText("4#钢筋长度(mm)")]
        public double n4gangjingchangdu { get; set; }
        [HeaderText("单重(kg/m)")]
        public double danzhong { get; set; }
        [HeaderText("总重(kg)")]
        public double zongzhong { get; set; }

    }
}
