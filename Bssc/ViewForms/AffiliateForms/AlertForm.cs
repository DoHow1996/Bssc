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

namespace Bssc.ViewForms.AffiliateForms
{
    public partial class AlertForm : Skin_Mac
    {

        TreeNode node;

        public AlertForm(TreeNode node)
        {
            InitializeComponent();
            this.node = node;
        }

        public AlertForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (node != null)
            {
                node.Remove();
            }
            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
