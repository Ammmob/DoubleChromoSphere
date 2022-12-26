using System.CodeDom;
using System.Drawing;
namespace CsFormProject
{
    partial class Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lbxPool = new System.Windows.Forms.ListBox();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.ptbClose = new System.Windows.Forms.PictureBox();
            this.ptbIcon = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.selector = new CsFormProject.Selector();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(106, 169);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 55);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(256, 169);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(93, 55);
            this.btnChoose.TabIndex = 3;
            this.btnChoose.Text = "选择";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(406, 169);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 55);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.Location = new System.Drawing.Point(556, 169);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(93, 55);
            this.btnBuy.TabIndex = 5;
            this.btnBuy.Text = "确认购买";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lbxPool
            // 
            this.lbxPool.FormattingEnabled = true;
            this.lbxPool.ItemHeight = 18;
            this.lbxPool.Location = new System.Drawing.Point(103, 252);
            this.lbxPool.Name = "lbxPool";
            this.lbxPool.Size = new System.Drawing.Size(543, 238);
            this.lbxPool.TabIndex = 6;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.pnlTitle.Controls.Add(this.ptbClose);
            this.pnlTitle.Controls.Add(this.ptbIcon);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(917, 40);
            this.pnlTitle.TabIndex = 7;
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            this.pnlTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseMove);
            // 
            // ptbClose
            // 
            this.ptbClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.ptbClose.Image = ((System.Drawing.Image)(resources.GetObject("ptbClose.Image")));
            this.ptbClose.InitialImage = ((System.Drawing.Image)(resources.GetObject("ptbClose.InitialImage")));
            this.ptbClose.Location = new System.Drawing.Point(881, 0);
            this.ptbClose.Name = "ptbClose";
            this.ptbClose.Size = new System.Drawing.Size(36, 40);
            this.ptbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbClose.TabIndex = 3;
            this.ptbClose.TabStop = false;
            this.ptbClose.Click += new System.EventHandler(this.ptbClose_Click);
            this.ptbClose.MouseEnter += new System.EventHandler(this.ptbClose_MouseEnter);
            this.ptbClose.MouseLeave += new System.EventHandler(this.ptbClose_MouseLeave);
            // 
            // ptbIcon
            // 
            this.ptbIcon.Image = ((System.Drawing.Image)(resources.GetObject("ptbIcon.Image")));
            this.ptbIcon.InitialImage = ((System.Drawing.Image)(resources.GetObject("ptbIcon.InitialImage")));
            this.ptbIcon.Location = new System.Drawing.Point(12, 3);
            this.ptbIcon.Name = "ptbIcon";
            this.ptbIcon.Size = new System.Drawing.Size(31, 33);
            this.ptbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbIcon.TabIndex = 2;
            this.ptbIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(49, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(185, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "双色球随机选号器";
            // 
            // selector
            // 
            this.selector.BackColor = System.Drawing.Color.Transparent;
            this.selector.Location = new System.Drawing.Point(99, 66);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(600, 120);
            this.selector.TabIndex = 2;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 550);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.lbxPool);
            this.Controls.Add(this.btnBuy);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.selector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.Form_Load);
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Selector selector;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.ListBox lbxPool;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox ptbIcon;
        private System.Windows.Forms.PictureBox ptbClose;
        private Point downPoint;
        //private Image closeNormal = Image.FromFile("C:\\Users\\XCDN\\Desktop\\CsFormProject\\CsFormProject\\res\\close0.png");
        //private Image closeHover = Image.FromFile("C:\\Users\\XCDN\\Desktop\\CsFormProject\\CsFormProject\\res\\close1.png");
        private Image closeNormal = Image.FromFile("..\\..\\res\\close0.png");
        private Image closeHover = Image.FromFile("..\\..\\res\\close1.png");
    }
}

