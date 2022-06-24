using Bssc.Control.Tools;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
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
    public partial class ProjectForm : Skin_Mac
    {

        /// <summary>
        /// 0为新建 1为修改
        /// </summary>
        public int openStatus;

        public TreeNode node;

        public ProjectForm(int openStatus,TreeNode node)
        {
            InitializeComponent();
            this.openStatus = openStatus;
            this.node = node;
            GlobalInitialize();
        }

        private void GlobalInitialize()
        {
            if (openStatus == 0)
            {
                this.Text = "新建项目";
                skinTextBoxCreateTime.Text = DateTime.Now.ToString();
            }
            else if (openStatus == 1)
            {
                this.Text = "修改项目";
                ProjectModelV projectmodelv = GlobalData.projectModelV;
                skinTextBoxName.Text = projectmodelv.Name;
                skinTextBoxNum.Text = projectmodelv.Num;
                skinTextBoxCreator.Text = projectmodelv.Creator;
                skinTextBoxCreateTime.Text = projectmodelv.CreateTime.ToString();
                skinTextBoxModifyTime.Text = DateTime.Now.ToString();
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (openStatus == 0)
            {
                ProjectModelV projectmodelv = GlobalData.projectModelV;
                string id = "project" + UUIDUtil.Get32UUID();
                projectmodelv.Id = id;
                projectmodelv.Name = skinTextBoxName.Text;
                projectmodelv.Num = skinTextBoxNum.Text;
                projectmodelv.Creator = skinTextBoxCreator.Text;
                projectmodelv.CreateTime = DateTime.Now;
                projectmodelv.ModifyTime = DateTime.Now;

                node.Name = id;
                node.Text = skinTextBoxName.Text;

                node.TreeView.Visible = true;
            }
            else if (openStatus == 1)
            {
                ProjectModelV projectmodelv = GlobalData.projectModelV;
                projectmodelv.Name = skinTextBoxName.Text;
                projectmodelv.Num = skinTextBoxNum.Text;
                projectmodelv.Creator = skinTextBoxCreator.Text;
                projectmodelv.CreateTime = Convert.ToDateTime(skinTextBoxCreateTime.Text);
                projectmodelv.ModifyTime = DateTime.Now;
                node.Text = skinTextBoxName.Text;
            }
            this.Close();
        }
    }
}
