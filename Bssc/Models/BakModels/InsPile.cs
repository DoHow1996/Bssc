using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsPile
    {
        public InsPile()
        {
            ResFoundations = new HashSet<ResFoundation>();
        }

        /// <summary>
        /// 桩基实例唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// 桩长
        /// </summary>
        public double? Length { get; set; }
        /// <summary>
        /// 坐标x
        /// </summary>
        public double? X { get; set; }
        /// <summary>
        /// 坐标y
        /// </summary>
        public double? Y { get; set; }
        /// <summary>
        /// 桩基体积
        /// </summary>
        public double? V { get; set; }
        /// <summary>
        /// 桩资源唯一编码
        /// </summary>
        public string PileResId { get; set; }

        //public virtual ResPile? PileRes { get; set; }
        public virtual ICollection<ResFoundation> ResFoundations { get; set; }
    }
}
