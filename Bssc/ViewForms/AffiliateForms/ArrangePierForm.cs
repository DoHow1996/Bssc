using Bssc.Control.CadControl;
using Bssc.Models.ModelsV;
using Bssc.ViewForms.ResourceMenu;
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
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using BaseLibrary.ExtensionMethod;
using Autodesk.AutoCAD.EditorInput;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Bssc.ViewForms.AffiliateForms
{
    public partial class ArrangePierForm : Skin_Mac
    {
        public TreeNode node;
        BindingSource bsPier = new BindingSource();
        BindingSource bsFoundation = new BindingSource();
        public ArrangePierForm(TreeNode node)
        {
            InitializeComponent();

            this.node = node;

            GlobalInitialize();
        }

        private void GlobalInitialize()
        {

            
            bsPier.DataSource = GlobalData.sourceModelV.pierSourceModelVs;
            skinComboBox1.DataSource = bsPier;
            skinComboBox1.DisplayMember = "Designation";
            skinComboBox1.ValueMember = "Id";
            
            bsFoundation.DataSource = GlobalData.sourceModelV.foundationSourceModelVs;
            skinComboBox2.DataSource = bsFoundation;
            skinComboBox2.DisplayMember = "Designation";
            skinComboBox2.ValueMember = "Id";

        }

        private void skinPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            TreeNode pierNode = node.TreeView.Nodes[1].Nodes[1];
            PierConfigForm pierConfigForm = new PierConfigForm(0, pierNode);
            pierConfigForm.Show();
            bsPier.ResetBindings(false);
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            TreeNode foundationNode = node.TreeView.Nodes[1].Nodes[0];
            FoundationConfigForm foundationConfigForm = new FoundationConfigForm(0, foundationNode);
            foundationConfigForm.Show();
            bsFoundation.ResetBindings(false);
        }

        private void skinComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var foundationSourceModelV = GlobalData.sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox2.SelectedValue)).FirstOrDefault();
            var pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox1.SelectedValue)).FirstOrDefault();
            previewControl1.ArrangePierPreView(foundationSourceModelV,pierSourceModelV);
        }

        private void skinComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var foundationSourceModelV = GlobalData.sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox2.SelectedValue)).FirstOrDefault();
            var pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox1.SelectedValue)).FirstOrDefault();
            previewControl1.ArrangePierPreView(foundationSourceModelV, pierSourceModelV);
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault();
            //roadModelV.roadSourceModelV;
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var foundationSourceModelV = GlobalData.sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox2.SelectedValue)).FirstOrDefault();
            var pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox1.SelectedValue)).FirstOrDefault();
            bool isFlag;
            if (skinRadioButton1.Checked == true)
            {
                isFlag = true;
            }
            else
            {
                isFlag = false;
            }
            this.Close();
            Application.DocumentManager.MdiActiveDocument.ArrangePier(roadSourceModelV,bridgeModelV,pierSourceModelV,foundationSourceModelV,isFlag);
        }
    }
}
