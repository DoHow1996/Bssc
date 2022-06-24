using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsExploration
    {
        /// <summary>
        /// 地勘孔唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 坐标X
        /// </summary>
        public string PointX { get; set; }
        /// <summary>
        /// 坐标Y
        /// </summary>
        public string PointY { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        public string Elevation { get; set; }
        /// <summary>
        /// 孔深
        /// </summary>
        public string HoleDepth { get; set; }
        /// <summary>
        /// 地下水稳定水位深度
        /// </summary>
        public string GroundwaterStableWaterLevelDepth { get; set; }
        /// <summary>
        /// 地下水稳定水位高程
        /// </summary>
        public string GroundwaterStableWaterLevelElevation { get; set; }
    }
}
