using Bssc.ViewForms.ProjectMenuForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.Tools.TreeviewContextMenu
{
    public class ContextMenuWrapForBridgeNode
    {

        public ContextMenuStrip mainMenu;

        public ToolStripMenuItem modifyMenuItem;

        public ToolStripMenuItem deleteMenuItem;

        public ToolStripMenuItem addMenuItem;

        public TreeNode node;

        public TreeView tv;

        public ContextMenuWrapForBridgeNode(TreeNode node)
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
            modifyMenuItem.Text = "修改桥梁";


            mainMenu.ResumeLayout(performLayout: false);


            setMainMenu();
        }

        public void setMainMenu()
        {
            node.ContextMenuStrip = mainMenu;
            modifyMenuItem.Click += ModifyMenuItem_Click;
        }

        private void ModifyMenuItem_Click(object sender, EventArgs e)
        {
            BridgeForm newBridgeForm = new BridgeForm(1,node);
            newBridgeForm.Show();
        }


    }
}
