using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class RelRoadRef
    {
        public RelRoadRef()
        {
            InsUnitProjectAuxiliaryRoadRels = new HashSet<InsUnitProject>();
            InsUnitProjectMainRoadRels = new HashSet<InsUnitProject>();
            RelUnitRoadLineAuxiliarRoadRels = new HashSet<RelUnitRoadLine>();
            RelUnitRoadLineMainRoadRels = new HashSet<RelUnitRoadLine>();
        }

        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 路线唯一编码
        /// </summary>
        public string RoadInsId { get; set; }
        /// <summary>
        /// 平曲线唯一编码
        /// </summary>
        public string PqxResId { get; set; }
        /// <summary>
        /// 竖曲线唯一编码
        /// </summary>
        public string SqxResId { get; set; }
        /// <summary>
        /// 地面线唯一编码
        /// </summary>
        public string DmxResId { get; set; }

        /// <summary>
        /// 平曲线名
        /// </summary>
        public string pqxFileName;
        /// <summary>
        /// 竖曲线名
        /// </summary>
        public string sqxFileName;
        /// <summary>
        /// 地面线名
        /// </summary>
        public string dmxFileName;
        /// <summary>
        /// 超高线名
        /// </summary>
        public string cgxFileName;

        //public virtual ResDmx? DmxRes { get; set; }
        //public virtual ResPqx? PqxRes { get; set; }
        //public virtual InsRoad? RoadIns { get; set; }
        //public virtual ResSqx? SqxRes { get; set; }
        public virtual ICollection<InsUnitProject> InsUnitProjectAuxiliaryRoadRels { get; set; }
        public virtual ICollection<InsUnitProject> InsUnitProjectMainRoadRels { get; set; }
        public virtual ICollection<RelUnitRoadLine> RelUnitRoadLineAuxiliarRoadRels { get; set; }
        public virtual ICollection<RelUnitRoadLine> RelUnitRoadLineMainRoadRels { get; set; }
    }
}
