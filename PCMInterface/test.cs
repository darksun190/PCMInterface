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
    public partial class test : UserControl
    {
        string[] _list_str;
      
  //      [Category("Custom")]
  //      [Description("The items in the UserControl's ComboBox."),
  //DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
  //Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
  //      public ComboBox.ObjectCollection options
  //      {
  //          get
  //          {
  //              return comboBox1.Items;
  //          }
  //          set
  //          {
  //              int new_number = value.Count;
  //              comboBox1.Items.Clear();
  //              for (int i = 0; i < new_number; ++i)
  //              {
  //                  comboBox1.Items.Add(value[i]);
  //              }
  //          }
  //      }

        public test()
        {
            _list_str = new string[1];
            InitializeComponent();
         

        }

     
    }
}
