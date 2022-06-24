using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class RelStandardInstanceReference
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
        /// 实例唯一编码
        /// </summary>
        public string MainInstanceId { get; set; }
        /// <summary>
        /// 实例类型
        /// </summary>
        public string MainInstanceType { get; set; }
        /// <summary>
        /// 从实例唯一编码
        /// </summary>
        public string SubInstanceId { get; set; }
        /// <summary>
        /// 从实例类型
        /// </summary>
        public string SubInstanceType { get; set; }
        /// <summary>
        /// 实例图元ID
        /// </summary>
        public string InstanceGraphicId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
