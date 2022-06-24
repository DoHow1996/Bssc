using Bssc.Control.DataControl;
using Bssc.Control.Tools;
using Bssc.Control.Tools.TreeviewContextMenu;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.ViewForms.AffiliateForms;
using BSSC.Models;
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
    public partial class RoadForm : Skin_Mac
    {

        public TreeNode node;
        public int openStatus;

        public RoadForm(int openStatus,TreeNode node)
        {
            InitializeComponent();
            this.node = node;
            this.openStatus = openStatus;
            GlobalInitialize();
        }

        private void GlobalInitialize()
        {

            skinComboBox1.DataSource = GlobalData.sourceModelV.roadSourceModelVs;
            skinComboBox1.DisplayMember = "Designation";
            skinComboBox1.ValueMember = "Id";

            if (openStatus == 0)
            {
                this.Text = "新建路线";
            }
            else if (openStatus == 1)
            {
                this.Text = "修改路线";
                var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                skinTextBoxName.Text = roadModelV.Designation;
                skinTextBoxNum.Text = roadModelV.Num;
                skinTextBoxRemark.Text = roadModelV.Remark;

                var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault();

                skinComboBox1.Text = roadSourceModelV.Designation;

                //if (roadModelV.roadSourceModelV != null)
                //{
                //    skinComboBox1.Text = roadModelV.roadSourceModelV.Designation;
                //}
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {

            if (openStatus == 0)
            {
                RoadModelV roadModelV = new RoadModelV();
                string id = "roadProject" + UUIDUtil.Get32UUID();
                roadModelV.Id = id;
                roadModelV.Designation = skinTextBoxName.Text;
                roadModelV.Num = skinTextBoxNum.Text;
                roadModelV.Remark = skinTextBoxRemark.Text;
                roadModelV.RoadSourceModelId = Convert.ToString(skinComboBox1.SelectedValue);
                //roadModelV.roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox1.SelectedValue)).FirstOrDefault();

                GlobalData.projectModelV.roadModelVs.Add(roadModelV);

                TreeNode childNode = new TreeNode();
                node.Nodes.Add(childNode);
                childNode.Name = id;
                childNode.Text = skinTextBoxName.Text; 

                ContextMenuWrapForRoadNode contextMenuWrapForRoadNode = new ContextMenuWrapForRoadNode(childNode);
            }
            else if (openStatus == 1)
            {
                var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                roadModelV.Designation = skinTextBoxName.Text;
                roadModelV.Num = skinTextBoxNum.Text;
                roadModelV.Remark = skinTextBoxRemark.Text;
                roadModelV.RoadSourceModelId = Convert.ToString(skinComboBox1.SelectedValue);
                //roadModelV.roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox1.SelectedValue)).FirstOrDefault();

                node.Text = skinTextBoxName.Text;
            }

            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
