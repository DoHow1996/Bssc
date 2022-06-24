using Bssc.Control.CadControl;
using Bssc.Models.ModelsV;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using BaseLibrary.ExtensionMethod;
using Autodesk.AutoCAD.EditorInput;
using Bssc.Models.ModelsV.SourceModelsV;

using BaseLibrary.ResultData;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Bssc.ViewForms.AffiliateForms
{
    public partial class PlotPqxForm : Skin_Mac
    {
        public PlotPqxForm()
        {
            InitializeComponent();
            GlobalInitialize();
        }

        private void GlobalInitialize()
        {
            var roadSourceModelvs = GlobalData.sourceModelV.roadSourceModelVs;
            skinComboBox1.DataSource = roadSourceModelvs;
            skinComboBox1.DisplayMember = "Designation";
            skinComboBox1.ValueMember = "Id";
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            var roadSourceModelVs = GlobalData.sourceModelV.roadSourceModelVs;
            RoadSourceModelV roadSourceModelV = roadSourceModelVs.Where(aa => aa.Id == Convert.ToString(skinComboBox1.SelectedValue)).FirstOrDefault();
            this.Close();
            Application.DocumentManager.MdiActiveDocument.DrawPqx(roadSourceModelV);
        }
    }
}
