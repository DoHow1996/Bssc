using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Bssc.Control.CadControl;
using Bssc.Control.Tools.TreeviewContextMenu;
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Bssc.Control.DataControl.SaveAndLoadData
{
    /// <summary>
    /// 保存数据 上传数据 通过xml语言
    /// </summary>
    public static class SaveAndLoadDataByXML
    {

        

        /// <summary>
        /// 保存为xml语言
        /// </summary>
        /// <param name="tempData"></param>
        public static void SaveDataByXML(this TempData tempData,string fileName)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "xml文件(*.xml)|*.xml";

            if (file.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TempData));
                    Stream stream = new FileStream(file.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                    xmlSerializer.Serialize(stream, tempData);
                    stream.Close();
                }
                catch (Exception)
                {
                    Application.ShowAlertDialog("保存文件进程出错");
                }
            }
            
            
            
        }
        /// <summary>
        /// 读取xml
        /// </summary>
        /// <returns></returns>
        public static TempData GetTempDataByXML()
        {
             
            //try
            //{
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Title = "请选择本地数据文件";
                openFileDialog.Filter = "本地数据文件|*.xml";

                string filename = "";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                }
                else
                {
                    return null;
                }


                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TempData));
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                var tempData = (TempData)xmlSerializer.Deserialize(stream);
                return tempData;
            //}
            //catch (Exception)
            //{
            //    Application.ShowAlertDialog("读取文件进程出错");
            //}
            //return null;
        }

        public static void GenerateTreeView(this TreeView treeView,TempData tempData)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            treeView.Nodes.Clear();
            ///项目文件
            if (tempData.projectModelV != null)
            {
                TreeNode projectNode = new TreeNode()
                {
                    Name = tempData.projectModelV.Id,
                    Text = tempData.projectModelV.Name
                };
                ContextMenuWrapForProjectNode contextMenuWrapForProjectNode = new ContextMenuWrapForProjectNode(projectNode);
                treeView.Nodes.Add(projectNode);
                if (tempData.projectModelV.roadModelVs.Count > 0)
                {
                    for (int i = 0; i < tempData.projectModelV.roadModelVs.Count; i++)
                    {
                        List<RoadModelV> roadModelVs = tempData.projectModelV.roadModelVs;
                        TreeNode roadNode = new TreeNode()
                        {
                            Name = roadModelVs[i].Id,
                            Text = roadModelVs[i].Designation
                        };
                        ContextMenuWrapForRoadNode contextMenuWrapForRoadNode = new ContextMenuWrapForRoadNode(roadNode);
                        projectNode.Nodes.Add(roadNode);

                        if (roadModelVs[i].bridgeModelVs.Count > 0)
                        {
                            for (int j = 0; j < roadModelVs[i].bridgeModelVs.Count; j++)
                            {
                                List<BridgeModelV> bridgeModelVs = roadModelVs[i].bridgeModelVs;
                                TreeNode bridgeNode = new TreeNode()
                                {
                                    Name = bridgeModelVs[j].Id,
                                    Text = bridgeModelVs[j].Designation
                                };
                                ContextMenuWrapForBridgeNode contextMenuWrapForBridgeNode = new ContextMenuWrapForBridgeNode(bridgeNode);
                                roadNode.Nodes.Add(bridgeNode);

                                
                                TreeNode superNode = new TreeNode()
                                {
                                    Name = "supers" + bridgeModelVs[j].Id,
                                    Text = "上部结构"
                                };
                                //ContextMenuWrapForBridgeSturctureNode context1 = new ContextMenuWrapForBridgeSturctureNode(superNode);
                                bridgeNode.Nodes.Add(superNode);


                                
                                TreeNode supportNode = new TreeNode()
                                {
                                    Name = "supports" + bridgeModelVs[j].Id,
                                    Text = "支座系统"
                                };
                                //ContextMenuWrapForBridgeSturctureNode context2 = new ContextMenuWrapForBridgeSturctureNode(supportNode);
                                bridgeNode.Nodes.Add(supportNode);
                                

                                
                                TreeNode subNode = new TreeNode()
                                {
                                    Name = "subs" + bridgeModelVs[j].Id,
                                    Text = "下部结构"
                                };
                                //ContextMenuWrapForBridgeSturctureNode context3 = new ContextMenuWrapForBridgeSturctureNode(subNode);
                                bridgeNode.Nodes.Add(subNode);

                                TreeNode allResultNode = new TreeNode()
                                {
                                    Name = "allresult" + bridgeModelVs[j].Id,
                                    Text = "总体成果"
                                };
                                //ContextMenuWrapForBridgeSturctureNode context4 = new ContextMenuWrapForBridgeSturctureNode(allResultNode);
                                bridgeNode.Nodes.Add(allResultNode);

                            }
                        }

                    }
                }

            }




            ///资源文件
            TreeNode sourceNode = new TreeNode()
            {
                Name = "source",
                Text = "资源"
            };
            treeView.Nodes.Add(sourceNode);

            TreeNode foundationsNode = new TreeNode()
            {
                Name = "foundations",
                Text = "基础"
            };
            sourceNode.Nodes.Add(foundationsNode);
            ContextMenuWrapForResourceNode contextMenuWrapForFoundationsFoundationsNode
               = new ContextMenuWrapForResourceNode(foundationsNode);

            TreeNode piersNode = new TreeNode()
            {
                Name = "piers",
                Text = "桥墩"
            };
            sourceNode.Nodes.Add(piersNode);
            ContextMenuWrapForResourceNode contextMenuWrapForFoundationsPiersNodeNode
               = new ContextMenuWrapForResourceNode(piersNode);

            TreeNode beamsNode = new TreeNode()
            {
                Name = "beams",
                Text = "主梁"
            };
            sourceNode.Nodes.Add(beamsNode);
            ContextMenuWrapForResourceNode contextMenuWrapForFoundationsBeamsNode
               = new ContextMenuWrapForResourceNode(beamsNode);

            TreeNode roadsNode = new TreeNode()
            {
                Name = "roads",
                Text = "路线"
            };
            sourceNode.Nodes.Add(roadsNode);
            ContextMenuWrapForResourceNode contextMenuWrapForFoundationsRoadsNode
               = new ContextMenuWrapForResourceNode(roadsNode);

            List<FoundationSourceModelV> foundationSourceModelVs = tempData.sourceModelV.foundationSourceModelVs;

            for (int i = 0; i < foundationSourceModelVs.Count; i++)
            {
                TreeNode foundationNode = new TreeNode()
                {
                    Name = foundationSourceModelVs[i].Id,
                    Text = foundationSourceModelVs[i].Designation
                };
                ContextMenuWrapForSingleSourceNode contextMenuWrapForSingleSourceNode = new ContextMenuWrapForSingleSourceNode(foundationNode);
                foundationsNode.Nodes.Add(foundationNode);
                doc.CreateFoundationBlock(foundationSourceModelVs[i]);
                
            }

            List<PierSourceModelV> pierSourceModelVs = tempData.sourceModelV.pierSourceModelVs;

            for (int i = 0; i < pierSourceModelVs.Count; i++)
            {
                TreeNode pierNode = new TreeNode()
                {
                    Name = pierSourceModelVs[i].Id,
                    Text = pierSourceModelVs[i].Designation
                };
                ContextMenuWrapForSingleSourceNode contextMenuWrapForSingleSourceNode = new ContextMenuWrapForSingleSourceNode(pierNode);
                piersNode.Nodes.Add(pierNode);
                doc.CreatePierBlock(pierSourceModelVs[i]);
            }

            List<BeamSourceModelV> beamSourceModelVs = tempData.sourceModelV.beamSourceModelVs;

            for (int i = 0; i < beamSourceModelVs.Count; i++)
            {
                TreeNode beamNode = new TreeNode()
                {
                    Name = beamSourceModelVs[i].Id,
                    Text = beamSourceModelVs[i].Designation
                };
                ContextMenuWrapForSingleSourceNode contextMenuWrapForSingleSourceNode = new ContextMenuWrapForSingleSourceNode(beamNode);
                beamsNode.Nodes.Add(beamNode);
            }

            List<RoadSourceModelV> roadSourceModelVs = tempData.sourceModelV.roadSourceModelVs;

            for (int i = 0; i < roadSourceModelVs.Count; i++)
            {
                TreeNode roadNode = new TreeNode()
                {
                    Name = roadSourceModelVs[i].Id,
                    Text = roadSourceModelVs[i].Designation
                };
                ContextMenuWrapForSingleSourceNode contextMenuWrapForSingleSourceNode = new ContextMenuWrapForSingleSourceNode(roadNode);
                roadsNode.Nodes.Add(roadNode);
            }

        }

    }
}
