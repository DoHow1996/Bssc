using Bssc.Control.DataControl;
using Bssc.Control.Tools;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
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
    public partial class SupersForm : Skin_Mac
    {
        public TreeNode node;
        public List<SuperSModelV> superSModelVs;
        public BridgeModelV bridgeModelV;
        public SupersForm(TreeNode node)
        {
            InitializeComponent();
            this.node = node;

            GlobalInitialize();
        }

        private void GlobalInitialize()
        {

            Column7.DataSource = GlobalData.sourceModelV.beamSourceModelVs;
            Column7.DisplayMember = "Designation";
            Column7.ValueMember = "Id";

            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            superSModelVs = bridgeModelV.subSModelVs.GetSuperSModelVs(node);

            bridgeModelV.superSModelVs = superSModelVs;

            BindingSource bs = new BindingSource();
            bs.DataSource = bridgeModelV.superSModelVs;
            this.skinDataGridView1.DataSource = bs;


            skinDataGridView1.SetDgvColumnsReadOnlyColor();

        }

        private void skinDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (skinDataGridView1.Columns[e.ColumnIndex].HeaderText == "主梁型号")
                {

                    BeamSourceModelV beamSourceModelV = GlobalData.sourceModelV.beamSourceModelVs.Where(
                            aa => aa.Id == Convert.ToString(skinDataGridView1[e.ColumnIndex, e.RowIndex].Value)).FirstOrDefault();

                    bridgeModelV.superSModelVs[e.RowIndex].BeamType = beamSourceModelV.Type;
                    bridgeModelV.superSModelVs[e.RowIndex].StartBeamHeight = "" + (beamSourceModelV.StartPointPierAddHeight);
                    bridgeModelV.superSModelVs[e.RowIndex].EndBeamHeight = "" + (beamSourceModelV.EndPointPierAddHeight);
                    
                }
            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
