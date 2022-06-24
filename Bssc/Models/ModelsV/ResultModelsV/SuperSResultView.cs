using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class SuperSResultView
    {
        [HeaderText("联号")]
        public string ChainNum { get; set; }
        [HeaderText("起点墩号")]
        public string StartPierNum { get; set; }
        [HeaderText("终点墩号")]
        public string EndPierNum { get; set; }
        [HeaderText("跨数")]
        public int SpanNum { get; set; }
        [HeaderText("主梁型号")]
        public string Designation { get; set; }
        [HeaderText("主梁类型")]
        public string ComponentClass { get; set; }
        [HeaderText("铺装厚度")]
        public double OverlayThickness { get; set; }
        [HeaderText("边墩梁高")]
        public double SideBeamPierHeight { get; set; }
        [HeaderText("中墩梁高")]
        public double MidBeamPierHeight { get; set; }
        [HeaderText("起点墩加高")]
        public double StartPointPierAddHeight { get; set; }
        [HeaderText("终点墩加高")]
        public double EndPointPierAddHeight { get; set; }
        [HeaderText("起点墩梁高")]
        public double StrartPierBeamHeight { get; set; }
        [HeaderText("终点墩梁高")]
        public double EndPierBeamHeight { get; set; }
        [HeaderText("底板坡度")]
        public int BottomBoardCrossSlope { get; set; }
    }
}
