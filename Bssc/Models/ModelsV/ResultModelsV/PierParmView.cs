using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class PierParmView
    {

        [HeaderText("墩序号")]
        public int dunxuhao { get; set; }
        [HeaderText("墩台号")]
        public string duntaihao { get; set; }
        [HeaderText("桩号")]
        public double zhuanghao { get; set; }
        [HeaderText("桥墩型号")]
        public string qiaodunxinghao { get; set; }
        [HeaderText("基础型号")]
        public string jichuxinghao { get; set; }
        [HeaderText("是否过渡墩")]
        public double shifouguodudun { get; set; }
        [HeaderText("基础类型")]
        public string jichuleixing { get; set; }
        [HeaderText("X坐标")]
        public double xzuobiao { get; set; }
        [HeaderText("Y坐标")]
        public double yzuobiao { get; set; }
        [HeaderText("切线方位角")]
        public double qiexianfangweijiao { get; set; }
        [HeaderText("墩顶标高A")]
        public double dundingbiaogaoA { get; set; }
        [HeaderText("承台顶标高B")]
        public double chengtaidingbiaogaoB { get; set; }
        [HeaderText("承台底标高C")]
        public double chengtaidingbiaogaoC { get; set; }
        [HeaderText("基础顶标高B")]
        public double jichudingbiaogaoB { get; set; }
        [HeaderText("基础底标高C")]
        public double jichudibiaogaoC { get; set; }
        [HeaderText("立柱高度H")]
        public double lizhugaoduH { get; set; }
        [HeaderText("桩底标高D")]
        public double zhuangdibiaogaoD { get; set; }
        [HeaderText("桩长")]
        public double zhuangchang { get; set; }
        [HeaderText("横向偏心距")]
        public double hengxiangpianxinju { get; set; }
        [HeaderText("纵向偏心距")]
        public double zongxiangpianxinju { get; set; }
        [HeaderText("支座距分跨线距离a")]
        public double zhizuojufenkuaxianjuliA { get; set; }
        [HeaderText("支座距分跨线距离b")]
        public double zhizuojufenkuaxianjuliB { get; set; }
    }
}
