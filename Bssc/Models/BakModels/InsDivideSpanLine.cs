using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsDivideSpanLine
    {
        /// <summary>
        /// 分跨线唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public Nullable<int> Serial { get; set; }
        /// <summary>
        /// 路线id
        /// </summary>
        public string RoadId { get; set; }
        /// <summary>
        /// 桩号
        /// </summary>
        public Nullable<double> Mark { get; set; }
        /// <summary>
        /// 分跨线斜交角
        /// </summary>
        public Nullable<double> ObliqueAngle { get; set; }
        /// <summary>
        /// 0否1是
        /// </summary>
        public Nullable<int> IsDividePart { get; set; }
    }
}
