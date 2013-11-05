namespace PCMInterface
{
    partial class MainformWithList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // textBox_filename
            // 
            this.textBox_filename.Location = new System.Drawing.Point(700, 421);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(700, 476);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(700, 447);
            // 
            // button_Apply
            // 
            this.button_Apply.Location = new System.Drawing.Point(700, 75);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(700, 46);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(700, 17);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(700, 104);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(75, 311);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // MainformWithList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "MainformWithList";
//            this.Load += new System.EventHandler(this.MainformWithList_Load);
            this.Controls.SetChildIndex(this.button_OK, 0);
            this.Controls.SetChildIndex(this.button_Cancel, 0);
            this.Controls.SetChildIndex(this.button_Apply, 0);
            this.Controls.SetChildIndex(this.button_Save, 0);
            this.Controls.SetChildIndex(this.button_Load, 0);
            this.Controls.SetChildIndex(this.textBox_filename, 0);
            this.Controls.SetChildIndex(this.Button_Debug, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
    }
}
