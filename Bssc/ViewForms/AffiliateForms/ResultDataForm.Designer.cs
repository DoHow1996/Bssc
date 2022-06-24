namespace Bssc.ViewForms.AffiliateForms
{
    partial class ResultDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("上部结构");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("支座系统");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("下部结构");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("总体设计", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("基础");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("桥墩");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("主梁");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("资源", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("支座数据一览表");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("支座数量表");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("上垫石钢筋数量表");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("下垫石钢筋数量表");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("桩基坐标表");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("桥墩结构数据表");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("总体参数表");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("设计成果", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.skinTreeView1 = new CCWin.SkinControl.SkinTreeView();
            this.skinPanel = new CCWin.SkinControl.SkinPanel();
            this.skinDataGridView = new CCWin.SkinControl.SkinDataGridView();
            this.skinPanel1.SuspendLayout();
            this.skinPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.skinTreeView1);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(0, 0);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(200, 510);
            this.skinPanel1.TabIndex = 0;
            // 
            // skinTreeView1
            // 
            this.skinTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTreeView1.ItemHeight = 20;
            this.skinTreeView1.Location = new System.Drawing.Point(0, 0);
            this.skinTreeView1.Name = "skinTreeView1";
            treeNode1.Name = "节点1";
            treeNode1.Text = "上部结构";
            treeNode2.Name = "节点2";
            treeNode2.Text = "支座系统";
            treeNode3.Name = "节点3";
            treeNode3.Text = "下部结构";
            treeNode4.Name = "节点0";
            treeNode4.Text = "总体设计";
            treeNode5.Name = "节点7";
            treeNode5.Text = "基础";
            treeNode6.Name = "节点8";
            treeNode6.Text = "桥墩";
            treeNode7.Name = "节点9";
            treeNode7.Text = "主梁";
            treeNode8.Name = "节点4";
            treeNode8.Text = "资源";
            treeNode9.Name = "节点11";
            treeNode9.Text = "支座数据一览表";
            treeNode10.Name = "节点12";
            treeNode10.Text = "支座数量表";
            treeNode11.Name = "节点13";
            treeNode11.Text = "上垫石钢筋数量表";
            treeNode12.Name = "节点14";
            treeNode12.Text = "下垫石钢筋数量表";
            treeNode13.Name = "节点15";
            treeNode13.Text = "桩基坐标表";
            treeNode14.Name = "节点16";
            treeNode14.Text = "桥墩结构数据表";
            treeNode15.Name = "节点17";
            treeNode15.Text = "总体参数表";
            treeNode16.Name = "节点10";
            treeNode16.Text = "设计成果";
            this.skinTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8,
            treeNode16});
            this.skinTreeView1.Size = new System.Drawing.Size(200, 510);
            this.skinTreeView1.TabIndex = 0;
            this.skinTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.skinTreeView1_AfterSelect);
            this.skinTreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.skinTreeView1_NodeMouseClick);
            // 
            // skinPanel
            // 
            this.skinPanel.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel.Controls.Add(this.skinDataGridView);
            this.skinPanel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPanel.DownBack = null;
            this.skinPanel.Location = new System.Drawing.Point(200, 0);
            this.skinPanel.MouseBack = null;
            this.skinPanel.Name = "skinPanel";
            this.skinPanel.NormlBack = null;
            this.skinPanel.Size = new System.Drawing.Size(1093, 510);
            this.skinPanel.TabIndex = 1;
            // 
            // skinDataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.skinDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.skinDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.skinDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.skinDataGridView.ColumnFont = null;
            this.skinDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.skinDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.skinDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skinDataGridView.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.skinDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.skinDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinDataGridView.EnableHeadersVisualStyles = false;
            this.skinDataGridView.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.skinDataGridView.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinDataGridView.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.skinDataGridView.Location = new System.Drawing.Point(0, 0);
            this.skinDataGridView.Name = "skinDataGridView";
            this.skinDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.skinDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.skinDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.skinDataGridView.RowTemplate.Height = 23;
            this.skinDataGridView.Size = new System.Drawing.Size(1093, 510);
            this.skinDataGridView.TabIndex = 0;
            this.skinDataGridView.TitleBack = null;
            this.skinDataGridView.TitleBackColorBegin = System.Drawing.Color.White;
            this.skinDataGridView.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // ResultDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 510);
            this.Controls.Add(this.skinPanel);
            this.Controls.Add(this.skinPanel1);
            this.Name = "ResultDataForm";
            this.Text = "ResultDataForm";
            this.skinPanel1.ResumeLayout(false);
            this.skinPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinTreeView skinTreeView1;
        private CCWin.SkinControl.SkinPanel skinPanel;
        private CCWin.SkinControl.SkinDataGridView skinDataGridView;
    }
}