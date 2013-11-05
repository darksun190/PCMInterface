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
    public partial class single_var_withTOL : single_var
    {
        private bool have_tol;
        protected TextBox ut_textbox;
        protected TextBox lt_textbox;
        public single_var_withTOL()
        {
            have_tol = false;
            ut_textbox = new TextBox();
            lt_textbox = new TextBox();
            this.Controls.Add(ut_textbox);
            this.Controls.Add(lt_textbox);
            InitializeComponent();
            //
            // ut_textbox
            //
            ut_textbox.Name = "ut_textbox";
            ut_textbox.Anchor = AnchorStyles.None;
            ut_textbox.Location = new Point(160, 0);
            ut_textbox.Size = new Size(80, 20);
            ut_textbox.Text = "";
            ut_textbox.TextAlign = HorizontalAlignment.Center;
            ut_textbox.TextChanged += new EventHandler(textBox1_TextChanged);
            ut_textbox.Hide();
            //
            // lt_textbox
            //
            lt_textbox.Name = "lt_textbox";
            lt_textbox.Anchor = AnchorStyles.None;
            lt_textbox.Location = new Point(160, 20);
            lt_textbox.Size = new Size(80, 20);
            lt_textbox.Text = "";
            lt_textbox.TextAlign = HorizontalAlignment.Center;
            lt_textbox.TextChanged += new EventHandler(textBox1_TextChanged);
            lt_textbox.Hide();

            ut_textbox.TextChanged += new EventHandler(textBox1_TextChanged);
            lt_textbox.TextChanged += new EventHandler(textBox1_TextChanged);

        }
     
        [Category("Custom")]
        public bool with_tol
        {
            get
            {
                return have_tol;
            }
            set
            {
                if (value)
                {
                    number_only = true;
                    this.Size = new Size(240, 40);
                    this.name_label.Location = new Point(0, 10);
                    this.value_textbox.Location = new Point(80, 10);
                    ut_textbox.Location = new Point(160, 0);
                    lt_textbox.Location = new Point(160, 20);
                    this.ut_textbox.Show();
                    this.lt_textbox.Show();
                }
                else
                {
                    this.Size = new Size(160, 20);
                    this.name_label.Location = new Point(0, 0);
                    this.value_textbox.Location = new Point(80, 0);
                    ut_textbox.Hide();
                    lt_textbox.Hide();
                }
                have_tol = value;
            }
        }
        public override bool Check_Validation()
        {
            return base.Check_Validation();
        }
        public override bool Write_Parameter(Dictionary<string, string> dic)
        {
            base.Write_Parameter(dic);
            if (have_tol)
            {
                string key = name_label.Text;
                key += "_UT";
                if (dic.ContainsKey(key))
                {
                    dic.Remove(key);
                }
                dic.Add(key, ut_textbox.Text == "" ? "0" : ut_textbox.Text);

                key = name_label.Text;
                key += "_LT";
                if (dic.ContainsKey(key))
                {
                    dic.Remove(key);
                }
                dic.Add(key, lt_textbox.Text == "" ? "0" : lt_textbox.Text);
            }
            return true;
        }
        public override bool Read_Parameter(Dictionary<string, string> dic)
        {
            base.Read_Parameter(dic);
            if (have_tol)
            {
                string key = name_label.Text + "_UT";
                if (dic.Keys.Contains(key))
                {
                    ut_textbox.Text = dic[key];
                }
                else
                {
                    ut_textbox.Text = "";
                }
                key = name_label.Text + "_LT";
                if (dic.Keys.Contains(key))
                {
                    lt_textbox.Text = dic[key];
                }
                else
                {
                    lt_textbox.Text = "";
                }
            }
            return true;
        }
    }
}
