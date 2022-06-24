using System.Drawing;
using System.Windows.Forms;
using Autodesk.AutoCAD;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsSystem;
using Autodesk.AutoCAD.EditorInput;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using View = Autodesk.AutoCAD.GraphicsSystem.View;
using BaseLibrary.ExtensionMethod;
using System.Collections.Generic;
using System.IO;
using CCWin;
using System;
using BSSC.Models;
using Bssc.Control.Tools;
using Bssc.Control.DataControl;
using Bssc.ViewForms.AffiliateForms;
using Bssc.Control.Tools.TreeviewContextMenu;
using Bssc.Models.ModelsV;
using System.Linq;
using Bssc.Models.ModelsV.SourceModelsV;
using Bssc.Control.CadControl;

namespace Bssc.ViewForms.ResourceMenu
{
    public partial class FoundationConfigForm : Skin_Mac
    {

        View view;
        Model model;
        Extents3d extents;

        /// <summary>
        /// 0为新建 1为修改
        /// </summary>
        public int openStatus;

        /// <summary>
        /// 当openState为0时 node为父节点
        /// 当openState为1时 node为当前节点
        /// </summary>
        public TreeNode node;


        public FoundationConfigForm(int openStatus,TreeNode node)
        {
            InitializeComponent();

            this.openStatus = openStatus;

            this.node = node;

            GlobalInitialize();

        }

        private void GlobalInitialize()
        {
            #region 初始化datagirdview
            List<Position> pileCenterPositions = new List<Position>();
            pileCenterPositions.Add(new Position(10, 10));
            pileCenterPositions.Add(new Position(-10, 10));
            pileCenterPositions.Add(new Position(-10, -10));
            pileCenterPositions.Add(new Position(10, -10));
            BindingSource bs1 = new BindingSource();
            bs1.DataSource = pileCenterPositions;
            skinDataGridView_caps.DataSource = bs1;

            List<Position> capsPositions = new List<Position>();
            capsPositions.Add(new Position(5, 5));
            capsPositions.Add(new Position(-5, 5));
            capsPositions.Add(new Position(-5, -5));
            capsPositions.Add(new Position(5, -5));
            BindingSource bs2 = new BindingSource();
            bs2.DataSource = capsPositions;
            skinDataGridView_pile.DataSource = bs2;

            #endregion


            if (openStatus == 0)
            {
                this.Text = "新建基础";
            }
            else if (openStatus == 1)
            {
                this.Text = "修改基础";

                var foundationSourceModelV = GlobalData.sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();

                skinTextBoxName.Text = foundationSourceModelV.Designation;
                skinComboBoxType.Text = foundationSourceModelV.Type;
                skinTextBoxThickness.Text = Convert.ToString(foundationSourceModelV.CapsThickness);
                skinTextBox1.Text = Convert.ToString(foundationSourceModelV.pileRadius);
                skinTextBox2.Text = Convert.ToString(foundationSourceModelV.ExpandTheMinimumValueOfTheFoundationIntoTheBearingLayer);

                skinTextBoxCapLength.Text = Convert.ToString(foundationSourceModelV.capsXLen);
                skinTextBoxCapWidth.Text = Convert.ToString(foundationSourceModelV.capsYLen);

                checkBox1.Checked = foundationSourceModelV.ischecked;

                skinTextBoxPileXNum.Text = Convert.ToString(foundationSourceModelV.pileXNum);
                skinTextBoxPileXDis.Text = Convert.ToString(foundationSourceModelV.pileXDis);
                skinTextBoxPileYNum.Text = Convert.ToString(foundationSourceModelV.pileYNum);
                skinTextBoxPileYDis.Text = Convert.ToString(foundationSourceModelV.pileYDis);

                if (foundationSourceModelV.CapsSectionPoints != null || foundationSourceModelV.CapsSectionPoints != "")
                {
                    BindingSource bsCapsPoints = new BindingSource();
                    bsCapsPoints.DataSource = foundationSourceModelV.CapsSectionPoints.GetPositions();
                    skinDataGridView_caps.DataSource = bsCapsPoints;
                }

                if (foundationSourceModelV.PileCenterPoints != null || foundationSourceModelV.PileCenterPoints != "")
                {
                    BindingSource bsPileCenterPoints = new BindingSource();
                    bsPileCenterPoints.DataSource = foundationSourceModelV.PileCenterPoints.GetPositions();
                    skinDataGridView_pile.DataSource = bsPileCenterPoints;
                }
                
            }  
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {

            if (!node.hasName(skinTextBoxName.Text, openStatus))
            {
                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "基础名已存在";
                alertForm.Show();
                return;
            }

            
            if (openStatus == 0)
            {

                FoundationSourceModelV foundationSourceModelV = new FoundationSourceModelV();

                string id = "foundation" + UUIDUtil.Get32UUID();
                foundationSourceModelV.Id = id;
                foundationSourceModelV.Designation = skinTextBoxName.Text;
                foundationSourceModelV.Type = skinComboBoxType.Text;
                foundationSourceModelV.CapsThickness = Convert.ToDouble(skinTextBoxThickness.Text);
                foundationSourceModelV.pileRadius = Convert.ToDouble(skinTextBox1.Text);
                
                foundationSourceModelV.ExpandTheMinimumValueOfTheFoundationIntoTheBearingLayer = Convert.ToDouble(skinTextBox2.Text);

                foundationSourceModelV.ischecked = checkBox1.Checked;
                foundationSourceModelV.capsXLen = Convert.ToDouble(skinTextBoxCapLength.Text);
                foundationSourceModelV.capsYLen = Convert.ToDouble(skinTextBoxCapWidth.Text);
                foundationSourceModelV.pileXNum = Convert.ToDouble(skinTextBoxPileXNum.Text);
                foundationSourceModelV.pileXDis = Convert.ToDouble(skinTextBoxPileXDis.Text);
                foundationSourceModelV.pileYNum = Convert.ToDouble(skinTextBoxPileYNum.Text);
                foundationSourceModelV.pileYDis = Convert.ToDouble(skinTextBoxPileYDis.Text);

                if (checkBox1.Checked)
                {
                    foundationSourceModelV.CapsSectionPoints = ResFoundationControl.GetPositions(
                        Convert.ToDouble(skinTextBoxCapLength.Text), Convert.ToDouble(skinTextBoxCapWidth.Text));
                    foundationSourceModelV.PileCenterPoints = ResFoundationControl.GetPositions(
                        Convert.ToDouble(skinTextBoxPileXNum.Text), Convert.ToDouble(skinTextBoxPileYNum.Text),
                        Convert.ToDouble(skinTextBoxPileXDis.Text), Convert.ToDouble(skinTextBoxPileYDis.Text));
                }
                else
                {
                    foundationSourceModelV.CapsSectionPoints = skinDataGridView_caps.GetPositions().GetPosition();
                    foundationSourceModelV.PileCenterPoints = skinDataGridView_pile.GetPositions().GetPosition();
                }

                TreeNode childNode = new TreeNode();
                childNode.Name = id;
                childNode.Text = skinTextBoxName.Text;
                node.Nodes.Add(childNode);
                GlobalData.sourceModelV.foundationSourceModelVs.Add(foundationSourceModelV);
                ContextMenuWrapForSingleSourceNode context = new ContextMenuWrapForSingleSourceNode(childNode);
                ///同步保存cad块
                Application.DocumentManager.MdiActiveDocument.CreateFoundationBlock(foundationSourceModelV);
            }
            else if (openStatus == 1)
            {
                var foundationSourceModelV = GlobalData.sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                node.Text = skinTextBoxName.Text;
                foundationSourceModelV.Designation = skinTextBoxName.Text;
                foundationSourceModelV.Type = skinComboBoxType.Text;
                foundationSourceModelV.CapsThickness = Convert.ToDouble(skinTextBoxThickness.Text);
                foundationSourceModelV.pileRadius = Convert.ToDouble(skinTextBox1.Text);
       
                foundationSourceModelV.ExpandTheMinimumValueOfTheFoundationIntoTheBearingLayer = Convert.ToDouble(skinTextBox2.Text);

                foundationSourceModelV.ischecked = checkBox1.Checked;
                foundationSourceModelV.capsXLen = Convert.ToDouble(skinTextBoxCapLength.Text);
                foundationSourceModelV.capsYLen = Convert.ToDouble(skinTextBoxCapWidth.Text);
                foundationSourceModelV.pileXNum = Convert.ToDouble(skinTextBoxPileXNum.Text);
                foundationSourceModelV.pileXDis = Convert.ToDouble(skinTextBoxPileXDis.Text);
                foundationSourceModelV.pileYNum = Convert.ToDouble(skinTextBoxPileYNum.Text);
                foundationSourceModelV.pileYDis = Convert.ToDouble(skinTextBoxPileYDis.Text);

                if (checkBox1.Checked)
                {
                    foundationSourceModelV.CapsSectionPoints = ResFoundationControl.GetPositions(
                        Convert.ToDouble(skinTextBoxCapLength.Text), Convert.ToDouble(skinTextBoxCapWidth.Text));
                    foundationSourceModelV.PileCenterPoints = ResFoundationControl.GetPositions(
                        Convert.ToDouble(skinTextBoxPileXNum.Text), Convert.ToDouble(skinTextBoxPileYNum.Text),
                        Convert.ToDouble(skinTextBoxPileXDis.Text), Convert.ToDouble(skinTextBoxPileYDis.Text));
                }
                else
                {
                    foundationSourceModelV.CapsSectionPoints = skinDataGridView_caps.GetPositions().GetPosition();
                    foundationSourceModelV.PileCenterPoints = skinDataGridView_pile.GetPositions().GetPosition();
                }

                ///同步保存cad块
                Application.DocumentManager.MdiActiveDocument.CreateFoundationBlock(foundationSourceModelV);
            }
            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            ///临时的实例用来预览
            FoundationSourceModelV foundationSourceModelV = new FoundationSourceModelV();

            string id = "foundation" + UUIDUtil.Get32UUID();
            foundationSourceModelV.Id = id;
            foundationSourceModelV.Designation = skinTextBoxName.Text;
            foundationSourceModelV.Type = skinComboBoxType.Text;
            foundationSourceModelV.CapsThickness = Convert.ToDouble(skinTextBoxThickness.Text);
            foundationSourceModelV.pileRadius = Convert.ToDouble(skinTextBox1.Text);

            if (checkBox1.Checked)
            {
                foundationSourceModelV.CapsSectionPoints = ResFoundationControl.GetPositions(
                    Convert.ToDouble(skinTextBoxCapLength.Text), Convert.ToDouble(skinTextBoxCapWidth.Text));
                foundationSourceModelV.PileCenterPoints = ResFoundationControl.GetPositions(
                    Convert.ToDouble(skinTextBoxPileXNum.Text), Convert.ToDouble(skinTextBoxPileYNum.Text),
                    Convert.ToDouble(skinTextBoxPileXDis.Text), Convert.ToDouble(skinTextBoxPileYDis.Text));
            }
            else
            {
                foundationSourceModelV.CapsSectionPoints = skinDataGridView_caps.GetPositions().GetPosition();
                foundationSourceModelV.PileCenterPoints = skinDataGridView_pile.GetPositions().GetPosition();
            }

            previewControl1.FoundationConfigPreView(foundationSourceModelV);
        }

        private void skinPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
