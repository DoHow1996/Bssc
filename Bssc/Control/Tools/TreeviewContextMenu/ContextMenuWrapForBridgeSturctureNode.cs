using Bssc.ViewForms.AffiliateForms;
using Bssc.ViewForms.ProjectMenuForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.Tools.TreeviewContextMenu
{
    public class ContextMenuWrapForBridgeSturctureNode
    {

        public ContextMenuStrip mainMenu;

        public ToolStripMenuItem modifyMenuItem;

        public ToolStripMenuItem deleteMenuItem;

        public ToolStripMenuItem addMenuItem;

        public TreeNode node;

        public TreeView tv;

        public ContextMenuWrapForBridgeSturctureNode(TreeNode node)
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
            
            modifyMenuItem.Text = "编辑" + node.Text;
            

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
            switch (node.Text)
            {
                case "上部结构":
                    SupersForm supersForm = new SupersForm(node);
                    supersForm.Show();
                    break;
                case "支座系统":
                    SupportsForm supportsForm = new SupportsForm(node);
                    supportsForm.Show();
                    break;
                case "下部结构":
                    SubsForm subsForm = new SubsForm(node);
                    subsForm.Show();
                    break;
                case "总体成果":
                    ResultDataForm resultDataForm = new ResultDataForm(node.Parent);
                    resultDataForm.Show();
                    break;
                default:
                    break;
            }
        }


    }
}
