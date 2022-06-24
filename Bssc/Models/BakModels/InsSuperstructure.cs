using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class InsSuperstructure
    {
        /// <summary>
        /// 上部结构唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 联号
        /// </summary>
        public string ChainNum { get; set; }
        /// <summary>
        /// 起始墩号
        /// </summary>
        public string StrartPierNum { get; set; }
        /// <summary>
        /// 终止墩号
        /// </summary>
        public string EndPierNum { get; set; }
        /// <summary>
        /// 跨数
        /// </summary>
        public int? SpanNum { get; set; }
        /// <summary>
        /// 主梁id
        /// </summary>
        public string BeamResId { get; set; } 

        public virtual BeamSourceModelV BeamRes { get; set; } 
    }
}
