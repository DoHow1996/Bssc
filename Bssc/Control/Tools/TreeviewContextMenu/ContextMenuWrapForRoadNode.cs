using Bssc.ViewForms.ProjectMenuForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.Tools.TreeviewContextMenu
{
    public class ContextMenuWrapForRoadNode
    {
        public ContextMenuStrip mainMenu;

        public ToolStripMenuItem modifyMenuItem;

        public ToolStripMenuItem deleteMenuItem;

        public ToolStripMenuItem addMenuItem;

        public TreeNode node;

        public TreeView tv;

        public ContextMenuWrapForRoadNode(TreeNode node)
        {

            this.node = node;

            this.tv = node.TreeView;

            mainMenu = new ContextMenuStrip();

            modifyMenuItem = new ToolStripMenuItem();

            addMenuItem = new ToolStripMenuItem();

            deleteMenuItem = new ToolStripMenuItem();

            mainMenu.SuspendLayout();
            mainMenu.Items.AddRange(new ToolStripItem[2] { addMenuItem, modifyMenuItem});
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new System.Drawing.Size(100, 120);

            modifyMenuItem.Name = "modifyMenu";
            modifyMenuItem.Size = new System.Drawing.Size(100, 22);
            modifyMenuItem.Text = "修改路线";

            addMenuItem.Name = "add";
            addMenuItem.Size = new System.Drawing.Size(100, 22);
            addMenuItem.Text = "新建桥梁";

            deleteMenuItem.Name = "delete";
            deleteMenuItem.Size = new System.Drawing.Size(100, 22);
            deleteMenuItem.Text = "删除路线";

            mainMenu.ResumeLayout(performLayout: false);


            setMainMenu();
        }

        public void setMainMenu()
        {
            node.ContextMenuStrip = mainMenu;
            modifyMenuItem.Click += ModifyMenuItem_Click;
            addMenuItem.Click += AddMenuItem_Click;
        }

        private void ModifyMenuItem_Click(object sender, EventArgs e)
        {
            RoadForm newRoadForm = new RoadForm(1,node);
            newRoadForm.Show();

        }

        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            BridgeForm newBridgeForm = new BridgeForm(0,node);
            newBridgeForm.Show();
        }

    }
}
