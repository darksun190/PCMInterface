using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCMInterface
{
    public partial class single_var_select : baseUI
    {
        protected Label name_label;
        protected ComboBox value_box;
        public single_var_select()
        {
            InitializeComponent();
            name_label = new Label();
            value_box = new ComboBox();

            this.Size = new Size(160, 22);
            this.Controls.Add(name_label);
            this.Controls.Add(value_box);

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
            value_box.Name = "value_box";
            value_box.Anchor = AnchorStyles.None;
            value_box.Location = new Point(80, 1);
            value_box.Size = new Size(80, 20);
            value_box.DropDownStyle = ComboBoxStyle.DropDownList;
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
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ComboBox.ObjectCollection Items
        {
            get
            {
                return value_box.Items;
            }
            set
            {
                value_box.Items.Clear();
                foreach (var item in value)
                {
                    value_box.Items.Add(item);
                }
            }
        }
        //public ComboBox combobox
        //{
        //    get
        //    {
        //        return value_box;
        //    }
        //    set
        //    {
        //        value_box = value;
        //    }
        //}
        #endregion
        override public bool Check_Validation()
        {
            if (value_box.SelectedIndex == -1)
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
                value_box.SelectedItem = dic[name_label.Text];
                return true;
            }
            else
            {
                value_box.SelectedIndex = -1;
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
            dic.Add(key, value_box.SelectedItem.ToString());
            return true;
        }
    }
}
