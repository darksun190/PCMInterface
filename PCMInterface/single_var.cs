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
    public partial class single_var : baseUI
    {
        public string var_value;
        protected Label name_label;
        protected TextBox value_textbox;
        public single_var()
        {
            InitializeComponent();
            name_label = new Label();
            value_textbox = new TextBox();

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
            name_label.TextAlign = ContentAlignment.MiddleCenter;
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
           #region "properties"
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
