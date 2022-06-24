using System;
using System.Collections.Generic;

namespace BSSC.Models
{
    public partial class ResSupport
    {
        public ResSupport()
        {
            InsSupports = new HashSet<InsSupport>();
        }

        /// <summary>
        /// 支座唯一编码
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 序号
        /// </summary>
        public int? Serial { get; set; }
        /// <summary>
        /// 支座型号
        /// </summary>
        public string SuppoerName { get; set; }
        /// <summary>
        /// 支座高度
        /// </summary>
        public double? SupportHeight { get; set; }
        /// <summary>
        /// 上顶板长A1
        /// </summary>
        public double? TopBoardLengthA1 { get; set; }
        /// <summary>
        /// 上顶板宽A
        /// </summary>
        public double? TopBoardWidthA { get; set; }
        /// <summary>
        /// 钢板长A1
        /// </summary>
        public double? SteelBoardLengthA1 { get; set; }
        /// <summary>
        /// 钢板宽A
        /// </summary>
        public double? SteelBoardWidthA { get; set; }
        /// <summary>
        /// 上顶板长A1 Z50
        /// </summary>
        public double? TopBoardLengthA1Z50 { get; set; }
        /// <summary>
        /// 上顶板长A1 Z100
        /// </summary>
        public double? TopBoardLengthA1Z100 { get; set; }
        /// <summary>
        /// 上顶板长A1 Z150
        /// </summary>
        public double? TopBoardLengthA1Z150 { get; set; }
        /// <summary>
        /// 横向位移H
        /// </summary>
        public double? TransverseDisplacement { get; set; }

        public virtual ICollection<InsSupport> InsSupports { get; set; }
    }
}
