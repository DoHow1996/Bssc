using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.ViewForms.AffiliateForms
{
    public partial class showdatagridview : Form
    {
        public List<PqxSourceModelV> pqxSourceModelVs;


        public showdatagridview(List<PqxSourceModelV> pqxSourceModelVs)
        {
            InitializeComponent();

            this.pqxSourceModelVs = pqxSourceModelVs;
            this.dataGridView1.DataSource = pqxSourceModelVs;

        }




    }
}
