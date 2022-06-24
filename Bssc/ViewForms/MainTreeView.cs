using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bssc.Control.Tools.TreeviewContextMenu;
using Bssc.Control.Tools;
using BSSC.Models;
using Bssc.ViewForms.ProjectMenuForm;
using Bssc.Models.ModelsV;
using Bssc.ViewForms.AffiliateForms;
using Bssc.ViewForms.ResourceMenu;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

using BaseLibrary.Runtime;
using BaseLibrary.ResultData;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Filter;
using BaseLibrary.Util;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Bssc.Control.DataControl.SaveAndLoadData;
using System.Reflection;
using System.Xml.Serialization;
using Bssc.Models.ModelsV.SourceModelsV;
using System.IO;

namespace Bssc.ViewForms
{
    public partial class MainTreeView : UserControl
    {

        public List<RelRoadRef> relRoadRefs = new List<RelRoadRef>();
        public List<RelStandardInstanceReference> relStandardInstanceReferences 
            = new List<RelStandardInstanceReference>();
        public List<RelStandardResourceReference> relStandardResourceReferences
            = new List<RelStandardResourceReference>();
        public List<RelUnitRoadLine> relUnitRoadLines = new List<RelUnitRoadLine>();



        public MainTreeView()
        {
            InitializeComponent();

            ContextMenuWrapForResourceNode contextMenuWrapForFoundationsResourceNode
                = new ContextMenuWrapForResourceNode(skinTreeView1.Nodes.GetTreeNodeByName("resource").Nodes.GetTreeNodeByName("foundations"));

            ContextMenuWrapForResourceNode contextMenuWrapForBeamsResourceNode
                = new ContextMenuWrapForResourceNode(skinTreeView1.Nodes.GetTreeNodeByName("resource").Nodes.GetTreeNodeByName("beams"));

            ContextMenuWrapForResourceNode contextMenuWrapForPiersResourceNode
                = new ContextMenuWrapForResourceNode(skinTreeView1.Nodes.GetTreeNodeByName("resource").Nodes.GetTreeNodeByName("piers"));

            ContextMenuWrapForResourceNode contextMenuWrapForRoadLinesResourceNode
                = new ContextMenuWrapForResourceNode(skinTreeView1.Nodes.GetTreeNodeByName("resource").Nodes.GetTreeNodeByName("roadlines"));

            ContextMenuWrapForProjectNode contextMenuWrapForProjectNode = new ContextMenuWrapForProjectNode(skinTreeView1.Nodes[0]);

            skinTreeView1.Visible = false;

            string path = GetPath() + "sourceFile.dwg";

            Application.DocumentManager.MdiActiveDocument.Database.Ex_ColneDatabaseToCurrentDatabase(path);


            string SupportXmlData = GetPath() + "支座数据.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TempData));
            Stream stream = new FileStream(SupportXmlData,FileMode.Open,FileAccess.Read,FileShare.Read);
            GlobalData.supportSourceModelVs = ((TempData) xmlSerializer.Deserialize(stream)).supportSourceModelVs;
            

        }

        private void 新建工程ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ProjectForm projectForm = new ProjectForm(0, skinTreeView1.Nodes[0]);
            projectForm.Show();

        }

        private void 自由分跨ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlotPqxForm plotPqxForm = new PlotPqxForm();
            plotPqxForm.Show();
        }

        private void 分跨ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DivideSpanForm divideSpanForm = new DivideSpanForm();
            divideSpanForm.Show();
        }

        private void 导入地勘资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadGeoDataForm loadGeoDataForm = new LoadGeoDataForm();
            loadGeoDataForm.Show();
        }

        private void 数据保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TempData tempData = new TempData()
            {
                sourceModelV = GlobalData.sourceModelV,
                projectModelV = GlobalData.projectModelV,
                supportSourceModelVs = GlobalData.supportSourceModelVs
            };
            tempData.SaveDataByXML(GlobalData.projectModelV.Name);
        }

        private void 上传数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TempData tempData = SaveAndLoadDataByXML.GetTempDataByXML();
            if (tempData != null)
            {
                GlobalData.sourceModelV = tempData.sourceModelV;
                GlobalData.projectModelV = tempData.projectModelV;
                //if (tempData.supportSourceModelVs != null)
                //{
                //    GlobalData.supportSourceModelVs = tempData.supportSourceModelVs;
                //}
                
                skinTreeView1.GenerateTreeView(tempData);
                skinTreeView1.Visible = true;
            }
        }

        private string GetPath()
        {
            string str = Assembly.GetExecutingAssembly().Location;
            string[] strs = str.Split('\\');
            string path = "";
            for (int i = 0; i < strs.Length - 1; i++)
            {
                path += strs[i] + "\\";
            }
            return path;
        }

        private void skinTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "上部结构":
                    SupersForm supersForm = new SupersForm(e.Node);
                    supersForm.Show();
                    break;
                case "支座系统":
                    SupportsForm supportsForm = new SupportsForm(e.Node);
                    supportsForm.Show();
                    break;
                case "下部结构":
                    SubsForm subsForm = new SubsForm(e.Node);
                    subsForm.Show();
                    break;
                case "总体成果":
                    ResultDataForm resultDataForm = new ResultDataForm(e.Node.Parent);
                    if (resultDataForm.IsDisposed)
                    {
                        return;
                    }
                    else
                    {
                        resultDataForm.Show();
                    }
                    
                    break;
                default:
                    break;
            }
        }
    }
}
