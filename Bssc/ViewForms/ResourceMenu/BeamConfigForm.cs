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

namespace Bssc.ViewForms.ResourceMenu
{
    public partial class BeamConfigForm : Skin_Mac
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

        public BeamConfigForm(int openStatus,TreeNode node)
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
                this.Text = "新建梁";
            }
            else if (openStatus == 1)
            {
                this.Text = "修改梁";

                List<BeamSourceModelV> beamSourceModelVs = GlobalData.sourceModelV.beamSourceModelVs;
                var beamSourceModelV = beamSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();

                skinTextBoxDesignation.Text = beamSourceModelV.Designation;
                skinComboBoxType.Text = beamSourceModelV.Type;
                skinTextBoxSpanNum.Text = Convert.ToString(beamSourceModelV.SpanNum);
                skinTextBoxOverlayThickness.Text = Convert.ToString(beamSourceModelV.OverlayThickness);
                skinTextBoxSideBeamPierHeight.Text = Convert.ToString(beamSourceModelV.SideBeamPierHeight);
                skinTextBoxMidBeamPierHeight.Text = Convert.ToString(beamSourceModelV.MidBeamPierHeight);
                skinTextBoxStartPointPierAddHeight.Text = Convert.ToString(beamSourceModelV.StartPointPierAddHeight);
                skinTextBoxEndPointPierAddHeight.Text = Convert.ToString(beamSourceModelV.EndPointPierAddHeight);
                if (beamSourceModelV.BottomBoardCrossSlope == 0)
                {
                    skinRadioButtondbsp.Checked = true;
                }
                else
                {
                    skinRadioButtondbtp.Checked = true;
                }
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            if (!node.hasName(skinTextBoxDesignation.Text, openStatus))
            {
                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "梁名已存在";
                alertForm.Show();
                return;
            }

            
            if (openStatus == 0)
            {
                BeamSourceModelV beamSourceModelV = new BeamSourceModelV();
                string id = "beam" + UUIDUtil.Get32UUID();
                beamSourceModelV.Id = id;
                beamSourceModelV.Designation = skinTextBoxDesignation.Text;
                beamSourceModelV.Type = skinComboBoxType.Text;
                beamSourceModelV.SpanNum = Convert.ToInt16(skinTextBoxSpanNum.Text);
                beamSourceModelV.OverlayThickness = Convert.ToDouble(skinTextBoxOverlayThickness.Text);
                beamSourceModelV.SideBeamPierHeight = Convert.ToDouble(skinTextBoxSideBeamPierHeight.Text);
                beamSourceModelV.MidBeamPierHeight = Convert.ToDouble(skinTextBoxMidBeamPierHeight.Text);
                beamSourceModelV.StartPointPierAddHeight = Convert.ToDouble(skinTextBoxStartPointPierAddHeight.Text);
                beamSourceModelV.EndPointPierAddHeight = Convert.ToDouble(skinTextBoxEndPointPierAddHeight.Text);
                if (skinRadioButtondbsp.Checked)
                {
                    beamSourceModelV.BottomBoardCrossSlope = 0;
                }
                else
                {
                    beamSourceModelV.BottomBoardCrossSlope = 1;
                }

                TreeNode childNode = new TreeNode();
                node.Nodes.Add(childNode);
                childNode.Name = id;
                childNode.Text = skinTextBoxDesignation.Text;
                GlobalData.sourceModelV.beamSourceModelVs.Add(beamSourceModelV);
                ContextMenuWrapForSingleSourceNode context = new ContextMenuWrapForSingleSourceNode(childNode);
            }
            else if (openStatus == 1)
            {
                var beamSourceModelV = GlobalData.sourceModelV.beamSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                beamSourceModelV.Designation = skinTextBoxDesignation.Text;
                beamSourceModelV.Type = skinComboBoxType.Text;
                beamSourceModelV.SpanNum = Convert.ToInt16(skinTextBoxSpanNum.Text);
                beamSourceModelV.OverlayThickness = Convert.ToDouble(skinTextBoxOverlayThickness.Text);
                beamSourceModelV.SideBeamPierHeight = Convert.ToDouble(skinTextBoxSideBeamPierHeight.Text);
                beamSourceModelV.MidBeamPierHeight = Convert.ToDouble(skinTextBoxMidBeamPierHeight.Text);
                beamSourceModelV.StartPointPierAddHeight = Convert.ToDouble(skinTextBoxStartPointPierAddHeight.Text);
                beamSourceModelV.EndPointPierAddHeight = Convert.ToDouble(skinTextBoxEndPointPierAddHeight.Text);
                if (skinRadioButtondbsp.Checked)
                {
                    beamSourceModelV.BottomBoardCrossSlope = 0;
                }
                else
                {
                    beamSourceModelV.BottomBoardCrossSlope = 1;
                }

                node.Text = skinTextBoxDesignation.Text;
            }

            this.Close();

        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
