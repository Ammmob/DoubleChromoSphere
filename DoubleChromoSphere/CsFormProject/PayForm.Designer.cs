using System.Windows.Controls;
using System.Windows.Forms;

namespace CsFormProject
{
    partial class PayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayForm));
            this.btnSuccess = new System.Windows.Forms.Button();
            this.btnFail = new System.Windows.Forms.Button();
            this.rtbExplain = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnSuccess
            // 
            this.btnSuccess.Location = new System.Drawing.Point(359, 289);
            this.btnSuccess.Name = "btnSuccess";
            this.btnSuccess.Size = new System.Drawing.Size(111, 41);
            this.btnSuccess.TabIndex = 0;
            this.btnSuccess.TabStop = false;
            this.btnSuccess.Text = "支付成功";
            this.btnSuccess.UseCompatibleTextRendering = true;
            this.btnSuccess.UseVisualStyleBackColor = true;
            this.btnSuccess.Click += new System.EventHandler(this.btnSuccess_Click);
            // 
            // btnFail
            // 
            this.btnFail.Location = new System.Drawing.Point(507, 289);
            this.btnFail.Name = "btnFail";
            this.btnFail.Size = new System.Drawing.Size(111, 41);
            this.btnFail.TabIndex = 1;
            this.btnFail.Text = "支付失败";
            this.btnFail.UseVisualStyleBackColor = true;
            this.btnFail.Click += new System.EventHandler(this.btnFail_Click);
            // 
            // rtbExplain
            // 
            this.rtbExplain.BackColor = System.Drawing.SystemColors.Window;
            this.rtbExplain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbExplain.Location = new System.Drawing.Point(327, 12);
            this.rtbExplain.Name = "rtbExplain";
            this.rtbExplain.Size = new System.Drawing.Size(302, 271);
            this.rtbExplain.TabIndex = 2;
            this.rtbExplain.Text = "";
            // 
            // PayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 359);
            this.Controls.Add(this.rtbExplain);
            this.Controls.Add(this.btnFail);
            this.Controls.Add(this.btnSuccess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PayForm";
            this.Text = "扫码支付";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSuccess;
        private System.Windows.Forms.Button btnFail;
        private System.Windows.Forms.RichTextBox rtbExplain;
        private DialogResult result { get; set; } = DialogResult.None;
    }
}