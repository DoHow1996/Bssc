using Bssc.Control.Tools;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.DataControl
{
    public static class SuperModelControl
    {

        public static List<SuperSModelV> GetSuperSModelVs(this List<SubSModelV> subSModelVs,TreeNode node)
        {

            List<SuperSModelV> superSModelVs = new List<SuperSModelV>();
            List<SubSModelV> subSModelVsTemp = new List<SubSModelV>();
            for (int i = 0; i < subSModelVs.Count; i++)
            {
                if (subSModelVs[i].IsTransitionalPier == 1 && subSModelVs[i].IsAuxiliaryPier == 0)
                {
                    subSModelVsTemp.Add(subSModelVs[i]);
                }
            }


            for (int i = 0; i < subSModelVsTemp.Count - 1; i++)
            {
                SuperSModelV superSModelV = new SuperSModelV();
                superSModelV.Id = node.Name;
                superSModelV.UnitId = "unitSuper" + UUIDUtil.Get32UUID();
                superSModelV.UniteNum = "第" + (i + 1) + "联";
                superSModelV.StartPierNum = subSModelVsTemp[i].PierNum;
                superSModelV.EndPierNum = subSModelVsTemp[i + 1].PierNum;
                superSModelV.SpanNum = Convert.ToInt16(subSModelVsTemp[i + 1].PierNum.Split('_')[0]) -
                    Convert.ToInt16(subSModelVsTemp[i].PierNum.Split('_')[0]);

                if (GlobalData.sourceModelV.beamSourceModelVs.Count > 0)
                {
                    superSModelV.BeamId = GlobalData.sourceModelV.beamSourceModelVs[0].Id;
                    superSModelV.BeamType = GlobalData.sourceModelV.beamSourceModelVs[0].Type;
                    superSModelV.StartBeamHeight = "" + GlobalData.sourceModelV.beamSourceModelVs[0].SideBeamPierHeight;
                    superSModelV.EndBeamHeight = "" + GlobalData.sourceModelV.beamSourceModelVs[0].SideBeamPierHeight;
                }
                

                superSModelVs.Add(superSModelV);
            }

            return superSModelVs;

        }

    }
}
