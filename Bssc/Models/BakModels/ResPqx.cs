using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResPqx
    {
        public ResPqx()
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
        /// 线元序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 线形标识
        /// </summary>
        public double UnitType { get; set; }
        /// <summary>
        /// 起点桩号
        /// </summary>
        public double StartMark { get; set; }
        /// <summary>
        /// 终点桩号
        /// </summary>
        public double EndMark { get; set; }
        /// <summary>
        /// 起点坐标X
        /// </summary>
        public double StartX { get; set; }
        /// <summary>
        /// 起点坐标Y
        /// </summary>
        public double StartY { get; set; }
        /// <summary>
        /// 起点方位角
        /// </summary>
        public double StartAngle { get; set; }
        /// <summary>
        /// 起点半径
        /// </summary>
        public double StartRadius { get; set; }
        /// <summary>
        /// 终点半径
        /// </summary>
        public double EndRadius { get; set; }
        /// <summary>
        /// 转向
        /// </summary>
        public double UnitTurn { get; set; }
        /// <summary>
        /// 长度L
        /// </summary>
        public double UnitLength { get; set; }
        /// <summary>
        /// 回旋曲线A
        /// </summary>
        public double UnitA { get; set; }

        public virtual ICollection<RelRoadRef> RelRoadRefs { get; set; }
    }
}
