using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Timers;
using System.Threading;
using System.Reflection;

namespace CsFormProject
{
    internal class DoubleChromoSphere : PictureBox
    {
        private void InitializeComponent()
        {
            Debug.WriteLine(Environment.CurrentDirectory);
        }

        public DoubleChromoSphere(Color color, int posIndex)
        {
            this.color = color;
            if (color == Color.Red)
            {
                ballNum = RedNum; 
                img = Image.FromFile(RedPath);
                offset = RedOffset;
                Delta = new List<int>(RedDelta);
            }
            else
            {
                ballNum = BlueNum;
                img = Image.FromFile(BluePath);
                offset = BlueOffset;
                Delta = new List<int>(BlueDelta);
            }

            frameNum = ballNum * steps;
            frameWidth = img.Width;
            frameHeight = (img.Height) / (ballNum + 1);
            this.posIndex = posIndex;
            this.lastBall = null;
            SetIndexFrame(frameIndex);
            this.SizeMode = PictureBoxSizeMode.Zoom;
            InitializeComponent();
        }

        public DoubleChromoSphere(Color color,int posIndex,DoubleChromoSphere lastBall)
        {
            Debug.WriteLine(Environment.CurrentDirectory);
            this.color = color;
            if (color == Color.Red)
            {
                ballNum = RedNum;
                img = Image.FromFile(RedPath);
                offset = RedOffset;
                Delta = new List<int>(RedDelta);
            }
            else
            {
                ballNum = BlueNum;
                img = Image.FromFile(BluePath);
                offset = BlueOffset;
                Delta = new List<int>(BlueDelta);
            }

            frameNum = ballNum * steps;
            frameWidth = img.Width;
            frameHeight = (img.Height) / (ballNum + 1);
            this.posIndex = posIndex;
            this.lastBall = lastBall;
            SetIndexFrame(frameIndex);
            this.SizeMode = PictureBoxSizeMode.Zoom;
            InitializeComponent();
        }
        
        public void Reset()
        {
            isFinished = false;
            SetIndexFrame(0);
        }

        // 设置当前frameIndex帧的图像
        private void SetIndexFrame(int index)
        {
            //Debug.WriteLine("{0}", index);
            if (index == 0) add = 0;
            // 更新当前球的标号
            if (index % steps == 0)
            {
                ballIndex = (index + steps) / steps;
            }
            else
            {
                ballIndex = 0;
            }
            // 从img中截取一帧并绘制显示
            int start = offset + (int)(index * frameHeight * (1.0 / steps));
            if (Delta.Contains(index*4/steps))
            {
                add++;
            }
            start += add;

            Bitmap bitmap = new Bitmap(frameWidth, frameHeight);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, frameWidth, frameHeight),
                new Rectangle(0, start, frameWidth, frameHeight), GraphicsUnit.Pixel);
            this.Image = bitmap;
            graphics.Dispose();
        }
        
        // 滚动单帧
        private void RollFrame()
        {
            frameIndex = (frameIndex + 1) % frameNum;
            SetIndexFrame(frameIndex);
        }

        // 滚动到stopNum,保证不会在15轮以内停止，并在最后10轮开始减速
        public void Roll()
        {
            // 初始时间间隔
            int interval = 20;

            // 若不是第一个球，在上一个球没有进行开始并停止时需要循环滚动
            while (!choose || (posIndex != 0 && !(lastBall.isFinished)))
            {
                for (int i = 0; i < steps; i++)
                {
                    RollFrame();
                    Thread.Sleep(interval);
                }
            }

            // 当stopNum小于当前的ballIndex时，或大于ballIndex但是相差不超过15轮时增加一轮
            int reStopNum = (stopNum - ballIndex) > 15 ? stopNum : stopNum + ballNum;
            int turns = steps * (reStopNum - ballIndex); //总共需要跑的轮次

            while (turns > 0)
            {
                RollFrame();
                turns--;
                if (turns < 15 * steps)
                {
                    interval = (int)(1.05 * (double)interval);
                }
                Thread.Sleep(interval);
            }
            // 动画停止，杀死当前线程
            isFinished = true;
            Thread.CurrentThread.Abort();

            // 利用 Timer 实现动画 已弃用
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += new ElapsedEventHandler((object sender,
                System.Timers.ElapsedEventArgs e) =>
            {
                RollFrame();
                turns--;
                // 最后15轮开始减速
                if (turns < 15 * steps)
                {
                    timer.Interval *= 1.05;
                }

                if (turns == 0)
                {
                    timer.Stop();
                    Thread.CurrentThread.Abort();
                }

            });
            //timer.Start();
        }

        // 颜色枚举值
        public enum Color { Red, Blue };

        // 用来设置是否按下选择按钮，即开始停止减速
        public static bool choose { get; set; } = false;

        //private static string RedPath = "D:\\lab\\.net lab\\CsFormProject\\CsFormProject\\res\\RedBall.png";
        //private static string BluePath = "D:\\lab\\.net lab\\CsFormProject\\CsFormProject\\res\\BlueBall.png";
        private static string RedPath = "..\\..\\res\\RedBall.png";
        private static string BluePath = "..\\..\\res\\BlueBall.png";
        //移动到下个球需要steps移动，用来控制动画的帧数
        private static int steps { get; } = 4;
        private static int RedNum { get; } = 33;
        private static int BlueNum { get; } = 16;
        private static int RedOffset { get; } = 2;
        private static int BlueOffset { get; } = 4;

        //移动积累产生的位置偏移，需要在特定轮时进行修正
        private static List<int> RedDelta { get; } = new List<int>(new int[] {
            2, 8, 20, 28, 32, 40, 44, 56, 64,
            68, 80, 88, 92, 96, 104, 112, 116
        });

        private static List<int> BlueDelta { get; } = new List<int>(new int[] {
            2,4,8,44
        });

        public int stopNum { get; set; } = 1;          // 停止时球的数字编号
        public int ballNum { get; } = 0;               // 该类球的总数
        private int ballIndex { get; set; } = 0;       // 当前球的数字编号
        private int frameNum { get; } = 0;             // 图像帧的总数
        private int frameIndex { get; set; } = 0;      // 图像帧的编号
        private int offset { get; }                    // 初始修正偏移
        private int add { get; set; } = 0;             // 修正辅助量
        private List<int> Delta { get; }               // 修正偏移量
        private Image img { get; }                     // 整个动画图像
        private Color color { get; }                   // 当前球的颜色
        private int frameWidth { get; }                // 每帧图像的宽度
        private int frameHeight { get; }               // 每帧图像的高度
        private int posIndex { get; } = 0;             // 当前球的位置编号
        private DoubleChromoSphere lastBall { get; }   // 上一个球
        private bool isFinished { get; set; } = false; // 是否被中止
    }
}
