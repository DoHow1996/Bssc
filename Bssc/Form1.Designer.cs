namespace Bssc
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainTreeView2 = new Bssc.ViewForms.MainTreeView();
            this.SuspendLayout();
            // 
            // mainTreeView2
            // 
            this.mainTreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTreeView2.Location = new System.Drawing.Point(4, 32);
            this.mainTreeView2.Name = "mainTreeView2";
            this.mainTreeView2.Size = new System.Drawing.Size(263, 715);
            this.mainTreeView2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 751);
            this.Controls.Add(this.mainTreeView2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Bssc";
            this.ResumeLayout(false);

        }

        #endregion

        private ViewForms.MainTreeView mainTreeView1;
        private ViewForms.MainTreeView mainTreeView2;
    }
}

