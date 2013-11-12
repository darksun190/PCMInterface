using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCMInterface
{
    /// <summary>
    /// a simple variable define control
    /// </summary>
    public partial class single_var : baseUI
    {
        /// <summary>
        /// the variable value
        /// </summary>
        protected string _tt_text;
        public string var_value;
        protected Label name_label;
        protected TextBox value_textbox;
        protected System.Windows.Forms.ToolTip toolTip1;
        public single_var()
        {
            InitializeComponent();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            name_label = new Label();
            value_textbox = new TextBox();

            this.Resize += single_var_Resize;
            this.MinimumSize = new Size(160, 20);
            #region controls
            this.SuspendLayout();
            this.Size = new Size(160, 20);
            this.Controls.Add(name_label);
            this.Controls.Add(value_textbox);

            //
            // name_label
            //
            name_label.Name = "name_label";
            name_label.Anchor = AnchorStyles.None;
            name_label.Location = new Point(0, 0);
            name_label.Size = new Size(80, 20);
            name_label.Text = "label";
            name_label.TextAlign = ContentAlignment.MiddleLeft;
            //
            // value_textbox
            //
            value_textbox.Name = "value_textbox";
            value_textbox.Anchor = AnchorStyles.None;
            value_textbox.Location = new Point(80, 0);
            value_textbox.Size = new Size(80, 20);
            value_textbox.Text = "";
            value_textbox.TextAlign = HorizontalAlignment.Center;
            value_textbox.TextChanged += new EventHandler(textBox1_TextChanged);
            this.ResumeLayout(false);
            #endregion


        }

        protected void single_var_Resize(object sender, EventArgs e)
        {
            name_label.Location = new Point(0, 0);
            name_label.Size = new Size(this.Width - 80,20);
            value_textbox.Location = new Point(this.Width - 80, 0);
            value_textbox.Width = 80;
            
        }
        #region "properties"
        [Category("Custom")]
        public string tooltipText
        {
            get
            {
                return _tt_text;
            }
            set
            {
                toolTip1.SetToolTip(name_label, value);
                _tt_text = value;
            }
        }
        /// <summary>
        /// variable key name
        /// </summary>
        [Category("Custom")]
        public string varible_name
        {
            get
            {
                return name_label.Text;
            }
            set
            {
                name_label.Text = value;
            }
        }
        /// <summary>
        /// define the input value only accept number or not
        /// </summary>
        [Category("Custom")]
        public bool number_only
        {
            get;
            set;
        }
        #endregion

        override public bool Check_Validation()
        {
            if (value_textbox.Text == "")
            {
                MessageBox.Show(name_label.Text + " should not be empty");
                return false;
            }
            return true;
        }
        override public bool Read_Parameter(Dictionary<string, string> dic)
        {
            if (dic.Keys.Contains(name_label.Text))
            {
                value_textbox.Text = dic[name_label.Text];
                return true;
            }
            else
            {
                value_textbox.Text = "";
                return false;
            }

        }
        override public bool Write_Parameter(Dictionary<string, string> dic)
        {
            string key = name_label.Text;
            if (dic.ContainsKey(key))
            {
                dic.Remove(key);
            }
            dic.Add(key, value_textbox.Text);
            return true;
        }
        protected void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == "-")
            {
                var_value = "";
                //return;
            }
            else
            {
                if (number_only)
                {
                    string text1 = ((TextBox)sender).Text;
                    double value1;
                    if (!double.TryParse(text1, out value1))
                    {
                        if (double.TryParse(text1.Substring(0, text1.Count() - 1), out value1))
                        {
                            ((TextBox)sender).Text = text1.Substring(0, text1.Count() - 1);
                            ((TextBox)sender).Select(text1.Count(), 0);
                        }
                        else
                        {
                            ((TextBox)sender).Text = "";
                        }
                    }
                }
                var_value = value_textbox.Text;
            }
            if (this.TextChanged != null)
                this.TextChanged(this, e);
        }

        #region event
        new public event EventHandler TextChanged;
        #endregion
    }
}
