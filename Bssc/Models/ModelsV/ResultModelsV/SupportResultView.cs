using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class SupportResultView
    {
        [HeaderText("墩台号")] [Frozen(true)] public string duntaihao{ get; set; }
        [HeaderText("位置")] [Frozen(true)] public int weizhi{ get; set; }
        [HeaderText("联序号")] [Frozen(true)] public string lianxuhao{ get; set; }
        [HeaderText("墩序号")] [Frozen(true)] public int dongxuhao{ get; set; }
        [HeaderText("支座系统预设高")] public double zhizuoxitongyushegao{ get; set; }
        [HeaderText("支座与道路中心线距离")] public double zhzuoyudaoluzhongxinxianjuli{ get; set; }
        [HeaderText("支座距分划线距离")] public double zhzuojufenhuaxianjuli{ get; set; }
        [HeaderText("上垫石厚度")] public double shangdianshihoudong{ get; set; }
        [HeaderText("上垫石最小包络长a")] public double shangdianshizuixiaobaoluochanga{ get; set; }
        [HeaderText("上垫石最小包络宽b")] public double shangdianshizuixiaobaoluokuanb{ get; set; }
        [HeaderText("支反力恒载")] public double zhifanlihengzai{ get; set; }
        [HeaderText("最大标准组合反力")] public double zuidabiaozhunzuhefanli{ get; set; }
        [HeaderText("最小标准组合反力")] public double zuixiaobiaozhunzuhefanli{ get; set; }
        [HeaderText("拟选支座承载力")] public double nixuanzhzuochengzaili{ get; set; }
        [HeaderText("固定方向")] public string gudingfangxiang{ get; set; }
        [HeaderText("支座型号")] public string zhzuoxinghao{ get; set; }
        [HeaderText("道路中心线设计高")] public double daoluzhongxinxianshejigao{ get; set; }
        [HeaderText("铺装厚")] public double puzhuanghou{ get; set; }
        [HeaderText("梁高")] public double lianggao{ get; set; }
        [HeaderText("梁底横坡（边线比中心线低为+）")] public double liangdihengpo{ get; set; }
        [HeaderText("纵坡")] public double zongpo{ get; set; }
        [HeaderText("横向偏心距e")] public double hengxiangpianxinjue{ get; set; }
        [HeaderText("纵向偏心距f")] public double zongxiangpianxinjuf{ get; set; }
        [HeaderText("支座处梁底高差")] public double zhizuochuliangdigaocha{ get; set; }
        [HeaderText("支座处最大高差")] public double zhizuochuzuidagaocha{ get; set; }
        [HeaderText("支座系统高度")] public double zhizuoxitonggaodong{ get; set; }
        [HeaderText("支座高度mm")] public double zhizuogaodongmm{ get; set; }
        [HeaderText("支座下垫石厚度")] public double zhizuoxiadianshihoudong{ get; set; }
        [HeaderText("是否固定墩")] public double shifougudingdong{ get; set; }
        [HeaderText("上垫石尺寸(a×b)")] public string shangdianshichicunab{ get; set; }
        [HeaderText("下垫石尺寸(c)")] public double xiadianshichicunc{ get; set; }
        [HeaderText("下垫石尺寸(c×c)")] public string xiadianshichicuncc{ get; set; }
        [HeaderText("支座所需桥墩厚度")] public double zhizuosuoxuqiaodonghoudong{ get; set; }
        [HeaderText("桥墩顺桥向厚度")] public double qiaodongshunqiaoxianghoudong{ get; set; }
        [HeaderText("桥墩宽度是否满足需要")] public string qiaodongkuandongshifoumanzuxuyao{ get; set; }


    }
}
