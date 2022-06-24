using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bssc.ViewForms.ResourceMenu;
using Bssc.ViewForms.AffiliateForms;

namespace Bssc.Control.Tools.TreeviewContextMenu
{
    public class ContextMenuWrapForResourceNode
    {

        public ContextMenuStrip mainMenu;

        public ToolStripMenuItem modifyMenuItem;

        public ToolStripMenuItem deleteMenuItem;

        public ToolStripMenuItem addMenuItem;

        public TreeNode node;

        public TreeView tv;

        public ContextMenuWrapForResourceNode(TreeNode node)
        {
            this.node = node;

            this.tv = node.TreeView;

            mainMenu = new ContextMenuStrip();

            modifyMenuItem = new ToolStripMenuItem();

            addMenuItem = new ToolStripMenuItem();

            deleteMenuItem = new ToolStripMenuItem();

            mainMenu.SuspendLayout();
            mainMenu.Items.AddRange(new ToolStripItem[1] { addMenuItem });
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new System.Drawing.Size(100, 120);

            modifyMenuItem.Name = "modifyMenu";
            modifyMenuItem.Size = new System.Drawing.Size(100, 22);
            modifyMenuItem.Text = "修改" + node.Text;

            addMenuItem.Name = "add";
            addMenuItem.Size = new System.Drawing.Size(100, 22);
            addMenuItem.Text = "新建" + node.Text;

            deleteMenuItem.Name = "delete";
            deleteMenuItem.Size = new System.Drawing.Size(100, 22);
            deleteMenuItem.Text = "删除" + node.Text;

            mainMenu.ResumeLayout(performLayout: false);


            setMainMenu();
        }

        public void setMainMenu()
        {
            node.ContextMenuStrip = mainMenu;
            addMenuItem.Click += AddMenuItem_Click;
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            ///此处应做一个警告窗口
            AlertForm alertForm = new AlertForm(node);
            alertForm.skinTextBox1.Text = "您确定删除" + node.Text + "?";
            alertForm.Show();
        }

        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            if (node.Text == "桥墩")
            {
                PierConfigForm pierConfigForm = new PierConfigForm(0,node);
                pierConfigForm.Show();
            }
            else if (node.Text == "主梁")
            {
                BeamConfigForm beamConfigForm = new BeamConfigForm(0,node);
                beamConfigForm.Show();
            }
            if (node.Text == "基础")
            {
                FoundationConfigForm foundationConfigForm = new FoundationConfigForm(0,node);
                foundationConfigForm.Show();
            }
            if (node.Text == "路线")
            {
                RoadConfigForm roadConfigForm = new RoadConfigForm(0,node);
                roadConfigForm.Show();
            }
        }

    }
}
