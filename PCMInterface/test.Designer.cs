﻿namespace PCMInterface
{
    partial class test
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainForm1 = new PCMInterface.MainForm();
            this.SuspendLayout();
            // 
            // mainForm1
            // 
            this.mainForm1.ext_name = "para";
            this.mainForm1.Location = new System.Drawing.Point(4, 4);
            this.mainForm1.Name = "mainForm1";
            this.mainForm1.origin_file = "Please select a file";
            this.mainForm1.Size = new System.Drawing.Size(998, 784);
            this.mainForm1.sub_f_name = null;
            this.mainForm1.TabIndex = 0;
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainForm1);
            this.Name = "test";
            this.Size = new System.Drawing.Size(1006, 822);
            this.ResumeLayout(false);

        }

        #endregion

        private MainForm mainForm1;




    }
}
