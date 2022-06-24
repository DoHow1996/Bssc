using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResPier
    {
        public ResPier()
        {
            InsSubstructures = new HashSet<InsSubstructure>();
        }

        /// <summary>
        /// 墩唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 桥墩型号
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// 桥墩类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 是否过渡墩
        /// </summary>
        public int IsTransitionalPier { get; set;}
        /// <summary>
        /// 变高段高度
        /// </summary>
        public double? VariableHeight { get; set; }
        /// <summary>
        /// 底部横向宽
        /// </summary>
        public double? BottomTransverseWidth { get; set; }
        /// <summary>
        /// 顶部横向宽
        /// </summary>
        public double? TopTransverseWidth { get; set; }
        /// <summary>
        /// 底部纵向厚
        /// </summary>
        public double? BottomLongitudinalThickness { get; set; }
        /// <summary>
        /// 顶部纵向厚
        /// </summary>
        public double? TopLLongitudinalThickness { get; set; }
        /// <summary>
        /// 支座布置
        /// </summary>
        public string SupportArrangement { get; set; }
        /// <summary>
        /// 支座横向间距
        /// </summary>
        public double? SupportTransverseSpacing { get; set; }
        /// <summary>
        /// 墩台圆角半径
        /// </summary>
        public double? CapsAngle { get; set; }
        /// <summary>
        /// 墩台圆角半径
        /// </summary>
        public double? PierRounderCornerRadius { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public virtual ICollection<InsSubstructure> InsSubstructures { get; set; }
    }
}
