using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TstForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Column1.HeaderText = "";
            Column1.ReadOnly = true;
            Column1.Frozen = true;
            Column1.Width = 30;
            Column1.Visible = true;
        }
    }
}
