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
    public class BeamSourceModelV
    {
        /// <summary>
        /// 梁唯一编码
        /// </summary>
        [HeaderText("梁唯一编码")]
        [Visible(false)]
        public string Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [HeaderText("序号")]
        public int Serial { get; set; }
        /// <summary>
        /// 梁名称
        /// </summary>
        [HeaderText("梁名称")]
        public string Designation { get; set; }
        /// <summary>
        /// 梁类型
        /// </summary>
        [HeaderText("梁类型")]
        public string Type { get; set; }
        /// <summary>
        /// 梁跨数
        /// </summary>
        [HeaderText("梁跨数")]
        public int SpanNum { get; set; }
        /// <summary>
        /// 铺装厚
        /// </summary>
        [HeaderText("铺装厚")]
        public double OverlayThickness { get; set; }
        /// <summary>
        /// 边梁墩高
        /// </summary>
        [HeaderText("边梁墩高")]
        public double SideBeamPierHeight { get; set; }
        /// <summary>
        /// 中梁墩高
        /// </summary>
        [HeaderText("中梁墩高")]
        public double MidBeamPierHeight { get; set; }
        /// <summary>
        /// 底板横坡
        /// </summary>
        [HeaderText("底板横坡")]
        public int BottomBoardCrossSlope { get; set; }
        /// <summary>
        /// 起点墩加高
        /// </summary>
        [HeaderText("起点墩加高")]
        public double StartPointPierAddHeight { get; set; }
        /// <summary>
        /// 终点墩加高
        /// </summary>
        [HeaderText("终点墩加高")]
        public double EndPointPierAddHeight { get; set; }
    }
}
