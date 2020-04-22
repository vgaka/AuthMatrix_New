using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthMatrix
{
    public partial class FrmKeySearch : Form
    {
        List<string> searchlist= new List<string>();
        string linqstr = "";
        int ans = 0;
        string cmd = "";
        private void recommand(List<string> a)
        {
            for (int i = 1;i == a.Count(); i++)
            {
                cmd = cmd + gentext(a[i]);
            }
        }
        private string gentext(string condition)
        {
            string ret = "";
            ret = "select * from tbl where columna like %" + condition.ToString() + "%";
            return ret;
        }
        public FrmKeySearch()
        {
            InitializeComponent();
        }
        private String createlinq(List<string> l)
        {

            string linq = "";
            string command = "";
            for (int i = 0; i < l.Count(); i++)
            {
                command += (l[i].Trim().ToString()) + "||";
                linq = command;
            }
            return linq;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            void ShowData<T>(T x)
            {
                //MessageBox.Show(x.ToString(), "อ่านค่า");
                Console.WriteLine("จากShowData "+ x.ToString());
            }
            void AddValue(int a, int b)
            {
                ans = a + b;
                ShowData<int>(ans);
            }
            var activity = new Activity
            {
                task = "Header LV 1",
                role = "",
                responsibilities = ""
            };
            try 
            { 
                searchlist = textBox1.Text.Split(',').ToList();
                linqstr = createlinq(searchlist);
                ShowData(searchlist);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally             {            }
            /*
            int aa = 5;
            int bb = 7;
            AddValue(aa, bb);
            */         
        }
    }
}