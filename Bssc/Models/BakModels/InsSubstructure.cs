using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsSubstructure
    {
        /// <summary>
        /// 下部结构唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 墩号
        /// </summary>
        public string PierNum { get; set; }
        /// <summary>
        /// 墩名
        /// </summary>
        public string PierName { get; set; }
        /// <summary>
        /// 是否反转桥墩
        /// </summary>
        public int? IsTurn { get; set; }
        /// <summary>
        /// 承台埋深
        /// </summary>
        public double? CapsDepth { get; set; }
        /// <summary>
        /// 承台转角
        /// </summary>
        public double? CapsAngle { get; set; }
        /// <summary>
        /// 偏心距
        /// </summary>
        public double? EccentricDistance { get; set; }
        /// <summary>
        /// 是否过渡墩
        /// </summary>
        public int? IsTransitionalPier { get; set; }
        /// <summary>
        /// 是否辅墩
        /// </summary>
        public int? IsAuxiliaryPier { get; set; }
        /// <summary>
        /// 墩唯一编码
        /// </summary>
        public string PierResId { get; set; }
        /// <summary>
        /// 基础唯一编码
        /// </summary>
        public string FoundatiionResId { get; set; }

        //public virtual ResFoundation? FoundatiionRes { get; set; }
        //public virtual ResPier? PierRes { get; set; }
    }
}
