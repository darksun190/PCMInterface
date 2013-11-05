using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace PCMInterface
{
    public partial class _10paras_Pic : baseUI
    {
        protected List<single_var_withTOL> paras;
        protected Label[] highlight_label;
        protected PictureBox pic_box;
        protected Point[] label_pos;
        private string[] label_name;
        private Color h_color;
        private int _var_no;
        private bool[] _have_tol;
        #region interface
        [Category("Custom")]
        public bool[] With_tolerance
        {
            get
            {
                for (int i = 0; i < 10; ++i)
                {
                    _have_tol[i] = paras[i].with_tol;
                }
                return (bool[])_have_tol.Clone();
            }
            set
            {
                _have_tol = value;
                for (int i = 0; i < 10; ++i)
                {
                    paras[i].with_tol = _have_tol[i];
                }
            }
        }
        [Category("Custom")]
        public int var_no
        {
            get
            {
                return _var_no;
            }
            set
            {

                for (int i = 0; i < value; ++i)
                {
                    paras[i].Show();
                    highlight_label[i].Show();
                }
                for (int i = value; i < 10; ++i)
                {
                    paras[i].Hide();
                    highlight_label[i].Hide();
                }
                _var_no = value;
            }

        }
        [Category("Custom")]
        public Color Highlight_Color
        {
            get
            {
                return h_color;
            }
            set
            {
                h_color = value;
            }
        }
        [Category("Custom")]
        public Image select_pic
        {
            get
            {
                return pic_box.Image;
            }
            set
            {
                pic_box.Image = value;
            }
        }
        [Category("Custom")]
        public Point[] Label_Pos
        {
            get
            {
                for (int i = 0; i < 10; i++)
                {
                    label_pos[i] = highlight_label[i].Location;
                }
                return (Point[])label_pos.Clone();
            }
            set
            {
                label_pos = value;
                for (int i = 0; i < 10; i++)
                {
                    highlight_label[i].Location = label_pos[i];
                }
            }
        }
        [Category("Custom")]
        public string[] Varible_Name
        {
            get
            {
                for (int i = 0; i < 10; ++i)
                {
                    label_name[i] = paras[i].varible_name;
                }
                return (string[])label_name.Clone();
            }
            set
            {
                label_name = value;
                for (int i = 0; i < 10; ++i)
                {
                    paras[i].varible_name = label_name[i];
                    highlight_label[i].Text = label_name[i] + "  " + paras[i].var_value;
                }
            }
        }
        #endregion

        public _10paras_Pic()
        {
            label_pos = new Point[11];
            label_name = new string[11];
            _have_tol = new bool[11];
            paras = new List<single_var_withTOL>();

            highlight_label = new Label[10];
            h_color = Color.Yellow;
            _var_no = 10;
            //   this.SuspendLayout();

            Trace.WriteLine(label_name.Count());
            ///
            /// variables
            /// 
            for (int i = 0; i < 10; ++i)
            {
                paras.Add(new single_var_withTOL());
                this.Controls.Add(paras[i]);
                paras[i].Size = new Size(160, 20);
                paras[i].Location = new System.Drawing.Point(610, 60 + i * 40);
                paras[i].Name = "paras_" + i.ToString();
                paras[i].number_only = true;
                paras[i].TabIndex = i;
                paras[i].varible_name = "variable_" + i.ToString();
                paras[i].with_tol = false;
                label_name[i] = paras[i].varible_name;
            }
            ///
            /// highlight Labels
            /// 
            for (int i = 0; i < 10; ++i)
            {
                highlight_label[i] = new Label();
                this.Controls.Add(highlight_label[i]);
                highlight_label[i].Size = new Size(80, 20);
                highlight_label[i].Location = new Point(200, 60 + i * 40);
                highlight_label[i].Name = "h_label_" + i.ToString();
                highlight_label[i].Text = "variable_" + i.ToString();
                highlight_label[i].AutoSize = true;
                highlight_label[i].TextAlign = ContentAlignment.MiddleCenter;
            }
            ///
            /// picture Box
            /// 
            pic_box = new PictureBox();
            this.Controls.Add(pic_box);
            pic_box.Name = "Picture_Box";
            pic_box.Size = new Size(600, 500);
            pic_box.Location = new Point(0, 0);
            pic_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            //   this.ResumeLayout(false);
            for (int i = 0; i < 10; ++i)
            {
                paras[i].TextChanged += new EventHandler(textBoxs_Text_Changed);
                paras[i].Enter += new EventHandler(textBoxs_Enter);
                paras[i].Leave += new EventHandler(textBoxs_Leave);

            }
            InitializeComponent();

        }
        #region "internal function"

        private void textBoxs_Enter(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < 10; ++i)
            {
                if (paras[i] == sender)
                {
                    break;
                }
            }
            highlight_label[i].BackColor = h_color;
        }
        private void textBoxs_Leave(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < 10; ++i)
            {
                if (paras[i] == sender)
                {
                    break;
                }
            }
            highlight_label[i].BackColor = Control.DefaultBackColor;
        }
        private void textBoxs_Text_Changed(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < 10; ++i)
            {
                if (paras[i] == sender)
                {
                    break;
                }
            }
            highlight_label[i].Text = paras[i].varible_name + "  " + paras[i].var_value;
        }
        #endregion
        #region public function
        public override bool Read_Parameter(Dictionary<string, string> dic)
        {
            bool result=true;
            for (int i = 0; i < _var_no; ++i)
            {
                if (!paras[i].Read_Parameter(dic))
                    result = false;
            }
            return result;
        }
        public override bool Write_Parameter(Dictionary<string, string> dic)
        {
            for (int i = 0; i < _var_no; ++i)
            {
                paras[i].Write_Parameter(dic);
            }
            return true;
          
        }
        public override bool Check_Validation()
        {
            for (int i = 0; i < _var_no; ++i)
            {
                if (!paras[i].Check_Validation())
                    return false;
            }
            return true;
        }
        #endregion
    }
}
