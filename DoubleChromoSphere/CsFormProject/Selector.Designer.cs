using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace CsFormProject
{
    partial class Selector
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
   
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
            this.SuspendLayout();
            // 
            // Selector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "Selector";
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.IContainer components = null;
      
        // 子线程 用来进行球的滚动动画
        private Thread[] threads { get; set; }
        // 计时器 用来判断是否所有球都停止
        private System.Timers.Timer timer;
        // 双色球
        private DoubleChromoSphere[] balls { get; set; }
        // 红球集合 避免出现相同的红球
        private HashSet<int> RedBallSet { get; set; }
        // 号码选择映射 用来避免生成已选择号码或已购买号码
        private Dictionary<string, numberState> NumberDict { get; set; }

        // 号码状态的枚举值
        public enum numberState { choosed, purchased };
        // 当前选择的号码
        public string number { get;private set; } = "";

    }
}
