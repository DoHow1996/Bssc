using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsUnitProject
    {
        /// <summary>
        /// 单位工程唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 单位工程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 单位工程编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 单位工程类型 0 road 1 bridge 3 underpass
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 单位工程创建者
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 单位工程创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 单位工程修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 单位工程起始桩号
        /// </summary>
        public double? StartMark { get; set; }
        /// <summary>
        /// 单位工程终止桩号
        /// </summary>
        public double? EndMark { get; set; }
        /// <summary>
        /// 单位工程主线中心线
        /// </summary>
        public string MainRoadRelId { get; set; }
        /// <summary>
        /// 单位工程辅道中心线
        /// </summary>
        public string AuxiliaryRoadRelId { get; set; }

        //public virtual RelRoadRef? AuxiliaryRoadRel { get; set; }
        //public virtual RelRoadRef? MainRoadRel { get; set; }
    }
}
