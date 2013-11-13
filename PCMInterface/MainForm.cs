using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace PCMInterface
{
    public partial class MainForm : ContainerControl
    {
        protected Dictionary<string, string> dic;
        protected object[] para;
        //private string all_text;
        private string _origin_file;
        /// <summary>
        /// the extention file name, like "para" or "pcm" without the dot"."
        /// </summary>
        public string ext_name
        {
            get;
            set;
        }
        protected string folder;
        protected string _sub_f_name;
        /// <summary>
        /// the sub folder name, empty means the same path of the exe file
        /// </summary>
        public string sub_f_name
        {
            get
            {
                return _sub_f_name;
            }
            set
            {
                _sub_f_name = value;
                if (_sub_f_name != "")
                    folder = Path.Combine(System.IO.Directory.GetCurrentDirectory(), _sub_f_name);
                else
                    folder = System.IO.Directory.GetCurrentDirectory();
            }
        }
        /// <summary>
        /// the example text , use origin_file to import a file
        /// </summary>
        public string all_text
        {
            get;

            set;
        }
        /// <summary>
        /// import a file, it will read the file content to the all_text string automatically
        /// </summary>
        [Editor("System.Windows.Forms.Design.FileNameEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

        public string origin_file
        {
            get
            {
                return _origin_file;
            }
            set
            {
                _origin_file = value;
                if (File.Exists(value))
                    all_text = System.IO.File.ReadAllText(value);
                else
                    all_text = "";
            }
        }

        protected Dictionary<string, string> master_dic;
        protected Dictionary<string, string> comment_dic;
        public MainForm()
        {
            if (ext_name == null)
                ext_name = "para";

            dic = new Dictionary<string, string>();
            InitializeComponent();
            para = new object[] { dic };
        

            master_dic = new Dictionary<string, string>();
            comment_dic = new Dictionary<string, string>();
            all_text = "";
        }

        protected void Read_Parameter(Control dad)
        {
            var flec_fun = dad.GetType().GetMethod("Read_Parameter");
            if (flec_fun != null)
            {
                flec_fun.Invoke(dad, para);
            }
            else
            {
                if (dad.Controls.Count == 0)
                {
                    return;
                }
                else
                {
                    foreach (Control a in dad.Controls)
                    {
                        Read_Parameter(a);
                    }
                }
            }
            return;
        }
        protected void Write_Parameter(Control dad)
        {
            var flec_fun = dad.GetType().GetMethod("Write_Parameter");
            if (flec_fun != null)
            {
                flec_fun.Invoke(dad, para);
            }
            else
            {
                if (dad.Controls.Count == 0)
                {
                    return;
                }
                else
                {
                    foreach (Control a in dad.Controls)
                    {
                        Write_Parameter(a);
                    }
                }
            }
            return;
        }
        protected bool Check_Validation(Control dad)
        {
            var flec_fun = dad.GetType().GetMethod("Check_Validation");
            if (flec_fun != null)
            {

                if (!(bool)flec_fun.Invoke(dad, null))
                {
                    return false;
                }

            }
            else
            {
                if (dad.Controls.Count == 0)
                {
                    return true;
                }
                else
                {
                    foreach (Control a in dad.Controls)
                    {
                        if (!Check_Validation(a))
                            return false;
                    }
                }
            }
            return true;
        }
        protected void button_Apply_Click(object sender, EventArgs e)
        {
            //  throw new Exception();
            if (!Check_Validation(this))
                return;


            Write_Parameter(this);
        }

        protected void button_Save_Click(object sender, EventArgs e)
        {
            init_master_file();

            button_Apply_Click(sender, e);
            string filename = Path.Combine(folder, textBox_filename.Text + "." + ext_name);
            var fileWriter = new StreamWriter(filename);
            foreach (string key in dic.Keys)
            {
                string val = dic[key];
                double test_P;
                string one_line = key + " = ";
                if (double.TryParse(val, out test_P))
                {
                    one_line += val;
                }
                else
                {
                    one_line += "\"" + val + "\"";
                }
                if (comment_dic.ContainsKey(key) && comment_dic[key] != "")
                    one_line += comment_dic[key];

                fileWriter.WriteLine(one_line);

            }
            fileWriter.Close();
        }

        protected void button_Load_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(folder, textBox_filename.Text + "." + ext_name);
            var fileReader = new StreamReader(filename);
            string buffStr = "";
            string subBuff = "";


            do
            {
                buffStr = fileReader.ReadLine();

                int position = 0;
                //position if the string has "//"
                position = buffStr.IndexOf("//");
                //check if there is a "//"
                if (position == 0)
                {
                    continue;
                }
                if (position == -1)
                {
                    subBuff = buffStr;
                    //no comment in this line

                }
                else
                {
                    subBuff = buffStr.Substring(0, position);
                    //keep "abc" for "abc//def"
                }

                if (string.IsNullOrWhiteSpace(subBuff))
                {
                    continue;
                }

                string[] split = null;
                //an array for save the split string
                split = subBuff.Split(new Char[] { '=' });
                //split the line by =
                string key = null;
                string value = null;
                key = split[0].Trim();
                value = split[1].Trim().Trim('\"');
                // trim the space and the "
                if (dic.ContainsKey(key))
                {
                    dic.Remove(key);
                }
                dic.Add(key, value);
                //save the para to the dictionary


                Console.WriteLine(key + "  " + value);
            } while (!(fileReader.EndOfStream));
            fileReader.Close();
            Read_Parameter(this);
        }

        protected void button_OK_Click(object sender, EventArgs e)
        {
            button_Save_Click(sender, e);
            ((Form)this.Parent).Close();
        }

        protected void button_Cancel_Click(object sender, EventArgs e)
        {
            ((Form)this.Parent).Close();
        }

        protected void init_master_file()
        {

            string[] subtexts = all_text.Split(Environment.NewLine.ToCharArray());
            string subBuff = null;

            foreach (string s in subtexts)
            {
                string key = "";
                string value = "";
                string comment = "";
                int position = 0;
                //position if the string has "//"
                position = s.IndexOf("//");
                //check if there is a "//"
                if (position == 0)
                {
                    continue;
                }
                if (position == -1)
                {
                    subBuff = s;
                    //no comment in this line

                }
                else
                {
                    subBuff = s.Substring(0, position);
                    comment = s.Substring(position);
                    //keep "abc" for "abc//def"
                }

                if (string.IsNullOrWhiteSpace(subBuff))
                {
                    continue;
                }

                string[] split = null;
                //an array for save the split string
                split = subBuff.Split(new Char[] { '=' });
                //split the line by =
                key = split[0].Trim();
                value = split[1].Trim().Trim(new char[] { '\"' });
                // trim the space and the "
                if (master_dic.ContainsKey(key))
                {
                    master_dic.Remove(key);
                }
                master_dic.Add(key, value);
                if(!comment_dic.ContainsKey(key))
                    comment_dic.Add(key, comment);
                //save the para to the dictionary
            }

        }

        protected void only_for_debug(object sender, EventArgs e)
        {
            init_master_file();
            MessageBox.Show(comment_dic.Count.ToString());
            try
            {
                foreach (string key in master_dic.Keys)
                {
                    if (master_dic[key] == "")
                    {
                        throw new Exception(key + " didn't have a default value");
                    }
                    if (!dic.ContainsKey(key))
                    {
                        throw new Exception("didn't put a control to windows named : " + key);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
