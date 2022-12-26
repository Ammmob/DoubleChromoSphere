using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Media.Media3D;
using System.CodeDom;

namespace CsFormProject
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
           
            Control.CheckForIllegalCrossThreadCalls = false;
            // 设置窗体固定大小
            this.Text = "双色球随机选号器";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(500, 350);

            // 使得 selector 居中显示
            Debug.WriteLine("h:{0},w:{1}", Height, Width);
            this.selector.Location = new System.Drawing.Point(
               (this.Width - this.selector.Width) / 2, 70);

            // style设计
            this.lbxPool.Font = new Font(FontFamily.GenericSerif, 15);
            this.lbxPool.BackColor = Color.FromArgb(204, 255, 225);
            this.lbxPool.BorderStyle = BorderStyle.None;

            this.btnStart.BackColor = Color.FromArgb(255, 255, 102);
            this.btnChoose.BackColor = Color.FromArgb(255, 255, 102);
            this.btnClear.BackColor = Color.FromArgb(255, 255, 102);
            this.btnBuy.BackColor = Color.FromArgb(255, 255, 102);

            SetWindowRegion();
        }
        
        #region 在标题栏添加按钮效果
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void DisableProcessWindowsGhosting();

        [DllImport("UxTheme.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        Rectangle m_rect = new Rectangle(452, 10, 18, 18);
        protected override void OnHandleCreated(EventArgs e)
        {
            //SetWindowTheme(this.Handle, "", "");
            base.OnHandleCreated(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            //switch (m.Msg)
            //{
            //    case 0x86://WM_NCACTIVATE
            //        goto case 0x85;
            //    case 0x85://WM_NCPAINT
            //        {
                        
            //            IntPtr hDC = GetWindowDC(m.HWnd);
            //            //把DC转换为 .NET的Graphics就可以很方便地使用Framework提供的绘图功能了
            //            Graphics gs = Graphics.FromHdc(hDC);
            //            gs.FillRectangle(new LinearGradientBrush(m_rect, Color.White, Color.White, LinearGradientMode.BackwardDiagonal), m_rect);
            //            StringFormat strFmt = new StringFormat
            //            {
            //                Alignment = StringAlignment.Center,
            //                LineAlignment = StringAlignment.Center
            //            };
            //            gs.DrawString("!", this.Font, Brushes.BlanchedAlmond, m_rect, strFmt);
            //            gs.Dispose();
            //            //释放GDI资源
            //            ReleaseDC(m.HWnd, hDC);
            //            break;
            //        }
            //    case 0xA1://WM_NCLBUTTONDOWN
            //        {
            //            Point mousePoint = new Point((int)m.LParam);
            //            mousePoint.Offset(-this.Left, -this.Top);
            //            if (m_rect.Contains(mousePoint))
            //            {
            //                MessageBox.Show("test");
            //            }
            //            break;
            //        }
            //}
        }
        #endregion

        #region 通过点击移动 panel 移动窗体
        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion

        #region 设置无边框的窗体为圆角区域
        // 设置圆角边框
        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 10);
            this.Region = new Region(FormPath);
        }
        // 绘制圆角矩形曲线
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            // 左上角  
            path.AddArc(arcRect, 180, 90);
            // 右上角  
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            // 右下角  
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            // 左下角  
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线  
            return path;

        }
        #endregion

        #region 关闭按钮的事件处理
        private void ptbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ptbClose_MouseEnter(object sender, EventArgs e)
        {
            this.ptbClose.Image = this.closeHover;
            this.ptbClose.BackColor = Color.Red;
        }

        private void ptbClose_MouseLeave(object sender, EventArgs e)
        {
            this.ptbClose.Image = this.closeNormal;
            this.ptbClose.BackColor = Color.Transparent;
        }
        #endregion

        // 设置启动和清除按钮的状态
        public void SetState(bool enabled)
        {
            btnStart.Enabled = enabled;
            btnClear.Enabled = enabled;
        }

        // 动画停止后 选择池增加号码，按钮状态更新
        public void Stop(string number)
        {
            this.lbxPool.Items.Add(number);
            this.SetState(true);
        }

        // 重写调整大小事件 使得调整大小后保持 selector 水平居中位置
        protected override void OnResize(EventArgs e)
        {
            
            this.selector.Location = new System.Drawing.Point(
                (this.Width - this.selector.Width) / 2+10, 40);
        }

        // 重新即将关闭事件 在关闭前将所有子线程杀死
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.selector.ThreadAbort();
            base.OnClosing(e);
        }

        //重写绘图事件，设置背景色为双色渐变
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Color _Color1 = Color.FromArgb(255, 250, 205);
            Color _Color2 = Color.FromArgb(17, 238, 238);
            float _ColorAngle = 90f;
            Graphics g = pevent.Graphics;

            Rectangle rBackground = new Rectangle(0, 0, this.Width, this.Height);
            //生成线性渐变画刷
            System.Drawing.Drawing2D.LinearGradientBrush bBackground
                = new System.Drawing.Drawing2D.LinearGradientBrush(
                    rBackground, _Color1, _Color2, _ColorAngle);

            g.FillRectangle(bBackground, rBackground);
            bBackground.Dispose();
           
        }

        // 启动按钮 将按钮状态设置为false，并启动selector
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.SetState(false);
            this.selector.Start();
        }

        // 清除按钮 将选择池情况，并将selector清空
        private void btnClear_Click(object sender, EventArgs e)
        {
            lbxPool.Items.Clear();
            this.selector.Clear();
            
        }

        // 选择按钮 设置双色球的选择状态为真
        private void btnChoose_Click(object sender, EventArgs e)
        {
            DoubleChromoSphere.choose = true;
        }

        // 购买按钮 
        private void btnBuy_Click(object sender, EventArgs e)
        {
            // 如果没有选中项直接返回
            if (this.lbxPool.SelectedItem == null)
                return;
            
            string number = this.lbxPool.Text;
            if(MessageBox.Show("您要购买的号码是：\r\n\t"+number,
                "购买确认", MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                // 支付成功将购买号码标记为已购买
                if (PayForm.Show()== DialogResult.OK)
                {
                    Debug.WriteLine("pay success!");
                    number = number.Replace(" ", "");
                    this.selector.BuyNumber(number);
                    this.lbxPool.Items.Remove(this.lbxPool.SelectedItem);
                }
            }
            Debug.WriteLine("pay falied!");
        }


        private void Form_Load(object sender, EventArgs e)
        {
            //this.Text = "";
            //this.ControlBox = false;
        }



    }
}
