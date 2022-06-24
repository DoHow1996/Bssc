using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.SourceModelsV
{
    [Serializable]
    public class PierSourceModelV
    {

        /// <summary>
        /// 墩唯一编码
        /// </summary>
        [HeaderText("墩唯一编码")]
        [Visible(false)]
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [HeaderText("序号")]
        public int Serial { get; set; }
        /// <summary>
        /// 桥墩型号
        /// </summary>
        [HeaderText("桥墩型号")]
        public string Designation { get; set; }
        /// <summary>
        /// 桥墩类型
        /// </summary>
        [HeaderText("桥墩类型")]
        public string Type { get; set; }
        /// <summary>
        /// 是否过渡墩
        /// </summary>
        [HeaderText("是否过渡墩")]
        public int IsTransitionalPier { get; set; }
        /// <summary>
        /// 变高段高度
        /// </summary>
        [HeaderText("变高段高度")]
        public double VariableHeight { get; set; }
        /// <summary>
        /// 底部横向宽
        /// </summary>
        [HeaderText("底部横向宽")]
        public double BottomTransverseWidth { get; set; }
        /// <summary>
        /// 顶部横向宽
        /// </summary>
        [HeaderText("顶部横向宽")]
        public double TopTransverseWidth { get; set; }
        /// <summary>
        /// 底部纵向厚
        /// </summary>
        [HeaderText("底部纵向厚")]
        public double BottomLongitudinalThickness { get; set; }
        /// <summary>
        /// 顶部纵向厚
        /// </summary>
        [HeaderText("顶部纵向厚")]
        public double TopLLongitudinalThickness { get; set; }
        /// <summary>
        /// 支座布置
        /// </summary>
        [HeaderText("支座布置")]
        public string SupportArrangement { get; set; }
        /// <summary>
        /// 支座横向间距
        /// </summary>
        [HeaderText("支座横向间距")]
        public double SupportTransverseSpacing { get; set; }
        /// <summary>
        /// 支座横向间距
        /// </summary>
        [HeaderText("支座纵向间距")]
        public double SupportLongitudinalSpacing { get; set; }
        /// <summary>
        /// 墩台圆角半径
        /// </summary>
        [HeaderText("承台转角")]
        public double CapsAngle { get; set; }
        /// <summary>
        /// 墩台圆角半径
        /// </summary>
        [HeaderText("墩台圆角半径")]
        public double PierRounderCornerRadius { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [HeaderText("备注")]
        public string Remark { get; set; }

    }
}
