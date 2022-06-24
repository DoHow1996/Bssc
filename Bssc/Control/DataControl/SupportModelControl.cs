using Bssc.Control.Tools;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.DataControl
{
    public static  class SupportModelControl
    {

        public static List<SupportSModelV> GetSupportSModelVs(this List<SubSModelV> subSModelVs,TreeNode node)
        {

            List<SupportSModelV> supportSModelVs = new List<SupportSModelV>();
            int index = 0;

            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var superSModelVs = bridgeModelV.superSModelVs;

            TreeNode superNode = node.Parent.Nodes.GetTreeNodeByText("上部结构");
            superSModelVs = bridgeModelV.subSModelVs.GetSuperSModelVs(superNode);


            for (int i = 0; i < subSModelVs.Count; i++)
            {
                SubSModelV subSModelV = subSModelVs[i];
                double pierNum = Convert.ToDouble(subSModelV.PierNum.Split('_')[0]);
                for (int j = 0; j < superSModelVs.Count; j++)
                {
                    double startPierNum = Convert.ToDouble(superSModelVs[j].StartPierNum.Split('_')[0]);
                    double endPierNum = Convert.ToDouble(superSModelVs[j].EndPierNum.Split('_')[0]);

                    if (pierNum >= startPierNum && pierNum <= endPierNum)
                    {
                        ///如果是辅墩 都是SX约束
                        if (subSModelV.IsAuxiliaryPier == 1)
                        {
                            if (subSModelV.IsTransitionalPier == 0)
                            {
                                PierSourceModelV pierSourceModelV =
                               GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                for (int m = 0; m < supportNum; m++)
                                {
                                    SupportSModelV supportSModelV = new SupportSModelV();
                                    supportSModelV.index = ++index;
                                    supportSModelV.Id = node.Name;
                                    supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                    supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                    supportSModelV.PierNum = subSModelV.PierNum;
                                    supportSModelV.Position = m + 1;
                                    ///要根据不同的支座类型对支座的约束进行描述
                                    supportSModelV.LimitDirection = "SX";
                                    supportSModelVs.Add(supportSModelV);
                                }
                            }
                            ///如果是该联起始墩分跨线上的支座
                            if (pierNum == Convert.ToDouble(superSModelVs[j].StartPierNum.Split('_')[0]))
                            {
                                PierSourceModelV pierSourceModelV =
                                   GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                int allSupportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                int maxSupportNum = pierSourceModelV.SupportArrangement.GetMaxMarkSupportNum(subSModelV.IsTurn);
                                int minSupportNum = pierSourceModelV.SupportArrangement.GetMinMarkSupportNum(subSModelV.IsTurn);
                                for (int m = 0; m < maxSupportNum; m++)
                                {
                                    SupportSModelV supportSModelV = new SupportSModelV();
                                    supportSModelV.index = ++index;
                                    supportSModelV.Id = node.Name;
                                    supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                    supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                    supportSModelV.PierNum = subSModelV.PierNum;
                                    supportSModelV.Position = minSupportNum + m + 1;
                                    ///要根据不同的支座类型对支座的约束进行描述
                                    supportSModelV.LimitDirection = "SX";
                                    supportSModelVs.Add(supportSModelV);
                                }
                            }
                            ///如果是该联终止墩分跨线上的支座
                            else if (pierNum == Convert.ToDouble(superSModelVs[j].EndPierNum.Split('_')[0]))
                            {
                                PierSourceModelV pierSourceModelV =
                                   GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                int allSupportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                int minSupportNum = pierSourceModelV.SupportArrangement.GetMinMarkSupportNum(subSModelV.IsTurn);
                                for (int m = 0; m < minSupportNum; m++)
                                {
                                    SupportSModelV supportSModelV = new SupportSModelV();
                                    supportSModelV.index = ++index;
                                    supportSModelV.Id = node.Name;
                                    supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                    supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                    supportSModelV.PierNum = subSModelV.PierNum;
                                    supportSModelV.Position = m + 1;
                                    supportSModelV.LimitDirection = "SX";
                                    supportSModelVs.Add(supportSModelV);
                                    ///要根据不同的支座类型对支座的约束进行描述
                                }

                            }
                        }
                        else
                        {
                            #region 可能要修改
                            //if (subSModelV.IsTransitionalPier == 0)
                            //{
                            //    PierSourceModelV pierSourceModelV =
                            //    GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                            //    int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                            //    for (int m = 0; m < supportNum; m++)
                            //    {
                            //        SupportSModelV supportSModelV = new SupportSModelV();
                            //        supportSModelV.index = ++index;
                            //        supportSModelV.Id = node.Name;
                            //        supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                            //        supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                            //        supportSModelV.PierNum = subSModelV.PierNum;
                            //        supportSModelV.Position = m + 1;
                            //        ///要根据不同的支座类型对支座的约束进行描述
                            //        supportSModelV.LimitDirection = GetNotTransPierSupportConstraint(pierSourceModelV.SupportArrangement, m, subSModelV.IsTurn);
                            //        supportSModelVs.Add(supportSModelV);
                            //    }
                            //}
                            /////如果是首联起始墩的分跨线上的支座
                            //else if (pierNum == Convert.ToDouble(superSModelVs.First().StartPierNum.Split('_')[0]))
                            //{
                            //    PierSourceModelV pierSourceModelV =
                            //    GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                            //    int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                            //    for (int m = 0; m < supportNum; m++)
                            //    {
                            //        SupportSModelV supportSModelV = new SupportSModelV();
                            //        supportSModelV.index = ++index;
                            //        supportSModelV.Id = node.Name;
                            //        supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                            //        supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                            //        supportSModelV.PierNum = subSModelV.PierNum;
                            //        supportSModelV.Position = m + 1;
                            //        ///要根据不同的支座类型对支座的约束进行描述
                            //        #region 支座的约束
                            //        switch (pierSourceModelV.SupportArrangement)
                            //        {
                            //            case "单支座":
                            //                supportSModelV.LimitDirection = "DX";
                            //                break;
                            //            case "1排2列":
                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }
                            //                else
                            //                {
                            //                    supportSModelV.LimitDirection = "SX";
                            //                }

                            //                break;
                            //            case "2排1列":

                            //                supportSModelV.LimitDirection = "DX";

                            //                break;
                            //            case "2排2列":
                            //                if (m == 0 || m == 2)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }
                            //                else
                            //                {
                            //                    supportSModelV.LimitDirection = "SX";
                            //                }
                            //                break;
                            //            case "品字形":
                            //                if (subSModelV.IsTurn == "是")
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                    else if (m == 1)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                    else if (m == 2)
                            //                    {
                            //                        supportSModelV.LimitDirection = "SX";
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                    else if (m == 1)
                            //                    {
                            //                        supportSModelV.LimitDirection = "SX";
                            //                    }
                            //                    else if (m == 2)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                }
                            //                break;
                            //            default:
                            //                break;
                            //        }
                            //        #endregion
                            //        supportSModelVs.Add(supportSModelV);
                            //    }
                            //}
                            /////如果是尾联终止墩的分跨线上的支座
                            //else if (pierNum == Convert.ToDouble(superSModelVs.Last().EndPierNum.Split('_')[0]))
                            //{
                            //    PierSourceModelV pierSourceModelV =
                            //       GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                            //    int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                            //    for (int m = 0; m < supportNum; m++)
                            //    {
                            //        SupportSModelV supportSModelV = new SupportSModelV();
                            //        supportSModelV.index = ++index;
                            //        supportSModelV.Id = node.Name;
                            //        supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                            //        supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                            //        supportSModelV.PierNum = subSModelV.PierNum;
                            //        supportSModelV.Position = m + 1;
                            //        ///要根据不同的支座类型对支座的约束进行描述
                            //        #region 支座的约束
                            //        switch (pierSourceModelV.SupportArrangement)
                            //        {
                            //            case "单支座":
                            //                supportSModelV.LimitDirection = "GD";
                            //                break;
                            //            case "1排2列":
                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "GD";
                            //                }
                            //                else
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }

                            //                break;
                            //            case "2排1列":

                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "GD";
                            //                }
                            //                else
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }

                            //                break;
                            //            case "2排2列":
                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "GD";
                            //                }
                            //                else if (m == 1)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }
                            //                else if (m == 2)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }
                            //                else if (m == 3)
                            //                {
                            //                    supportSModelV.LimitDirection = "SX";
                            //                }
                            //                break;
                            //            case "品字形":
                            //                if (subSModelV.IsTurn == "是")
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "GD";
                            //                    }
                            //                    else if (m == 1)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                    else if (m == 2)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "GD";
                            //                    }
                            //                    else if (m == 1)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                    else if (m == 2)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                }
                            //                break;
                            //            default:
                            //                break;
                            //        }
                            //        #endregion
                            //        supportSModelVs.Add(supportSModelV);
                            //    }
                            //}
                            /////如果是该联起始墩分跨线上的支座
                            //else if (pierNum == Convert.ToDouble(superSModelVs[j].StartPierNum.Split('_')[0]))
                            //{
                            //    PierSourceModelV pierSourceModelV =
                            //       GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                            //    int allSupportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                            //    int maxSupportNum = pierSourceModelV.SupportArrangement.GetMaxMarkSupportNum(subSModelV.IsTurn);
                            //    int minSupportNum = pierSourceModelV.SupportArrangement.GetMinMarkSupportNum(subSModelV.IsTurn);
                            //    for (int m = 0; m < maxSupportNum; m++)
                            //    {
                            //        SupportSModelV supportSModelV = new SupportSModelV();
                            //        supportSModelV.index = ++index;
                            //        supportSModelV.Id = node.Name;
                            //        supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                            //        supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                            //        supportSModelV.PierNum = subSModelV.PierNum;
                            //        supportSModelV.Position = minSupportNum + m + 1;
                            //        ///要根据不同的支座类型对支座的约束进行描述
                            //        #region 支座的约束
                            //        switch (pierSourceModelV.SupportArrangement)
                            //        {
                            //            case "单支座":
                            //                break;
                            //            case "1排2列":
                            //                break;
                            //            case "2排1列":

                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }

                            //                break;
                            //            case "2排2列":
                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }
                            //                else if (m == 1)
                            //                {
                            //                    supportSModelV.LimitDirection = "SX";
                            //                }
                            //                break;
                            //            case "品字形":
                            //                if (subSModelV.IsTurn == "是")
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                    else if (m == 1)
                            //                    {
                            //                        supportSModelV.LimitDirection = "SX";
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                }
                            //                break;
                            //            default:
                            //                break;
                            //        }
                            //        #endregion
                            //        supportSModelVs.Add(supportSModelV);
                            //    }
                            //}
                            /////如果是该联终止墩分跨线上的支座
                            //else if (pierNum == Convert.ToDouble(superSModelVs[j].EndPierNum.Split('_')[0]))
                            //{
                            //    PierSourceModelV pierSourceModelV =
                            //       GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                            //    int allSupportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                            //    int minSupportNum = pierSourceModelV.SupportArrangement.GetMinMarkSupportNum(subSModelV.IsTurn);
                            //    for (int m = 0; m < minSupportNum; m++)
                            //    {
                            //        SupportSModelV supportSModelV = new SupportSModelV();
                            //        supportSModelV.index = ++index;
                            //        supportSModelV.Id = node.Name;
                            //        supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                            //        supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                            //        supportSModelV.PierNum = subSModelV.PierNum;
                            //        supportSModelV.Position = m + 1;
                            //        ///要根据不同的支座类型对支座的约束进行描述
                            //        #region 支座的约束
                            //        switch (pierSourceModelV.SupportArrangement)
                            //        {
                            //            case "单支座":
                            //                supportSModelV.LimitDirection = "GD";
                            //                break;
                            //            case "1排2列":
                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "GD";
                            //                }
                            //                else if (m == 1)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }
                            //                break;
                            //            case "2排1列":

                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "GD";
                            //                }

                            //                break;
                            //            case "2排2列":
                            //                if (m == 0)
                            //                {
                            //                    supportSModelV.LimitDirection = "GD";
                            //                }
                            //                else if (m == 1)
                            //                {
                            //                    supportSModelV.LimitDirection = "DX";
                            //                }
                            //                break;
                            //            case "品字形":
                            //                if (subSModelV.IsTurn == "是")
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "GD";
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    if (m == 0)
                            //                    {
                            //                        supportSModelV.LimitDirection = "GD";
                            //                    }
                            //                    else if (m == 1)
                            //                    {
                            //                        supportSModelV.LimitDirection = "DX";
                            //                    }
                            //                }
                            //                break;
                            //            default:
                            //                break;
                            //        }
                            //        #endregion
                            //        supportSModelVs.Add(supportSModelV);
                            //    }
                            //}
                            #endregion

                            if (i > 0)
                            {
                                ///如果这个墩的上一个墩是过渡墩  
                                ///固定支座在这个墩上面
                                if (subSModelVs[i - 1].IsTransitionalPier == 1)
                                {
                                    PierSourceModelV pierSourceModelV =
                                GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                    int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                    for (int m = 0; m < supportNum; m++)
                                    {
                                        SupportSModelV supportSModelV = new SupportSModelV();
                                        supportSModelV.index = ++index;
                                        supportSModelV.Id = node.Name;
                                        supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                        supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                        supportSModelV.PierNum = subSModelV.PierNum;
                                        supportSModelV.Position = m + 1;
                                        ///要根据不同的支座类型对支座的约束进行描述
                                        #region 支座的约束
                                        switch (pierSourceModelV.SupportArrangement)
                                        {
                                            case "单支座":
                                                supportSModelV.LimitDirection = "GD";
                                                break;
                                            case "1排2列":
                                                if (m == 0)
                                                {
                                                    supportSModelV.LimitDirection = "GD";
                                                }
                                                else
                                                {
                                                    supportSModelV.LimitDirection = "DX";
                                                }

                                                break;
                                            case "2排1列":

                                                if (m == 0)
                                                {
                                                    supportSModelV.LimitDirection = "GD";
                                                }
                                                if (m == 1)
                                                {
                                                    supportSModelV.LimitDirection = "DX";
                                                }

                                                break;
                                            case "2排2列":
                                                if (m == 0 )
                                                {
                                                    supportSModelV.LimitDirection = "GD";
                                                }
                                                if (m == 2)
                                                {
                                                    supportSModelV.LimitDirection = "DX";
                                                }
                                                if (m == 1)
                                                {
                                                    supportSModelV.LimitDirection = "DX";
                                                }
                                                else
                                                {
                                                    supportSModelV.LimitDirection = "SX";
                                                }
                                                break;
                                            case "品字形":
                                                if (subSModelV.IsTurn == "是")
                                                {
                                                    if (m == 0)
                                                    {
                                                        supportSModelV.LimitDirection = "GD";
                                                    }
                                                    else if (m == 1)
                                                    {
                                                        supportSModelV.LimitDirection = "DX";
                                                    }
                                                    else if (m == 2)
                                                    {
                                                        supportSModelV.LimitDirection = "DX";
                                                    }
                                                }
                                                else
                                                {
                                                    if (m == 0)
                                                    {
                                                        supportSModelV.LimitDirection = "GD";
                                                    }
                                                    else if (m == 1)
                                                    {
                                                        supportSModelV.LimitDirection = "DX";
                                                    }
                                                    else if (m == 2)
                                                    {
                                                        supportSModelV.LimitDirection = "DX";
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                        #endregion
                                        supportSModelVs.Add(supportSModelV);
                                    }
                                }
                                else
                                {
                                    //如果这个墩是过渡墩 并且不是最后一个墩
                                    if (subSModelV.IsTransitionalPier == 1 && pierNum != Convert.ToDouble(superSModelVs.Last().EndPierNum.Split('_')[0]))
                                    {
                                        ///如果是该联起始墩分跨线上的支座
                                        if (pierNum == Convert.ToDouble(superSModelVs[j].StartPierNum.Split('_')[0]))
                                        {
                                            PierSourceModelV pierSourceModelV =
                                               GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                            int allSupportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                            int maxSupportNum = pierSourceModelV.SupportArrangement.GetMaxMarkSupportNum(subSModelV.IsTurn);
                                            int minSupportNum = pierSourceModelV.SupportArrangement.GetMinMarkSupportNum(subSModelV.IsTurn);
                                            for (int m = 0; m < maxSupportNum; m++)
                                            {
                                                SupportSModelV supportSModelV = new SupportSModelV();
                                                supportSModelV.index = ++index;
                                                supportSModelV.Id = node.Name;
                                                supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                                supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                                supportSModelV.PierNum = subSModelV.PierNum;
                                                supportSModelV.Position = minSupportNum + m + 1;
                                                ///要根据不同的支座类型对支座的约束进行描述
                                                #region 支座的约束
                                                switch (pierSourceModelV.SupportArrangement)
                                                {
                                                    case "单支座":
                                                        break;
                                                    case "1排2列":
                                                        break;
                                                    case "2排1列":

                                                        if (m == 0)
                                                        {
                                                            supportSModelV.LimitDirection = "DX";
                                                        }
                                                        break;
                                                    case "2排2列":
                                                        if (m == 0)
                                                        {
                                                            supportSModelV.LimitDirection = "DX";
                                                        }
                                                        else if (m == 1)
                                                        {
                                                            supportSModelV.LimitDirection = "SX";
                                                        }
                                                        break;
                                                    case "品字形":
                                                        if (subSModelV.IsTurn == "是")
                                                        {
                                                            if (m == 0)
                                                            {
                                                                supportSModelV.LimitDirection = "DX";
                                                            }
                                                            else if (m == 1)
                                                            {
                                                                supportSModelV.LimitDirection = "SX";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (m == 0)
                                                            {
                                                                supportSModelV.LimitDirection = "DX";
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                #endregion
                                                supportSModelVs.Add(supportSModelV);
                                            }
                                        }
                                        ///如果是该联终止墩分跨线上的支座
                                        else if (pierNum == Convert.ToDouble(superSModelVs[j].EndPierNum.Split('_')[0]))
                                        {
                                            PierSourceModelV pierSourceModelV =
                                               GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                            int allSupportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                            int minSupportNum = pierSourceModelV.SupportArrangement.GetMinMarkSupportNum(subSModelV.IsTurn);
                                            for (int m = 0; m < minSupportNum; m++)
                                            {
                                                SupportSModelV supportSModelV = new SupportSModelV();
                                                supportSModelV.index = ++index;
                                                supportSModelV.Id = node.Name;
                                                supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                                supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                                supportSModelV.PierNum = subSModelV.PierNum;
                                                supportSModelV.Position = m + 1;
                                                ///要根据不同的支座类型对支座的约束进行描述
                                                #region 支座的约束
                                                switch (pierSourceModelV.SupportArrangement)
                                                {
                                                    case "单支座":
                                                        supportSModelV.LimitDirection = "DX";
                                                        break;
                                                    case "1排2列":
                                                        if (m == 0)
                                                        {
                                                            supportSModelV.LimitDirection = "DX";
                                                        }
                                                        else if (m == 1)
                                                        {
                                                            supportSModelV.LimitDirection = "SX";
                                                        }
                                                        break;
                                                    case "2排1列":

                                                        if (m == 0)
                                                        {
                                                            supportSModelV.LimitDirection = "DX";
                                                        }

                                                        break;
                                                    case "2排2列":
                                                        if (m == 0)
                                                        {
                                                            supportSModelV.LimitDirection = "DX";
                                                        }
                                                        else if (m == 1)
                                                        {
                                                            supportSModelV.LimitDirection = "SX";
                                                        }
                                                        break;
                                                    case "品字形":
                                                        if (subSModelV.IsTurn == "是")
                                                        {
                                                            if (m == 0)
                                                            {
                                                                supportSModelV.LimitDirection = "DX";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (m == 0)
                                                            {
                                                                supportSModelV.LimitDirection = "DX";
                                                            }
                                                            else if (m == 1)
                                                            {
                                                                supportSModelV.LimitDirection = "SX";
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                #endregion
                                                supportSModelVs.Add(supportSModelV);
                                            }
                                        }

                                    }
                                    else if (subSModelV.IsTransitionalPier == 1 && pierNum == Convert.ToDouble(superSModelVs.Last().EndPierNum.Split('_')[0]))
                                    {
                                        PierSourceModelV pierSourceModelV =
                               GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                        int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                        for (int m = 0; m < supportNum; m++)
                                        {
                                            SupportSModelV supportSModelV = new SupportSModelV();
                                            supportSModelV.index = ++index;
                                            supportSModelV.Id = node.Name;
                                            supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                            supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                            supportSModelV.PierNum = subSModelV.PierNum;
                                            supportSModelV.Position = m + 1;
                                            ///要根据不同的支座类型对支座的约束进行描述
                                            supportSModelV.LimitDirection = GetNotTransPierSupportConstraint(pierSourceModelV.SupportArrangement, m, subSModelV.IsTurn);
                                            supportSModelVs.Add(supportSModelV);
                                        }
                                    }
                                    //如果这个墩不是过渡墩
                                    else
                                    {
                                        PierSourceModelV pierSourceModelV =
                               GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                        int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                        for (int m = 0; m < supportNum; m++)
                                        {
                                            SupportSModelV supportSModelV = new SupportSModelV();
                                            supportSModelV.index = ++index;
                                            supportSModelV.Id = node.Name;
                                            supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                            supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                            supportSModelV.PierNum = subSModelV.PierNum;
                                            supportSModelV.Position = m + 1;
                                            ///要根据不同的支座类型对支座的约束进行描述
                                            supportSModelV.LimitDirection = GetNotTransPierSupportConstraint(pierSourceModelV.SupportArrangement, m, subSModelV.IsTurn);
                                            supportSModelVs.Add(supportSModelV);
                                        }
                                    }
                                }
                            }
                            //第一个墩
                            else
                            {
                                PierSourceModelV pierSourceModelV =
                               GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault();
                                int supportNum = pierSourceModelV.SupportArrangement.GetSupportNum();
                                for (int m = 0; m < supportNum; m++)
                                {
                                    SupportSModelV supportSModelV = new SupportSModelV();
                                    supportSModelV.index = ++index;
                                    supportSModelV.Id = node.Name;
                                    supportSModelV.UnitId = "UnitSupport" + UUIDUtil.Get32UUID();
                                    supportSModelV.ChainNum = superSModelVs[j].UniteNum;
                                    supportSModelV.PierNum = subSModelV.PierNum;
                                    supportSModelV.Position = m + 1;
                                    ///要根据不同的支座类型对支座的约束进行描述
                                    supportSModelV.LimitDirection = GetNotTransPierSupportConstraint(pierSourceModelV.SupportArrangement, m, subSModelV.IsTurn);
                                    supportSModelVs.Add(supportSModelV);
                                }
                            }
                            

                        }
                        

                    }

                }
            }

            supportSModelVs = supportSModelVs.OrderBy(aa => aa.ChainNum.Replace('第', ' ').Replace('联', ' ').Trim())
                .ThenBy(aa => Convert.ToDouble(aa.PierNum.Split('_')[0]))
                .ThenBy(aa => Convert.ToDouble(aa.PierNum.Split('_')[1]))
                .ToList();
            

            return supportSModelVs;
        }

        private static int GetSupportNum(this string str)
        {
            switch (str)
            {
                case "单支座":
                    return 1;
                case "1排2列":
                    return 2;
                case "2排1列":
                    return 2;
                case "2排2列":
                    return 4;
                case "品字形":
                    return 3;
                default:
                    return 1;
            }
        }

        private static int GetMinMarkSupportNum(this string str,string isturn = "否")
        {
            switch (str)
            {
                case "单支座":
                    return 1;
                case "1排2列":
                    return 2;
                case "2排1列":
                    return 1;
                case "2排2列":
                    return 2;
                case "品字形":
                    if (isturn == "是")
                    {
                        return 2;
                    }
                    else if (isturn == "否")
                    {
                        return 1;
                    }
                    else
                    {
                        return 1;
                    }
                    
                default:
                    return 1;
            }
        }
        /// <summary>
        /// 获取大桩号方向的支座数量
        /// </summary>
        /// <param name="str">支座类型</param>
        /// <param name="isturn">是否翻转</param>
        /// <returns></returns>
        private static int GetMaxMarkSupportNum(this string str, string isturn = "否")
        {
            switch (str)
            {
                case "单支座":
                    return 0;
                case "1排2列":
                    return 0;
                case "2排1列":
                    return 1;
                case "2排2列":
                    return 2;
                case "品字形":
                    if (isturn == "是")
                    {
                        return 1;
                    }
                    else if (isturn == "否")
                    {
                        return 2;
                    }
                    else
                    {
                        return 2;
                    }

                default:
                    return 1;
            }
        }

        /// <summary>
        /// 获取非过渡墩的支座约束
        /// </summary>
        /// <param name="str">支座类型</param>
        /// <param name="num"></param>
        /// <returns></returns>
        private static string GetNotTransPierSupportConstraint(this string str,int num,string isTurn = "否")
        {
            switch (str)
            {
                case "单支座":
                    return "DX";
                case "2排1列":
                    return "DX";
                case "1排2列":
                    if (num == 0)
                    {
                        return "DX";
                    }
                    else if (num == 1)
                    {
                        return "SX";
                    }
                    break;
                case "品字形":
                    if (isTurn == "是")
                    {
                        switch (num)
                        {
                            case 0:
                                return "DX";
                            case 1:
                                return "DX";
                                case 2:
                                return "SX";
                            default:
                                break;
                        }

                    }
                    else
                    {
                        switch (num)
                        {
                            case 0:
                                return "DX";
                            case 1:
                                return "SX";
                            case 2:
                                return "DX";
                            default:
                                break;
                        }
                    }
                    break;
                case "2排2列":
                    switch (num)
                    {
                        case 0:
                            return "DX";
                        case 1:
                            return "SX";
                        case 2:
                            return "DX";
                        case 3:
                            return "SX";
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            return "DX";
        }
    }
}
