using Bssc.Models.ModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.DataControl
{
    public static class DataExamine
    {

        public static bool hasName(this TreeNode node,string name,int openStatus)
        {
            if (node.Name.Contains("foundation"))
            {
                var foundationSourceModelV = GlobalData.sourceModelV.foundationSourceModelVs.Where(aa => aa.Designation == name).FirstOrDefault();
                if (openStatus == 0)
                {
                    if (foundationSourceModelV != null)
                    {
                        return false;
                    }
                }
                else if (openStatus == 1)
                {
                    if (foundationSourceModelV != null && foundationSourceModelV.Id != node.Name)
                    {
                        return false;
                    }
                }
            }
            else if (node.Name.Contains("beam"))
            {
                var beamSourceModelV = GlobalData.sourceModelV.beamSourceModelVs.Where(aa => aa.Designation == name).FirstOrDefault();
                if (openStatus == 0)
                {
                    if (beamSourceModelV != null)
                    {
                        return false;
                    }
                }
                else if (openStatus == 1)
                {
                    if (beamSourceModelV != null && beamSourceModelV.Id != node.Name)
                    {
                        return false;
                    }
                }
            }
            else if (node.Name.Contains("pier"))
            {
                var pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Designation == name).FirstOrDefault();
                if (openStatus == 0)
                {
                    if (pierSourceModelV != null)
                    {
                        return false;
                    }
                }
                else if (openStatus == 1)
                {
                    if (pierSourceModelV != null && pierSourceModelV.Id != node.Name)
                    {
                        return false;
                    }
                }
            }
            else if (node.Name.Contains("roadline"))
            {
                var roadlineSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Designation == name).FirstOrDefault();
                if (openStatus == 0)
                {
                    if (roadlineSourceModelV != null)
                    {
                        return false;
                    }
                }
                else if (openStatus == 1)
                {
                    if (roadlineSourceModelV != null && roadlineSourceModelV.Id != node.Name)
                    {
                        return false;
                    }
                }
            }
            else if (node.Name.Contains("exploration"))
            {
                var roadlineSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Designation == name).FirstOrDefault();
                if (openStatus == 0)
                {
                    if (roadlineSourceModelV != null)
                    {
                        return false;
                    }
                }
                else if (openStatus == 1)
                {
                    if (roadlineSourceModelV != null && roadlineSourceModelV.Id != node.Name)
                    {
                        return false;
                    }
                }
            }


            return true;
        }

    }
}
