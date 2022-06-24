using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResPile
    {
       

        /// <summary>
        /// 桩唯一编码
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 桩型号名
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 桩工艺类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 桩径
        /// </summary>
        public double? Diameter { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string Material { get; set; }

    }
}
