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
    public partial class SelectSoilLayer : Form
    {
        public List<string> strs;

        public string s1 { get; set; }
        public string s2 { get; set; }
        public string s3 { get; set; }



        public SelectSoilLayer(List<string> strs)
        {
            InitializeComponent();
            this.strs = strs;

            BindingSource bs1 = new BindingSource();
            bs1.DataSource = strs;
            comboBox1.DataSource = bs1;

            BindingSource bs2 = new BindingSource();
            bs2.DataSource = strs;
            comboBox2.DataSource = bs2;

            BindingSource bs3 = new BindingSource();
            bs3.DataSource = strs;
            comboBox3.DataSource = bs3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s1 = comboBox1.Text;
            s2 = comboBox2.Text;
            s3 = comboBox3.Text;
            this.Close();
        }
    }
}
