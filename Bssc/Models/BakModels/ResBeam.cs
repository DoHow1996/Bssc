using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResBeam
    {
        public ResBeam()
        {
            InsSuperstructures = new HashSet<InsSuperstructure>();
        }

        /// <summary>
        /// 梁唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 梁名称
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 梁类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 梁跨数
        /// </summary>
        public int? SpanNum { get; set; }
        /// <summary>
        /// 铺装厚
        /// </summary>
        public double? OverlayThickness { get; set; }
        /// <summary>
        /// 边梁墩高
        /// </summary>
        public double? SideBeamPierHeight { get; set; }
        /// <summary>
        /// 中梁墩高
        /// </summary>
        public double? MidBeamPierHeight { get; set; }
        /// <summary>
        /// 底板横坡
        /// </summary>
        public int? BottomBoardCrossSlope { get; set; }
        /// <summary>
        /// 起点墩加高
        /// </summary>
        public double? StartPointPierAddHeight { get; set; }
        /// <summary>
        /// 终点墩加高
        /// </summary>
        public double? EndPointPierAddHeight { get; set; }

        public virtual ICollection<InsSuperstructure> InsSuperstructures { get; set; }
    }
}
