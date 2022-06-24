using Bssc.Control.CadControl;
using Bssc.Control.DataControl;
using Bssc.Control.Tools;
using Bssc.Control.Tools.TreeviewContextMenu;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
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
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Bssc.ViewForms.ResourceMenu
{
    public partial class PierConfigForm : Skin_Mac
    {

        /// <summary>
        /// 0为新建 1为修改
        /// </summary>
        public int openStatus;

        /// <summary>
        /// 当openState为0时 node为父节点
        /// 当openState为1时 node为当前节点
        /// </summary>
        public TreeNode node;

        public PierConfigForm(int openStatus, TreeNode node)
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
                this.Text = "新建墩";
            }
            else if (openStatus == 1)
            {
                this.Text = "修改墩";

                var pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();

                skinTextBoxDesignation.Text = pierSourceModelV.Designation;
                skinComboBoxType.Text = pierSourceModelV.Type;
                skinCheckBoxIsTransitionalPier.Checked = pierSourceModelV.IsTransitionalPier == 1 ? true : false;
                skinTextBoxVariableHeight.Text = Convert.ToString(pierSourceModelV.VariableHeight);
                skinTextBoxBottomTransverseWidth.Text = Convert.ToString(pierSourceModelV.BottomTransverseWidth);
                skinTextBoxTopTransverseWidth.Text = Convert.ToString(pierSourceModelV.TopTransverseWidth);
                skinTextBoxBottomLongitudinalThickness.Text = Convert.ToString(pierSourceModelV.BottomLongitudinalThickness);
                skinTextBoxTopLLongitudinalThickness.Text = Convert.ToString(pierSourceModelV.TopLLongitudinalThickness);
                skinComboBoxSupportArrangement.Text = pierSourceModelV.SupportArrangement;
                skinTextBoxSupportTransverseSpacing.Text = Convert.ToString(pierSourceModelV.SupportTransverseSpacing);
                skinTextBoxCapsAngle.Text = Convert.ToString(pierSourceModelV.CapsAngle);
                skinTextBoxPierRounderCornerRadius.Text = Convert.ToString(pierSourceModelV.PierRounderCornerRadius);
                skinTextBoxSupportLongitudinalSpacing.Text = Convert.ToString(pierSourceModelV.SupportLongitudinalSpacing);
                skinTextBoxRemark.Text = pierSourceModelV.Remark;
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (!node.hasName(skinTextBoxDesignation.Text, openStatus))
            {
                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "墩名已存在";
                alertForm.Show();
                return;
            }

            if (openStatus == 0)
            {
                PierSourceModelV pierSourceModelV = new PierSourceModelV();
                string id = "pier" + UUIDUtil.Get32UUID();
                pierSourceModelV.Id = id;
                pierSourceModelV.Designation = skinTextBoxDesignation.Text;
                pierSourceModelV.Type = skinComboBoxType.Text;
                pierSourceModelV.IsTransitionalPier = skinCheckBoxIsTransitionalPier.Checked == true ? 1 : 0;
                pierSourceModelV.VariableHeight = Convert.ToDouble(skinTextBoxVariableHeight.Text);
                pierSourceModelV.BottomTransverseWidth = Convert.ToDouble(skinTextBoxBottomTransverseWidth.Text);
                pierSourceModelV.TopTransverseWidth = Convert.ToDouble(skinTextBoxTopTransverseWidth.Text);
                pierSourceModelV.BottomLongitudinalThickness = Convert.ToDouble(skinTextBoxBottomLongitudinalThickness.Text);
                pierSourceModelV.TopLLongitudinalThickness = Convert.ToDouble(skinTextBoxTopLLongitudinalThickness.Text);
                pierSourceModelV.TopLLongitudinalThickness = Convert.ToDouble(skinTextBoxTopLLongitudinalThickness.Text);
                pierSourceModelV.SupportArrangement = skinComboBoxSupportArrangement.Text;
                pierSourceModelV.SupportTransverseSpacing = Convert.ToDouble(skinTextBoxSupportTransverseSpacing.Text);
                pierSourceModelV.CapsAngle = Convert.ToDouble(skinTextBoxCapsAngle.Text);
                pierSourceModelV.PierRounderCornerRadius = Convert.ToDouble(skinTextBoxPierRounderCornerRadius.Text);
                pierSourceModelV.Remark = skinTextBoxRemark.Text;
                pierSourceModelV.SupportLongitudinalSpacing = Convert.ToDouble(skinTextBoxSupportLongitudinalSpacing.Text);

                TreeNode childNode = new TreeNode();
                node.Nodes.Add(childNode);
                childNode.Name = id;
                childNode.Text = skinTextBoxDesignation.Text;
                GlobalData.sourceModelV.pierSourceModelVs.Add(pierSourceModelV);
                ContextMenuWrapForSingleSourceNode context = new ContextMenuWrapForSingleSourceNode(childNode);

                ///同步保存cad块
                Application.DocumentManager.MdiActiveDocument.CreatePierBlock(pierSourceModelV);
            }
            else if (openStatus == 1)
            {
                var pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                pierSourceModelV.Designation = skinTextBoxDesignation.Text;
                pierSourceModelV.Type = skinComboBoxType.Text;
                pierSourceModelV.IsTransitionalPier = skinCheckBoxIsTransitionalPier.Checked == true ? 1 : 0;
                pierSourceModelV.VariableHeight = Convert.ToDouble(skinTextBoxVariableHeight.Text);
                pierSourceModelV.BottomTransverseWidth = Convert.ToDouble(skinTextBoxBottomTransverseWidth.Text);
                pierSourceModelV.TopTransverseWidth = Convert.ToDouble(skinTextBoxTopTransverseWidth.Text);
                pierSourceModelV.BottomLongitudinalThickness = Convert.ToDouble(skinTextBoxBottomLongitudinalThickness.Text);
                pierSourceModelV.TopLLongitudinalThickness = Convert.ToDouble(skinTextBoxTopLLongitudinalThickness.Text);
                pierSourceModelV.TopLLongitudinalThickness = Convert.ToDouble(skinTextBoxTopLLongitudinalThickness.Text);
                pierSourceModelV.SupportArrangement = skinComboBoxSupportArrangement.Text;
                pierSourceModelV.SupportTransverseSpacing = Convert.ToDouble(skinTextBoxSupportTransverseSpacing.Text);
                pierSourceModelV.CapsAngle = Convert.ToDouble(skinTextBoxCapsAngle.Text);
                pierSourceModelV.PierRounderCornerRadius = Convert.ToDouble(skinTextBoxPierRounderCornerRadius.Text);
                pierSourceModelV.Remark = skinTextBoxRemark.Text;
                pierSourceModelV.SupportLongitudinalSpacing = Convert.ToDouble(skinTextBoxSupportLongitudinalSpacing.Text);

                node.Text = skinTextBoxDesignation.Text;

                ///同步保存cad块
                Application.DocumentManager.MdiActiveDocument.CreatePierBlock(pierSourceModelV);
            }
            this.Close();
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            ///临时的实例用来预览
            PierSourceModelV pierSourceModelV = new PierSourceModelV();
            string id = "pier" + UUIDUtil.Get32UUID();
            pierSourceModelV.Id = id;
            pierSourceModelV.Designation = skinTextBoxDesignation.Text;
            pierSourceModelV.Type = skinComboBoxType.Text;
            pierSourceModelV.IsTransitionalPier = skinCheckBoxIsTransitionalPier.Checked == true ? 1 : 0;
            pierSourceModelV.VariableHeight = Convert.ToDouble(skinTextBoxVariableHeight.Text);
            pierSourceModelV.BottomTransverseWidth = Convert.ToDouble(skinTextBoxBottomTransverseWidth.Text);
            pierSourceModelV.TopTransverseWidth = Convert.ToDouble(skinTextBoxTopTransverseWidth.Text);
            pierSourceModelV.BottomLongitudinalThickness = Convert.ToDouble(skinTextBoxBottomLongitudinalThickness.Text);
            pierSourceModelV.TopLLongitudinalThickness = Convert.ToDouble(skinTextBoxTopLLongitudinalThickness.Text);
            pierSourceModelV.TopLLongitudinalThickness = Convert.ToDouble(skinTextBoxTopLLongitudinalThickness.Text);
            pierSourceModelV.SupportArrangement = skinComboBoxSupportArrangement.Text;
            pierSourceModelV.SupportTransverseSpacing = Convert.ToDouble(skinTextBoxSupportTransverseSpacing.Text);
            pierSourceModelV.CapsAngle = Convert.ToDouble(skinTextBoxCapsAngle.Text);
            pierSourceModelV.PierRounderCornerRadius = Convert.ToDouble(skinTextBoxPierRounderCornerRadius.Text);
            pierSourceModelV.Remark = skinTextBoxRemark.Text;
            pierSourceModelV.SupportLongitudinalSpacing = Convert.ToDouble(skinTextBoxSupportLongitudinalSpacing.Text);

            previewControl1.PierConfigPreView(pierSourceModelV);
        }
    }
}
