using Bssc.Models.ModelsV.SourceModelsV;
using DynamicDataGridViewTool.ColumnAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bssc.Models.ModelsV.ResultModelsV
{
    public class ResultModel
    {
        [HeaderText("下部结构")]
        public List<SubSResultView> subSResultViews { get; set; }
        [HeaderText("上部结构")]
        public List<SuperSResultView> superSResultViews { get; set; }
        [HeaderText("支座系统")]
        public List<SupportResultView> supportResultViews { get; set; }
        [HeaderText("基础")]
        public List<FoundationSourceModelV> foundationSourceModelVs { get; set; }
        [HeaderText("桥墩")]
        public List<PierSourceModelV> pierSourceModelVs { get; set; }
        [HeaderText("主梁")]
        public List<BeamSourceModelV> beamSourceModelVs { get; set; }
        [HeaderText("支座数据一览表")]
        public List<SupportResultDataView> supportResultDataViews { get; set; }
        [HeaderText("支座数量表")]
        public List<SupportNumView> supportNumViews { get; set; }
        [HeaderText("上垫石钢筋数量表")]
        public List<SdsSteelNumView> sdsSteelNumViews { get; set; }
        [HeaderText("下垫石钢筋数量表")]
        public List<XdsSteelNumView> xdsSteelNumViews { get; set; }
        [HeaderText("桩基坐标表")]
        public List<PilePositionView> pilePositionViews { get; set; }
        [HeaderText("桥墩参数一览表")]
        public List<PierParmView> pierParmViews { get; set; }
    }
}
