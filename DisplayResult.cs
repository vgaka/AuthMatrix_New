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
    public partial class DisplayResult : Form
    {
        //Variable declaration
        IList<string> scrkey = new List<string>();
        List<string> searchkey = new List<string>();
        List<Filter> n = new List<Filter>();

        // method or sub program
        void ShowType(Object e)
        {
            if (e.GetType().Name.Substring(0, 4) == "List")
            {
                MessageBox.Show("List type");
            }
            else if (e.GetType().Name.Substring(0,4) == "ILis")
            {
                MessageBox.Show("IList type");
            }
            else if (e.GetType().Name.Substring(0, 4) == "String")
            {
                MessageBox.Show("String Type");
            }
            else
            {
                MessageBox.Show(e.GetType().Name.Substring(e.GetType().Name.Length - 4));
            }
            
        }

        //etc

        public DisplayResult()
        {
            InitializeComponent();
        }



        private void DisplayResult_Load(object sender, EventArgs e)
        {
            for (int ii = 10;ii <= 20; ii++)
            {
                string p = "000" + ii.ToString();
                Console.WriteLine(p.Substring(p.Length - 4));
                scrkey.Add(p);
                searchkey.Add(ii.ToString());
            }
            scrkey.Add("11111");
            scrkey.Add("2222");

            searchkey.Add("A");
            searchkey.Add("B");
            var n = scrkey.Zip( searchkey, (fi,se) => fi+" "+se);
            //ShowType(scrkey);
            //ShowType(searchkey);
        }
    }
   
}
