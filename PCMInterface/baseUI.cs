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
    [EditorBrowsable(EditorBrowsableState.Never)]
    abstract public partial class baseUI : UserControl
    {
        #region "interface"

        [Category("Custom")]
        virtual public bool Check_Validation()
        {
            return true;
        }
        virtual public bool Read_Parameter(Dictionary<string, string> dic)
        {
           return false;
        }
        virtual public bool Write_Parameter(Dictionary<string, string> dic)
        {
            return true;
        }
        public baseUI()
        {
            InitializeComponent();
        }
        #endregion
    }
}
