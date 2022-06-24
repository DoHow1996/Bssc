using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResDmx
    {
        public ResDmx()
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
        /// 桩号
        /// </summary>
        public double? Mark { get; set; }
        /// <summary>
        /// 高程
        /// </summary>
        public double? Elevation { get; set; }
        /// <summary>
        /// 曲线半径
        /// </summary>
        public double? Radius { get; set; }

        public virtual ICollection<RelRoadRef> RelRoadRefs { get; set; }
    }
}
