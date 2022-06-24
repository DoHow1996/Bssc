using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsSoilLayer
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 每个单元的id
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 地层特征唯一编码
        /// </summary>
        public string SoilCharacterUnitId { get; set; }
        /// <summary>
        /// 地层类别 0土层 1岩层
        /// </summary>
        public string SoilLayerType { get; set; }
        /// <summary>
        /// 层顶埋深
        /// </summary>
        public string TopLayerDepth { get; set; }
        /// <summary>
        /// 层顶高程
        /// </summary>
        public string TopLayerElevation { get; set; }
        /// <summary>
        /// 层底埋深
        /// </summary>
        public string BottomLayerDepth { get; set; }
        /// <summary>
        /// 层底高程
        /// </summary>
        public string BottomLayerElevation { get; set; }
        /// <summary>
        /// 层厚
        /// </summary>
        public string LayerThickness { get; set; }
        /// <summary>
        /// 取芯率
        /// </summary>
        public string CoringRate { get; set; }
        /// <summary>
        /// RQD
        /// </summary>
        public string Rqd { get; set; }
    }
}
