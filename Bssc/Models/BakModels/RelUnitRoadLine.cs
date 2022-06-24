using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class RelUnitRoadLine
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
        /// 主线唯一编码
        /// </summary>
        public string MainRoadRelId { get; set; }
        /// <summary>
        /// 辅道唯一编码
        /// </summary>
        public string AuxiliarRoadRelId { get; set; }

        //public virtual RelRoadRef? AuxiliarRoadRel { get; set; }
        //public virtual RelRoadRef? MainRoadRel { get; set; }
    }
}
