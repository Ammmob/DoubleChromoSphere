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
using System.Timers;
using System.Threading;

namespace CsFormProject
{
    public partial class PayForm : System.Windows.Forms.Form
    {
        public PayForm()
        {
            InitializeComponent();
            Debug.WriteLine(Environment.CurrentDirectory);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // 设置固定大小
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(450, 280);

            // 设置背景图片
            string path = "../../res/pay.png";
            this.BackgroundImageLayout = ImageLayout.None;
            this.BackgroundImage = Image.FromFile(path);

            // 设置富文本框的内容
            this.rtbExplain.Font = new Font(this.rtbExplain.Font.FontFamily, 10);
            string orderNum = "cz" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            string warm = "   未扣款，请重新扫描支付\r\n" +
                "   已扣款，切勿重复交费，系统\r\n" +
                "   会在1-2个工作日内对账处理";
            this.rtbExplain.Text = "1、请打开手机微信扫一扫功能，" +
                "扫描左侧二维码以完成支付\r\n" +
                "2、请记录订单号" + orderNum + ",以便出现问题核对使用\r\n" +
                "3、如果支付失败，请确认微信是否已扣款\r\n" + warm;
               
            this.rtbExplain.SelectionStart = this.rtbExplain.Find(orderNum);
            this.rtbExplain.SelectionLength = orderNum.Length;
            this.rtbExplain.SelectionColor = Color.Orange;

            this.rtbExplain.SelectionStart = this.rtbExplain.Find("未");
            this.rtbExplain.SelectionLength = warm.Length;
            this.rtbExplain.SelectionColor = Color.Red;
        }

        public static new DialogResult Show()
        {
            PayForm pform = new PayForm();
            pform.result = DialogResult.None;
            pform.ShowDialog();
            return pform.result;
        }

        private void btnSuccess_Click(object sender, EventArgs e)
        {
            this.result = DialogResult.OK;
            this.Close();
        }

        private void btnFail_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
