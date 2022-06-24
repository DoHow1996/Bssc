namespace Bssc.ViewForms
{
    partial class MainTreeView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainTreeView));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("工程名");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("基础");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("桥墩");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("主梁");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("路线");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("资源", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.skinToolStrip1 = new CCWin.SkinControl.SkinToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.自由分跨ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分跨ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.导入地勘资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinTreeView1 = new CCWin.SkinControl.SkinTreeView();
            this.skinToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // skinToolStrip1
            // 
            this.skinToolStrip1.Arrow = System.Drawing.Color.Black;
            this.skinToolStrip1.Back = System.Drawing.Color.White;
            this.skinToolStrip1.BackRadius = 4;
            this.skinToolStrip1.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.skinToolStrip1.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.skinToolStrip1.BaseFore = System.Drawing.Color.Black;
            this.skinToolStrip1.BaseForeAnamorphosis = false;
            this.skinToolStrip1.BaseForeAnamorphosisBorder = 4;
            this.skinToolStrip1.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.skinToolStrip1.BaseForeOffset = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.BaseHoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.BaseItemAnamorphosis = true;
            this.skinToolStrip1.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemBorderShow = true;
            this.skinToolStrip1.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemDown")));
            this.skinToolStrip1.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("skinToolStrip1.BaseItemMouse")));
            this.skinToolStrip1.BaseItemNorml = null;
            this.skinToolStrip1.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BaseItemRadius = 4;
            this.skinToolStrip1.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.BindTabControl = null;
            this.skinToolStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.skinToolStrip1.Fore = System.Drawing.Color.Black;
            this.skinToolStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.skinToolStrip1.HoverFore = System.Drawing.Color.White;
            this.skinToolStrip1.ItemAnamorphosis = true;
            this.skinToolStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemBorderShow = true;
            this.skinToolStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.skinToolStrip1.ItemRadius = 4;
            this.skinToolStrip1.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3});
            this.skinToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.skinToolStrip1.Name = "skinToolStrip1";
            this.skinToolStrip1.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.skinToolStrip1.Size = new System.Drawing.Size(226, 25);
            this.skinToolStrip1.SkinAllColor = true;
            this.skinToolStrip1.TabIndex = 0;
            this.skinToolStrip1.Text = "skinToolStrip1";
            this.skinToolStrip1.TitleAnamorphosis = true;
            this.skinToolStrip1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.skinToolStrip1.TitleRadius = 4;
            this.skinToolStrip1.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登录ToolStripMenuItem,
            this.新建工程ToolStripMenuItem,
            this.打开工程ToolStripMenuItem,
            this.数据保存ToolStripMenuItem,
            this.上传数据ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // 登录ToolStripMenuItem
            // 
            this.登录ToolStripMenuItem.Name = "登录ToolStripMenuItem";
            this.登录ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.登录ToolStripMenuItem.Text = "登录";
            // 
            // 新建工程ToolStripMenuItem
            // 
            this.新建工程ToolStripMenuItem.Name = "新建工程ToolStripMenuItem";
            this.新建工程ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新建工程ToolStripMenuItem.Text = "新建工程";
            this.新建工程ToolStripMenuItem.Click += new System.EventHandler(this.新建工程ToolStripMenuItem_Click);
            // 
            // 打开工程ToolStripMenuItem
            // 
            this.打开工程ToolStripMenuItem.Name = "打开工程ToolStripMenuItem";
            this.打开工程ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打开工程ToolStripMenuItem.Text = "打开工程";
            // 
            // 数据保存ToolStripMenuItem
            // 
            this.数据保存ToolStripMenuItem.Name = "数据保存ToolStripMenuItem";
            this.数据保存ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.数据保存ToolStripMenuItem.Text = "数据保存";
            this.数据保存ToolStripMenuItem.Click += new System.EventHandler(this.数据保存ToolStripMenuItem_Click);
            // 
            // 上传数据ToolStripMenuItem
            // 
            this.上传数据ToolStripMenuItem.Name = "上传数据ToolStripMenuItem";
            this.上传数据ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.上传数据ToolStripMenuItem.Text = "上传数据";
            this.上传数据ToolStripMenuItem.Click += new System.EventHandler(this.上传数据ToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自由分跨ToolStripMenuItem,
            this.分跨ToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            // 
            // 自由分跨ToolStripMenuItem
            // 
            this.自由分跨ToolStripMenuItem.Name = "自由分跨ToolStripMenuItem";
            this.自由分跨ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.自由分跨ToolStripMenuItem.Text = "绘制道路中心线";
            this.自由分跨ToolStripMenuItem.Click += new System.EventHandler(this.自由分跨ToolStripMenuItem_Click);
            // 
            // 分跨ToolStripMenuItem
            // 
            this.分跨ToolStripMenuItem.Name = "分跨ToolStripMenuItem";
            this.分跨ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.分跨ToolStripMenuItem.Text = "分跨";
            this.分跨ToolStripMenuItem.Click += new System.EventHandler(this.分跨ToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入地勘资料ToolStripMenuItem});
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton3.Text = "toolStripDropDownButton3";
            // 
            // 导入地勘资料ToolStripMenuItem
            // 
            this.导入地勘资料ToolStripMenuItem.Name = "导入地勘资料ToolStripMenuItem";
            this.导入地勘资料ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.导入地勘资料ToolStripMenuItem.Text = "导入地勘资料";
            this.导入地勘资料ToolStripMenuItem.Click += new System.EventHandler(this.导入地勘资料ToolStripMenuItem_Click);
            // 
            // skinTreeView1
            // 
            this.skinTreeView1.BackColor = System.Drawing.SystemColors.Window;
            this.skinTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTreeView1.ItemHeight = 20;
            this.skinTreeView1.Location = new System.Drawing.Point(0, 25);
            this.skinTreeView1.Name = "skinTreeView1";
            treeNode1.Name = "projectName";
            treeNode1.Text = "工程名";
            treeNode2.Name = "foundations";
            treeNode2.Text = "基础";
            treeNode3.Name = "piers";
            treeNode3.Text = "桥墩";
            treeNode4.Name = "beams";
            treeNode4.Text = "主梁";
            treeNode5.Name = "roadlines";
            treeNode5.Text = "路线";
            treeNode6.Name = "resource";
            treeNode6.Text = "资源";
            this.skinTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6});
            this.skinTreeView1.Size = new System.Drawing.Size(226, 771);
            this.skinTreeView1.TabIndex = 1;
            this.skinTreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.skinTreeView1_NodeMouseDoubleClick);
            // 
            // MainTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.skinTreeView1);
            this.Controls.Add(this.skinToolStrip1);
            this.Name = "MainTreeView";
            this.Size = new System.Drawing.Size(226, 796);
            this.skinToolStrip1.ResumeLayout(false);
            this.skinToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinToolStrip skinToolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem 自由分跨ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem 导入地勘资料ToolStripMenuItem;
        private CCWin.SkinControl.SkinTreeView skinTreeView1;
        private System.Windows.Forms.ToolStripMenuItem 分跨ToolStripMenuItem;
    }
}
