using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ProjectModelsV
{
    [Serializable]
    public class SubSModelV
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Id;
        /// <summary>
        /// 分跨线编码
        /// </summary>
        public string FkxId { get; set; }
        /// <summary>
        /// 单元编码
        /// </summary>
        public string FkxUnitId { get; set; }
        /// <summary>
        /// 桩号
        /// </summary>
        public double Mark { get; set; }
        /// <summary>
        /// 分跨线斜交角
        /// </summary>
        public double Angle { get; set; }
        /// <summary>
        /// 间距
        /// </summary>
        public double distance { get; set; }
        /// <summary>
        /// 墩布置唯一编码
        /// </summary>
        public string PierArrangeId { get; set; }
        /// <summary>
        /// 墩布置单元唯一编码
        /// </summary>
        public string PierArrangeUnitId { get; set; }
        /// <summary>
        /// 墩序号
        /// </summary>
        public string PierNum { get; set; }
        /// <summary>
        /// 墩号
        /// </summary>
        public string PierNumber { get; set; }
        /// <summary>
        /// 桥墩型号
        /// </summary>
        public string PierName { get; set; }
        /// <summary>
        /// 是否反转
        /// </summary>
        public string IsTurn { get; set; }
        /// <summary>
        /// 基础型号
        /// </summary>
        public string FoundationName { get; set; }
        /// <summary>
        /// 承台埋深
        /// </summary>
        public double CapsDepth { get; set; }
        /// <summary>
        /// 承台转角
        /// </summary>
        public double CapsAngle { get; set; }
        /// <summary>
        /// 偏心距
        /// </summary>
        public double PierEccentricDistance { get; set; }
        /// <summary>
        /// 墩斜交角
        /// </summary>
        public double PierAngle { get; set; }
        /// <summary>
        /// 地勘孔编号
        /// </summary>
        public string HoleNum { get; set; }
        /// <summary>
        /// 选择地勘孔 可不用
        /// </summary>
        public string ChooseHole { get; set; }
        /// <summary>
        /// 是否过渡墩
        /// </summary>
        public int IsTransitionalPier { get; set; }
        /// <summary>
        /// 是否辅墩
        /// </summary>
        public int IsAuxiliaryPier { get; set; }
        /// <summary>
        /// 支座系统预设高
        /// </summary>
        public double SupportSystemPredefineHeight { get; set; }
        /// <summary>
        /// 扩大基础最小标高
        /// </summary>
        public double MinimumElevationOfExpandFoundation { get; set; }
        /// <summary>
        /// 桩基持力层编号
        /// </summary>
        public string PileFoundationBearingLayerNumber { get; set; }
        /// <summary>
        /// 扩大基础第1持力层编号
        /// </summary>
        public string EnlargeBase1stHoldingLayerNumber { get; set; }
        /// <summary>
        /// 扩大基础第2持力层编号
        /// </summary>
        public string EnlargeBase2ndHoldingLayerNumber { get; set; }

    }
}
