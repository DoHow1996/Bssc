
using Bssc.ViewForms.AffiliateForms;
using Bssc.ViewForms.ResourceMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.Tools.TreeviewContextMenu
{
    public class ContextMenuWrapForSingleSourceNode
    {

        public ContextMenuStrip mainMenu;

        public ToolStripMenuItem modifyMenuItem;

        public ToolStripMenuItem deleteMenuItem;

        public ToolStripMenuItem addMenuItem;

        public TreeNode node;

        public TreeView tv;

        public ContextMenuWrapForSingleSourceNode(TreeNode node)
        {
            this.node = node;

            this.tv = node.TreeView;

            mainMenu = new ContextMenuStrip();

            modifyMenuItem = new ToolStripMenuItem();

            addMenuItem = new ToolStripMenuItem();

            deleteMenuItem = new ToolStripMenuItem();

            mainMenu.SuspendLayout();
            mainMenu.Items.AddRange(new ToolStripItem[1] { modifyMenuItem});
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
            modifyMenuItem.Click += ModifyMenuItem_Click;
            //deleteMenuItem.Click += DeleteMenuItem_Click;
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            ///此处应做一个警告窗口
            AlertForm alertForm = new AlertForm(node);
            alertForm.skinTextBox1.Text = "您确定删除" + node.Text + "?";
            alertForm.Show();
        }



        private void ModifyMenuItem_Click(object sender, EventArgs e)
        {

            if (node.Name.Contains("foundation"))
            {
                FoundationConfigForm foundationConfigForm = new FoundationConfigForm(1, node);
                foundationConfigForm.Show();
            }
            else if (node.Name.Contains("beam"))
            {
                BeamConfigForm beamConfigForm = new BeamConfigForm(1,node);
                beamConfigForm.Show();
            }
            else if (node.Name.Contains("pier"))
            {
                PierConfigForm pierConfigForm = new PierConfigForm(1,node);
                pierConfigForm.Show();
            }
            else if (node.Name.Contains("roadline"))
            {
                RoadConfigForm roadConfigForm = new RoadConfigForm(1, node);
                roadConfigForm.Show();
            }
        }
    }
}
