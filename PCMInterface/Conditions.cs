using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Design;
using System.Windows.Forms.Design;

namespace PCMInterface
{
    /// <summary>
    /// this is a container, drag other controls to this panel
    /// </summary>
    [Designer(typeof(TestControlBaseDesigner))]
    public partial class Conditions : UserControl
    {
        List<Panel> panels;
        protected string[] _list_source;
        protected string _var_name;
        public Conditions()
        {
            _list_source = new string[2] { "value1", "value2" };

            panels = new List<Panel>();
            for (int i = 0; i < 10; ++i)
            {
                panels.Add(new Panel());
                panels[i].Location = new Point(0, 30);
                panels[i].Size = new Size(300, 200);
                panels[i].Name = "panels[" + i.ToString() + "]";
                panels[i].Move += new EventHandler(panels_moved);
                this.Controls.Add(panels[i]);
            }
            panel0.Hide();
            panel1.Hide();

            InitializeComponent();

            comboBox1.DataSource = _list_source;
        }
        /// <summary>
        /// define the condition variable key name. like Type = Cone, define Type here
        /// </summary>
        [Category("Custom")]
        public string Variable_Name
        {
            get
            {
                return _var_name;
            }
            set
            {
                _var_name = value;
                label1.Text = "if " + value + " = ";
            }
        }
        /// <summary>
        /// switch the current selection of the comboBox 
        /// </summary>
        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public int Current_Selection
        {
            get
            {
                return comboBox1.SelectedIndex;
            }
            set
            {
                comboBox1.SelectedIndex = value;
                for (int i = 0; i < 10; ++i)
                {
                    panels[i].Hide();
                }
                panels[comboBox1.SelectedIndex].Show();
            }
        }
        /// <summary>
        /// define the content of the combobox, usually the possible values of the condition
        /// </summary>
        [Category("Custom")]
        public string[] list_str
        {
            get
            {
                return ((string[])_list_source.Clone());
            }
            set
            {
                _list_source = value;
                comboBox1.DataSource = null;
                comboBox1.DataSource = _list_source;

            }
        }

        #region interface
        /// <summary>
        /// try to find the corespond key and values for these 10 parameters from the dic.
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool Read_Parameter(Dictionary<string, string> dic)
        {
            if (dic.Keys.Contains(_var_name))
            {
                for (int i = 0; i < comboBox1.Items.Count; ++i)
                {
                    if (comboBox1.Items[i].ToString() == dic[_var_name])
                    {
                        comboBox1.SelectedIndex = i;
                        foreach (Control c in panels[comboBox1.SelectedIndex].Controls)
                        {
                            var flec_fun = c.GetType().GetMethod("Read_Parameter");
                            if (flec_fun != null)
                            {
                                flec_fun.Invoke(c, new object[] { dic });
                            }

                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// check every textbox was well input
        /// </summary>
        /// <returns></returns>
        public bool Check_Validation()
        {
            foreach (Control c in panels[comboBox1.SelectedIndex].Controls)
            {
                var flec_fun = c.GetType().GetMethod("Check_Validation");
                if (flec_fun != null)
                {

                    if (!(bool)flec_fun.Invoke(c, null))
                    {
                        return false;
                    }

                }

            }
            return true;
        }
        /// <summary>
        /// get every value from the textbox, input by the end user, import it to the dic
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool Write_Parameter(Dictionary<string, string> dic)
        {
            string key = _var_name;
            if (dic.ContainsKey(key))
            {
                dic.Remove(key);
            }
            dic.Add(key, comboBox1.SelectedItem.ToString());
            foreach (Control c in panels[comboBox1.SelectedIndex].Controls)
            {
                var flec_fun = c.GetType().GetMethod("Write_Parameter");
                if (flec_fun != null)
                {
                    flec_fun.Invoke(c, new object[] { dic });
                }
            }
            return true;
        }
        #endregion

        #region panels for program
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel0
        {
            get
            {
                return panels[0];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel1
        {
            get
            {
                return panels[1];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel2
        {
            get
            {
                return panels[2];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel3
        {
            get
            {
                return panels[3];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel4
        {
            get
            {
                return panels[4];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel5
        {
            get
            {
                return panels[5];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel6
        {
            get
            {
                return panels[6];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel7
        {
            get
            {
                return panels[7];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel8
        {
            get
            {
                return panels[8];
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel panel9
        {
            get
            {
                return panels[9];
            }
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || comboBox1.SelectedIndex > 9)
                comboBox1.SelectedIndex = 0;
            for (int i = 0; i < 10; ++i)
            {
                panels[i].Hide();
                panels[i].Enabled = false;
            }
            panels[comboBox1.SelectedIndex].Show();
            panels[comboBox1.SelectedIndex].Enabled = true;

        }
        private void panels_moved(object sender, EventArgs e)
        {
            ((Panel)sender).Parent = this;

            ((Panel)sender).Location = new Point(0, 30);
            return;
        }
    }
    /// <summary>
    /// to make the panels inside can contain the other controls and designable.
    /// </summary>
    public class TestControlBaseDesigner : ControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            Conditions ctl = (Conditions)component;
            EnableDesignMode(ctl.panel1, "panel1");
            EnableDesignMode(ctl.panel2, "panel2");
            EnableDesignMode(ctl.panel0, "panel0");
            EnableDesignMode(ctl.panel3, "panel3");
            EnableDesignMode(ctl.panel4, "panel4");
            EnableDesignMode(ctl.panel5, "panel5");
            EnableDesignMode(ctl.panel6, "panel6");
            EnableDesignMode(ctl.panel7, "panel7");
            EnableDesignMode(ctl.panel8, "panel8");
            EnableDesignMode(ctl.panel9, "panel9");
        }
    }
}
