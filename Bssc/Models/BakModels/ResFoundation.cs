using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResFoundation
    {
        public ResFoundation()
        {
            InsSubstructures = new HashSet<InsSubstructure>();
        }

        /// <summary>
        /// 基础唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 基础型号名
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 基础类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 承台厚度
        /// </summary>
        public double? CapsThickness { get; set; }
        /// <summary>
        /// 桩型号
        /// </summary>
        public string PileResId { get; set; }
        /// <summary>
        /// 承台截面   格式 2,2;-2,2;-2,-2;2,-2;
        /// </summary>
        public string CapsSectionPoints { get; set; }
        /// <summary>
        /// 桩基布置   格式 2,2;-2,2;-2,-2;2,-2;
        /// </summary>
        public string PileCenterPoints { get; set; }

        //public virtual InsPile? PileRes { get; set; }
        public virtual ICollection<InsSubstructure> InsSubstructures { get; set; }
    }
}
