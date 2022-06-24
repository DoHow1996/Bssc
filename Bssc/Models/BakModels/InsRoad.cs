using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsRoad
    {
        public InsRoad()
        {
            RelRoadRefs = new HashSet<RelRoadRef>();
        }

        /// <summary>
        /// 路线唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }

        /// <summary>
        /// 路线关系表唯一编码
        /// </summary>
        public string RelRoadId { get; set; }

        /// <summary>
        /// 路线名称
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 路线编号
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 横坡
        /// </summary>
        public string CrossSlope { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    

        public virtual ICollection<RelRoadRef> RelRoadRefs { get; set; }
    }
}
