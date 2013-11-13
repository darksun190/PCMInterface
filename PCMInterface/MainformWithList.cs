using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PCMInterface
{
    public partial class MainformWithList : MainForm
    {
        public event EventHandler sub_changed;
        public MainformWithList()
        {
            InitializeComponent();
            this.sub_changed += new EventHandler(update_listview);
            sub_f_name = "";
            this.button_Save.Click += update_listview;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        /// <summary>
        /// overwrite the old one, the listview depends on this folder
        /// </summary>
        public new string sub_f_name
        {
            get
            {
                return _sub_f_name;
            }
            set
            {
                if (value == "")
                    _sub_f_name = "";
                else
                    _sub_f_name = value;
                if (_sub_f_name != "")
                    folder = Path.Combine(System.IO.Directory.GetCurrentDirectory(), _sub_f_name);
                else
                    folder = System.IO.Directory.GetCurrentDirectory();
                if (sub_changed != null)
                    this.sub_changed(this, null);
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                textBox_filename.Text = listView1.SelectedItems[0].Text;
        }
        void update_listview(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(folder))
            {
                var filePaths = Directory.GetFiles(folder, "*." + ext_name).Select(Path.GetFileNameWithoutExtension);
                listView1.Clear();
                listView1.Columns.Add("Name", 75);
                foreach (string s in filePaths)
                {
                    listView1.Items.Add(s);
                }
            }
            else
            {

            }
        }


    }
}
