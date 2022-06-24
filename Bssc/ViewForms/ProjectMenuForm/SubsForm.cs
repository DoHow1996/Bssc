using CCWin;
using Autodesk.AutoCAD.DatabaseServices;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//CAD开发库
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;


//CAD开发基础库
using BaseLibrary.Runtime;
using BaseLibrary.ResultData;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Filter;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Bssc.Models.ModelsV;
using Bssc.ViewForms.AffiliateForms;
using Bssc.Control.CadControl;
using Bssc.Control.Tools;
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Models.ModelsV.SourceModelsV;

namespace Bssc.ViewForms.ProjectMenuForm
{
    public partial class SubsForm : Skin_Mac
    {
        public TreeNode node;
        BindingSource bs = new BindingSource();
        List<SubSModelV> subSModelVs;
        public SubsForm(TreeNode node)
        {
            InitializeComponent();
            this.node = node;
            GlobalInitialize();
        }

        private void GlobalInitialize()
        {

            Column9.DataSource = GlobalData.sourceModelV.pierSourceModelVs;
            Column9.DisplayMember = "Designation";
            Column9.ValueMember = "Id";

            Column11.DataSource = GlobalData.sourceModelV.foundationSourceModelVs;
            Column11.DisplayMember = "Designation";
            Column11.ValueMember = "Id";

            Column16.DataSource = GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs;
            Column16.DisplayMember = "Num";
            //Column16.ValueMember = "Num";
            Column16.ValueMember = "UnitId";


            var tempList = GlobalData.sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Distinct(new SoilLayerCompare()).ToList();

            Column21.DataSource = tempList;
            Column21.DisplayMember = "SoilLayerNum";
            Column21.ValueMember = "SoilLayerNum";

            Column22.DataSource = tempList;
            Column22.DisplayMember = "SoilLayerNum";
            Column22.ValueMember = "SoilLayerNum";

            Column23.DataSource = tempList;
            Column23.DisplayMember = "SoilLayerNum";
            Column23.ValueMember = "SoilLayerNum";

            ContextMenuWrapForDatagridview contextMenuWrapForDatagridview = new ContextMenuWrapForDatagridview(skinDataGridView1);

            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault();;
            subSModelVs = bridgeModelV.subSModelVs;
            
            //subSModelVs.Ex_ForEach(aa => aa.IsTransitionalPier = GlobalData.sourceModelV.pierSourceModelVs.Where(bb =>  bb.Id == aa.PierName).FirstOrDefault().IsTransitionalPier);
            //DrawingInfoGetOrSet.GetSubSModelVs(node);
            

            if (subSModelVs.Count == 0)
            {
                subSModelVs = DrawingInfoGetOrSet.GetStartSubsModelVs(node);
            }

            bs.DataSource = subSModelVs;
            skinDataGridView1.DataSource = bs;
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault(); ;
            bridgeModelV.subSModelVs = DrawingInfoGetOrSet.GetSubSModelVs(node);
            bs.DataSource = bridgeModelV.subSModelVs;
            skinDataGridView1.DataSource = bs;
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            ArrangePierForm arrangePierForm = new ArrangePierForm(node);
            arrangePierForm.Show();
        }

        private void skinButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton4_Click(object sender, EventArgs e)
        {

            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault(); ;
            subSModelVs = bridgeModelV.subSModelVs;

            ///调间距
            if (skinRadioButton1.Checked)
            {
                for (int i = 1; i < subSModelVs.Count; i++)
                {
                    subSModelVs[i].Mark = subSModelVs[i - 1].Mark + subSModelVs[i].distance;
                }
            }
            ///调桩号
            else
            {
                for (int i = 1; i < subSModelVs.Count; i++)
                {
                    subSModelVs[i].distance = subSModelVs[i].Mark - subSModelVs[i - 1].Mark;
                }
            }

            for (int i = 1; i < subSModelVs.Count; i++)
            {
                string a1 = subSModelVs[i - 1].PierNum.Split('_')[0];
                string a2 = subSModelVs[i].PierNum.Split('_')[0];

                if (a1 == a2)
                {
                    subSModelVs[i].Angle = subSModelVs[i - 1].Angle;
                }

            }


            DrawingInfoGetOrSet.SetSubSModelVs(subSModelVs,node);
        }

        private void skinDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                if (skinDataGridView1.Columns[e.ColumnIndex].HeaderText == "桥墩型号")
                {
                    subSModelVs[e.RowIndex].IsTransitionalPier =
                        GlobalData.sourceModelV.pierSourceModelVs.Where(bb => bb.Id == Convert.ToString(subSModelVs[e.RowIndex].PierName)).FirstOrDefault().IsTransitionalPier;
                }

            }
            
        }

        private void skinDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn dataGridViewColumn = skinDataGridView1.Columns[e.ColumnIndex];
            DataGridViewCell dataGridViewCell = skinDataGridView1[e.ColumnIndex,e.RowIndex];
            
        }

        private void skinDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (skinDataGridView1.Columns[e.ColumnIndex].Name == this.Column25.Name)
            {
                DataGridViewCell dataGridViewCell = skinDataGridView1[this.Column16.Index, e.RowIndex];
                this.WindowState = FormWindowState.Minimized;

                DrawingInfoGetOrSet.GetHoleInfo(subSModelVs,e.RowIndex);


                //string[] strs = DrawingInfoGetOrSet.GetHoleInfo();

                //if (strs != null)
                //{
                //    subSModelVs[e.RowIndex].HoleNum = strs[0];
                //    subSModelVs[e.RowIndex].PileFoundationBearingLayerNumber = strs[1];
                //    subSModelVs[e.RowIndex].EnlargeBase1stHoldingLayerNumber = strs[2];
                //    subSModelVs[e.RowIndex].EnlargeBase2ndHoldingLayerNumber = strs[3];
                //}
                

                this.WindowState = FormWindowState.Normal;
            }
            
        }

        private void skinRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (skinRadioButton1.Checked == true)
            {
                this.Column4.ReadOnly = false;
                this.Column5.ReadOnly = true;
                Column4.SetDgvColumnReadOnlyColor(Color.White);
                Column5.SetDgvColumnReadOnlyColor(Color.DarkGray);
            }
            else
            {
                this.Column4.ReadOnly = true;
                this.Column5.ReadOnly = false;
                Column4.SetDgvColumnReadOnlyColor(Color.DarkGray);
                Column5.SetDgvColumnReadOnlyColor(Color.White);
            }
        }

        private void skinRadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
