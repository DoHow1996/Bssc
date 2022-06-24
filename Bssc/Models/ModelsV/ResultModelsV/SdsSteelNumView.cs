using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class SdsSteelNumView
    {

        [HeaderText("支座型号")]
        public string zhizuoxinghao{ get; set; }
        [HeaderText("上垫石尺寸a(mm)")]
        public double shangdianshichicuna{ get; set; }
        [HeaderText("上垫石尺寸b(mm)")]
        public double shangdianshichicunb{ get; set; }
        [HeaderText("上座板尺寸")]
        public string shangzuobanchicun{ get; set; }
        [HeaderText("上座板个数")]
        public double shangzuobangeshu{ get; set; }
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
