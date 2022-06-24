using BSSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Bssc.Models.ModelsV.SourceModelsV;

namespace Bssc.Control.Tools
{
    public static class TreeNodeTool
    {
        /// <summary>
        /// 根据节点名找到相应节点
        /// </summary>
        /// <param name="Nodes"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static TreeNode GetTreeNodeByName(this TreeNodeCollection Nodes, string Name)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Name.Contains(Name))
                {
                    return Nodes[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 根据节点名找到相应节点
        /// </summary>
        /// <param name="Nodes"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static TreeNode GetTreeNodeByText(this TreeNodeCollection Nodes, string Name)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Text.Contains(Name))
                {
                    return Nodes[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="id">对应的唯一编码</param>
        /// <param name="sequence">
        /// i = 0,基础
        /// i = 1,桥墩
        /// i = 2,主梁
        /// i = 3,桩
        /// </param>
        /// <returns></returns>
        public static object getRes(this TreeView treeView,string id,int sequence)
        {
            TreeNodeCollection nodes = treeView.Nodes[1].Nodes[sequence].Nodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Name == id)
                {
                    switch (sequence)
                    {
                        case 0:
                            return (ResFoundation)nodes[i].Tag;
                            break;
                        case 1:
                            return (ResPier)nodes[i].Tag;
                            break;
                        case 2:
                            return (BeamSourceModelV)nodes[i].Tag;
                            break;
                        case 3:
                            return (ResPile)nodes[i].Tag;
                            break;
                        default:
                            break;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="node">
        /// openStatus = 0 为父节点
        /// openstatus = 1 为当前子节点
        /// </param>
        /// <param name="name">名称</param>
        /// <param name="openStatus">
        /// openStatus = 0 为新建状态
        /// openStatus = 1 为修改状态
        /// </param>
        /// <returns></returns>
        //public static bool hasName(this TreeNode node,string name,int openStatus)
        //{
        //    TreeNodeCollection nodes = null;
        //    if (openStatus == 0)
        //    {
        //        nodes = node.Nodes;
        //    }
        //    else if (openStatus == 1)
        //    {
        //        nodes = node.Parent.Nodes;
        //    }
            

        //    if (nodes.Count <= 0)
        //    {
        //        return true;
        //    }


        //    for (int i = 0; i < nodes.Count; i++)
        //    {

        //        string typeStr = nodes[i].Tag.GetType().ToString();
        //        switch (typeStr)
        //        {
        //            case "BSSC.Models.ResPile":
        //                ResPile resPile = (ResPile)nodes[i].Tag;
        //                if (openStatus == 0)
        //                {
        //                    if (resPile.Designation == name)
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else if (openStatus == 1)
        //                {
        //                    if (resPile.Designation == name && nodes[i].Name != node.Name)
        //                    {
        //                        return false;
        //                    }
        //                }
                        
        //                break;
        //            case "BSSC.Models.ResPier":
        //                ResPier resPier = (ResPier)nodes[i].Tag;
        //                if (openStatus == 0)
        //                {
        //                    if (resPier.Designation == name)
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else if (openStatus == 1)
        //                {
        //                    if (resPier.Designation == name && nodes[i].Name != node.Name)
        //                    {
        //                        return false;
        //                    }

        //                }
                        
        //                break;
        //            case "BSSC.Models.ResFoundation":
        //                ResFoundation resFoundation = (ResFoundation)nodes[i].Tag;
        //                if (openStatus == 0)
        //                {
        //                    if (resFoundation.Designation == name)
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else if (openStatus == 1)
        //                {
        //                    if (resFoundation.Designation == name && nodes[i].Name != node.Name)
        //                    {
        //                        return false;
        //                    }
        //                }
                        
        //                break;
        //            case "BSSC.Models.BeamSourceModelV":
        //                BeamSourceModelV beamSourceModelV = (BeamSourceModelV)nodes[i].Tag;
        //                if (openStatus == 0)
        //                {
        //                    if (beamSourceModelV.Designation == name)
        //                    {
        //                        return false;
        //                    }
        //                }
        //                else if (openStatus == 1)
        //                {
        //                    if (beamSourceModelV.Designation == name && nodes[i].Name != node.Name)
        //                    {
        //                        return false;
        //                    }
        //                }
                        
        //                break;

        //            default:
        //                break;
        //        }
                
        //    }
        //    return true;
        //}


    }
}
