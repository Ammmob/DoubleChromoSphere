using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace CsFormProject
{
    public partial class Selector : UserControl
    {
        public Selector()
        {
            InitializeComponent();

            // 变量初始化
            threads = new Thread[7];
            balls = new DoubleChromoSphere[7];
            RedBallSet = new HashSet<int>();
            NumberDict = new Dictionary<string, numberState>();
            balls[0] = new DoubleChromoSphere(DoubleChromoSphere.Color.Red, 0);
            for (int i = 1; i < 6; i++)
            {
                balls[i] = new DoubleChromoSphere(DoubleChromoSphere.Color.Red, i, balls[i - 1]);
            }
            balls[6] = new DoubleChromoSphere(DoubleChromoSphere.Color.Blue, 6, balls[5]);

            // 初始化定时器

            timer = new System.Timers.Timer(100);
            timer.Elapsed += new ElapsedEventHandler((object sender,
                System.Timers.ElapsedEventArgs e) =>
            {
                // 最后一个线程死亡，将数字加到父亲窗口的选择池中
                if (!threads[6].IsAlive && this.number != "") 
                {
                    ((Form)this.Parent).Stop(number);
                    //((Form)this.Parent).lbxPool.Items.Add(number);
                    //((Form)this.Parent).SetState(true);
                    timer.Stop();
                }
            });

            // 界面布局
            int sep = 5;
            this.Size = new System.Drawing.Size(380, 50);
            Debug.WriteLine("width:{0},height{1}",this.Width,this.Height);
            int width = (this.Width - 6 * sep) / 7;
            for (int i = 0; i < 7; i++)
            {
                balls[i].Location = new System.Drawing.Point(i * (width+sep), 0);
                balls[i].Size = new System.Drawing.Size(width, width);
                balls[i].Name = String.Format("ball_{0}", i);
                this.Controls.Add(balls[i]);
            }

        }

        // 计算当前号码与号码池里的号码相同比例
        private double similarRate(int len,int num)
        {
            double rate = 0;
            string sub = this.number.Replace(" ","") + string.Format("{0:d2}", num);
            Debug.WriteLine("index:{0}", len);
            Debug.WriteLine(sub);
            foreach (KeyValuePair<string,numberState> it in NumberDict)
            {
                Debug.WriteLine(it.Key.Substring(0, 2 * len));
                if (it.Key.Substring(0, 2 * len) == sub)
                {
                    rate = len / 7.0;
                    break;
                }
            }
            // 如果没有相同则为0
            Debug.WriteLine("rate:{0}",rate);
            return rate;
        }

        // 号码生成，概率避免算法
        private void numberGenerate()
        {
            Debug.WriteLine("numbr generating:");
            Random random = new Random();
            // 重新生成当前数字的概率
            double resetProbability = 0;
            // 前6个红球的号码生成
            for (int i = 0; i < 6; i++)
            {
                balls[i].stopNum = random.Next(1, balls[i].ballNum + 1);
                resetProbability = similarRate(i + 1, balls[i].stopNum);
                resetProbability = RedBallSet.Contains(balls[i].stopNum) ? 1 : resetProbability;
               
                // 如果生成的小数小于当前重置概率则重新生成
                while (random.NextDouble()<resetProbability)
                {
                    balls[i].stopNum = random.Next(1, balls[i].ballNum + 1);
                    // 如果和前面数字重复，则设置重置概率为1
                    resetProbability = similarRate(i + 1, balls[i].stopNum);
                    resetProbability = RedBallSet.Contains(balls[i].stopNum) ? 1 : resetProbability;
                }
                number += string.Format("{0:d2} ", balls[i].stopNum);
                RedBallSet.Add(balls[i].stopNum);
            }
            balls[6].stopNum = random.Next(1, balls[6].ballNum + 1);
            resetProbability = similarRate(6 + 1, balls[6].stopNum);
            while (random.NextDouble() < resetProbability)
            {
                balls[6].stopNum = random.Next(1, balls[6].ballNum + 1);
                resetProbability = similarRate(6 + 1, balls[6].stopNum);
            }
            number += string.Format("{0:d2} ", balls[6].stopNum);
            RedBallSet.Add(balls[6].stopNum);
        }

        public void Start()
        {
            // 重置
            Reset();

            // 生成号码
            numberGenerate();

            // 开启子线程
            for(int i = 0; i < 7; i++)
            {
                threads[i] = new Thread(balls[i].Roll);
                threads[i].Start();
            }
         
            NumberDict.Add(number.Replace(" ",""), numberState.choosed);

            // 利用 Timer 生成的线程进行检查是否停止，由于是跨线程操作，需要提前关闭跨线程检查
            timer.Start();
        }

        // 重置
        public void Reset()
        {
            DoubleChromoSphere.choose = false;
            RedBallSet.Clear();
            number = "";
            for (int i = 0; i < 7; i++)
            {
                balls[i].Reset();
            }
        }

        // 杀死所有子线程
        public void ThreadAbort()
        {
            if (this.timer.Enabled)
            {
                this.timer.Stop();
            }
            for (int i = 0; i < 7; i++)
            {
                if (this.threads[i] != null && this.threads[i].IsAlive) 
                {
                    this.threads[i].Abort();
                }
            }
        }

        // 清除选择池和当前号码
        public void Clear()
        {
            Reset();
            // 将选择池中的号码全部清楚，同时保留已经购买号码
            HashSet<string> deleteNumber = new HashSet<string>();
            foreach (KeyValuePair<string, numberState> it in NumberDict)
            {
                if (it.Value == numberState.choosed)
                {
                    deleteNumber.Add(it.Key);
                }
            }

            foreach(string key in deleteNumber)
            {
                NumberDict.Remove(key);
            }
        }

        // 将NumberDict 中number映射的修改为1
        public void BuyNumber(string number) 
        {
            NumberDict[number] = numberState.purchased;
        }
    }
}
