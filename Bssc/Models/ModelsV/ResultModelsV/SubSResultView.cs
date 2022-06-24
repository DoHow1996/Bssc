using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class SubSResultView
    {

        [HeaderText("墩序号")] [Frozen(true)] public int dongxuhao{ get; set; }
        [HeaderText("墩台号")] [Frozen(true)] public string dongtaihao{ get; set; }
        [HeaderText("桩号")] [Frozen(true)] public double zhuanghao{ get; set; }
        [HeaderText("跨径")] public double kuajing{ get; set; }
        [HeaderText("立柱型号")] public string lizhuxinghao{ get; set; }
        [HeaderText("是否反向")] public string shifoufanxiang{ get; set; }
        [HeaderText("基础型号")] public string jichuxinghao{ get; set; }
        [HeaderText("支座系统预设高度")] public double zhizuoxitongyushegaodong{ get; set; }
        [HeaderText("承台最小埋深标准值")] public double chengtaizuixiaomaishenbiaozhunzhi{ get; set; }
        [HeaderText("承台转角α")] public double chengtaizhuanjiao{ get; set; }
        [HeaderText("斜交角β")] public double xiejiaojiao{ get; set; }
        [HeaderText("钻孔编号")] public string zuankongbianhao{ get; set; }
        [HeaderText("是否辅助墩")] public double shifoufuzhudong{ get; set; }
        [HeaderText("是否固定墩")] public double shifougudingdong{ get; set; }
        [HeaderText("梁顶横坡-左（%）")] public double liangdinghengpozuo{ get; set; }
        [HeaderText("梁顶横坡-右（%）")] public double liangdinghengpoyou{ get; set; }
        [HeaderText("梁底横坡-左（%）")] public double liangdihengpozuo{ get; set; }
        [HeaderText("梁底横坡-右（%）")] public double liangdihengpoyou{ get; set; }
        [HeaderText("地面横坡-左（%）")] public double dimianhengpozuo{ get; set; }
        [HeaderText("地面横坡-右（%）")] public double dimianhengpoyou{ get; set; }
        [HeaderText("扩大基础最小覆土深度")] public double kuodajichuzuixiaofutushendong{ get; set; }
        [HeaderText("扩大基础入持力层最小值")] public double kuodajichuruchilicengzuixiaozhi{ get; set; }
        [HeaderText("扩大基础最小标高")] public double kuodajichuzuixiaobiaogao{ get; set; }
        [HeaderText("桩基持力层编号")] public string zhuangjichilicengbianhao{ get; set; }
        [HeaderText("扩大基础第1持力层编号")] public string kuodajichudi1chilicengbianhao{ get; set; }
        [HeaderText("扩大基础第2持力层编号")] public string kuodajichudi2chilicengbianhao{ get; set; }
        [HeaderText("基础类型")]	public string jichuleixing{ get; set; }
        [HeaderText("是否过渡墩")] public double shifouguodongdong{ get; set; }
        [HeaderText("桥墩中心桩号")] public double qiaodongzhongxinzhuanghao{ get; set; }
        [HeaderText("X坐标")] public double Xzuobiao{ get; set; }
        [HeaderText("Y坐标")] public double Yzuobiao{ get; set; }
        [HeaderText("道路设计线处桥面高程")] public double daolushejixianchuqiaomiangaocheng{ get; set; }
        [HeaderText("切向纵坡（%）")] public double qiexiangzongpo{ get; set; }
        [HeaderText("设计地面高")] public double shejidimiangao{ get; set; }
        [HeaderText("切线方位角")] public double qiexianfangweijiao{ get; set; }
        [HeaderText("墩顶标高A")] public double dongdingbiaogaoA{ get; set; }
        [HeaderText("横向偏心距e")] public double hengxiangpianxinjue{ get; set; }
        [HeaderText("纵向偏心距f")] public double zongxiangpianxinjuf{ get; set; }
        [HeaderText("立柱厚度")] public double lizhuhoudong{ get; set; }
        [HeaderText("承台埋深计算高度")] public double chengtaimaishenjisuangaodong{ get; set; }
        [HeaderText("立柱计算高度")] public double lizhujisuangaodong{ get; set; }
        [HeaderText("立柱采用高度H")] public double lizhucaiyonggaodongH{ get; set; }
        [HeaderText("承台顶标高B")] public double chengtaidingbiaogaoB{ get; set; }
        [HeaderText("承台/桥台盖梁厚度")] public double chengtai_qiaotaigailianghoudong{ get; set; }
        [HeaderText("承台底标高C")] public double chengtaidibiaogaoC{ get; set; }
        [HeaderText("桩基持力层顶面标高")] public double zhuangjichilicengdingmianbiaogao{ get; set; }
        [HeaderText("桩径")] public double zhuangjing{ get; set; }
        [HeaderText("入持力层深度")] public double ruchilicengshendong{ get; set; }
        [HeaderText("初定桩底标高")] public double chudingzhuangdibiaogao{ get; set; }
        [HeaderText("初定桩长")] public double chudingzhuangchang{ get; set; }
        [HeaderText("实设桩长")] public double shishezhuangchang{ get; set; }
        [HeaderText("桩底标高D")] public double zhuangdibiaogaoD{ get; set; }
        [HeaderText("扩基第1持力层顶面标高")] public double kuojidi1chilicengdingmianbiaogao{ get; set; }
        [HeaderText("扩基第2持力层顶面标高")] public double kuojidi2chilicengdingmianbiaogao{ get; set; }
        [HeaderText("扩基持力层标高1")] public double kuojichilicengbiaogao1{ get; set; }
        [HeaderText("扩基持力层标高2")] public double kuojichilicengbiaogao2{ get; set; }
        [HeaderText("基础厚度")] public double jichuhoudong{ get; set; }
        [HeaderText("扩基基底标高（按持力层）")] public double kuojijidibiaogao_anchiliceng{ get; set; }
        [HeaderText("扩基基底标高（按覆土）")] public double kuojijidibiaogao_anfutu{ get; set; }
        [HeaderText("初算基底标高")] public double chusuanjidibiaogao{ get; set; }
        [HeaderText("进入第二持力层时基底标高")] public double jinrudidongchilicengshijidibiaogao{ get; set; }
        [HeaderText("基顶标高")] public double jidingbiaogao{ get; set; }
        [HeaderText("墩柱计算高度")] public double dongzhujisuangaodong{ get; set; }
        [HeaderText("立柱采用高度H1")] public double lizhucaiyonggaodongH1{ get; set; }
        [HeaderText("基础顶标高B")] public double jichudingbiaogaoB{ get; set; }
        [HeaderText("基础底标高C")] public double jichudibiaogaoC{ get; set; }


    }
}
