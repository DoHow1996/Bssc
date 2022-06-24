using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResSqx
    {
        public ResSqx()
        {
            RelRoadRefs = new HashSet<RelRoadRef>();
        }

        /// <summary>
        /// 线元唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 曲线唯一编码
        /// </summary>
        public string CurveId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 变坡点桩号
        /// </summary>
        public double? GradientChangePointMark { get; set; }
        /// <summary>
        /// 变坡点高程
        /// </summary>
        public double? GradientChangePointElevation { get; set; }
        /// <summary>
        /// 设计曲线半径
        /// </summary>
        public double? DesignCurveRadius { get; set; }
        /// <summary>
        /// 纵坡i1
        /// </summary>
        public double? LongitudinalSlopeI1 { get; set; }
        /// <summary>
        /// 纵坡i2
        /// </summary>
        public double? LongitudinalSlopeI2 { get; set; }
        /// <summary>
        /// 曲线长
        /// </summary>
        public double? CurveLength { get; set; }
        /// <summary>
        /// 切线长
        /// </summary>
        public double? TangentLength { get; set; }
        /// <summary>
        /// 外距
        /// </summary>
        public double? OuterDistance { get; set; }
        /// <summary>
        /// 直圆桩号
        /// </summary>
        public double? StraightCircleMark { get; set; }
        /// <summary>
        /// 圆直桩号
        /// </summary>
        public double? CircleStraightMark { get; set; }

        public virtual ICollection<RelRoadRef> RelRoadRefs { get; set; }
    }
}
