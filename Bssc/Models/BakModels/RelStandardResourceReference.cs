using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class RelStandardResourceReference
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// 工程id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 资源id
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResourceType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
