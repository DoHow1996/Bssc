using Bssc.Control.Tools;
using Bssc.Control.Tools.TreeviewContextMenu;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.ViewForms.ProjectMenuForm
{
    public partial class BridgeForm : Skin_Mac
    {

        public int openStatus;
        public TreeNode node;

        public BridgeForm(int openStatus,TreeNode node)
        {
            InitializeComponent();

            this.openStatus = openStatus;
            this.node = node;

            GlobalInitilize();
        }

        private void GlobalInitilize()
        {
            var roadSourceModelVs = GlobalData.sourceModelV.roadSourceModelVs;
            skinComboBoxAffRoad.DataSource = roadSourceModelVs;
            skinComboBoxAffRoad.DisplayMember = "Designation";
            skinComboBoxAffRoad.ValueMember = "Id";

            if (openStatus == 0)
            {
                this.Text = "新建桥梁";
                var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                if (roadModelV.RoadSourceModelId != null)
                {
                    skinTextBoxMainRoad.Text = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().Designation; ;
                }
                
            }
            else if (openStatus == 1)
            {
                this.Text = "修改桥梁";
                var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == node.Parent.Name).FirstOrDefault();
                var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                skinTextBoxDeignation.Text = bridgeModelV.Designation;
                skinTextBoxNum.Text = bridgeModelV.Num;
                skinTextBoxStartMark.Text = Convert.ToString(bridgeModelV.StartMark);
                skinTextBoxEndMark.Text = Convert.ToString(bridgeModelV.EndMark);
                skinTextBoxMainRoad.Text = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == bridgeModelV.MainRoadSourceModelVId).FirstOrDefault().Designation;
                if (bridgeModelV.AffRoadSourceModelVId != null)
                {
                    skinComboBoxAffRoad.Text = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == bridgeModelV.AffRoadSourceModelVId).FirstOrDefault().Designation;
                }
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (openStatus == 0)
            {
                BridgeModelV bridgeModelV = new BridgeModelV();
                string id = "bridgeProject" + UUIDUtil.Get32UUID();
                bridgeModelV.Id = id;
                bridgeModelV.Designation = skinTextBoxDeignation.Text;
                bridgeModelV.Num = skinTextBoxNum.Text;
                bridgeModelV.Type = "1";
                bridgeModelV.CreateTime = DateTime.Now;
                bridgeModelV.ModifyTime = DateTime.Now;
                bridgeModelV.StartMark = Convert.ToDouble(skinTextBoxStartMark.Text);
                bridgeModelV.EndMark = Convert.ToDouble(skinTextBoxEndMark.Text);

                var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                bridgeModelV.MainRoadSourceModelVId = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().Id; ;
                bridgeModelV.AffRoadSourceModelVId = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBoxAffRoad.SelectedValue)).FirstOrDefault().Id;
                roadModelV.bridgeModelVs.Add(bridgeModelV);
                TreeNode childNode = new TreeNode();
                node.Nodes.Add(childNode);
                childNode.Name = id;
                childNode.Text = skinTextBoxDeignation.Text;

                ContextMenuWrapForBridgeNode contextMenuWrapForBridgeNode = new ContextMenuWrapForBridgeNode(childNode);

                TreeNode grandChild1 = new TreeNode();
                TreeNode grandChild2 = new TreeNode();
                TreeNode grandChild3 = new TreeNode();
                TreeNode grandChild4 = new TreeNode();
                grandChild1.Text = "上部结构";
                grandChild1.Name = "supers" + UUIDUtil.Get32UUID();
                grandChild2.Text = "支座系统";
                grandChild2.Name = "supports" + UUIDUtil.Get32UUID();
                grandChild3.Text = "下部结构";
                grandChild3.Name = "subs" + UUIDUtil.Get32UUID();
                grandChild4.Text = "总体成果";
                grandChild4.Name = "allResult" + UUIDUtil.Get32UUID();
                childNode.Nodes.Add(grandChild1);
                childNode.Nodes.Add(grandChild2);
                childNode.Nodes.Add(grandChild3);
                childNode.Nodes.Add(grandChild4);
                //ContextMenuWrapForBridgeSturctureNode contextMenuWrapForBridgeSturctureNode1 = new ContextMenuWrapForBridgeSturctureNode(grandChild1);
                //ContextMenuWrapForBridgeSturctureNode contextMenuWrapForBridgeSturctureNode2 = new ContextMenuWrapForBridgeSturctureNode(grandChild2);
                //ContextMenuWrapForBridgeSturctureNode contextMenuWrapForBridgeSturctureNode3 = new ContextMenuWrapForBridgeSturctureNode(grandChild3);
                //ContextMenuWrapForBridgeSturctureNode contextMenuWrapForBridgeSturctureNode4 = new ContextMenuWrapForBridgeSturctureNode(grandChild4);
            }
            else if (openStatus == 1)
            {
                var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == node.Parent.Name).FirstOrDefault();
                BridgeModelV bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                bridgeModelV.Designation = skinTextBoxDeignation.Text;
                bridgeModelV.Num = skinTextBoxNum.Text;
                bridgeModelV.Type = "1";
                bridgeModelV.CreateTime = DateTime.Now;
                bridgeModelV.ModifyTime = DateTime.Now;
                bridgeModelV.StartMark = Convert.ToDouble(skinTextBoxStartMark.Text);
                bridgeModelV.EndMark = Convert.ToDouble(skinTextBoxEndMark.Text);
                bridgeModelV.MainRoadSourceModelVId = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().Id; ;
                bridgeModelV.AffRoadSourceModelVId = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBoxAffRoad.SelectedValue)).FirstOrDefault().Id;

                node.Text = skinTextBoxDeignation.Text;
            }
            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
