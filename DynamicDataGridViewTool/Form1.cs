using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicDataGridViewTool
{
    public partial class Form1 : Form
    {
        public List<Person> people = new List<Person>();

        public Form1()
        {
            InitializeComponent();
            people.Add(new Person(1, "1", 1, true, 1, 1, "1", "1"));
            people.Add(new Person(2, "2", 2, true, 2, 2, "2", "2"));
            DynamicDataGridViewTool.DynamicDgv.GenerateDynamicDgv<Person>(this.dataGridView1,people);
        }
    }
}
