using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Newtonsoft.Json.Linq;
using FireSharp;

namespace AuthMatrix
{
    public partial class Form1 : Form
    {
        //IFirebaseClient client;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "u7L2MgXvKDJ5Bfyr0Qw2P8hlqh5TkD2KUJbRGsnA",
            BasePath = "https://api-project-935099486553.firebaseio.com/"
        };
        List<string> DName = new List<string>();
        List<string> DValue = new List<string>();
        List<string> DPath = new List<string>();
        List<string> DResponsibility = new List<string>();

        DataTable dt = new DataTable();
        DataRow dr;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            IFirebaseClient client = new FirebaseClient(config);
            // get new response object
            FirebaseResponse response = client.Get("");
            JObject o = JObject.Parse(response.Body);
            // Clear existing datatable
            DPath.Clear();
            DValue.Clear();
            DName.Clear();
            DResponsibility.Clear();
            dt.Clear();
            AddValtoList(o);
            //var result = !(txtsearchbox.Text.Contains(','));//false แปลว่าพบ , แต่ true แปลว่าไม่มีต้องแทรกให้
            string searchval;
                searchval = txtsearchbox.Text + ',';
                try
                {
                    List<string> slist = searchval.Split(',').ToList();
                    //อันนี้ทำงานได้                           

                    /*List<string> filterlist = (from d in DPath
                                               where d.Contains(slist[0].Trim().ToString())
                                               select d).ToList();
                    */
                    // จับคู่มันด้วย Zip()
                    /*
                     List<Tuple<string, string>> filterlisttuple = DPath
                            .Zip(DValue, (k, v) => new { k, v })
                            .Where(x => x.k.Contains(slist[0].Trim().ToString()) && x.k.Contains(slist[1].Trim().ToString()))
                            .Select(x => Tuple.Create(x.k, x.v))
                            .ToList();
                     */
                    //linq แบบใช้ class
                    List<Filter> filter = DPath
                            .Zip(DValue, (k, v) => new { k, v })
                            .Where(x => x.k.Contains(slist[0].Trim().ToString()) && x.k.Contains(slist[1].Trim().ToString()))
                            .Select(x => new Filter { Path = x.k, Value = x.v })
                            .ToList();

                    for (int i = 0; i < filter.Count(); i++)
                    {
                        // loop data to object  
                        dr = dt.NewRow();
                        dr["#"] = i + 1;
                        dr["Activities"] = filter[i].Path.Replace("Activities.","").Replace("Task.", "").Replace("_", ".").ToString();
                        if (dr["Activities"].ToString().Contains("กลั่นกรอง") )
                    {
                        dr["Responsibility"] = "ผู้กลั่นกรอง";
                    }
                        else
                    {
                        dr["Responsibility"] = "ผู้อนุมัติ";
                    }
                        dr["Action By"] = filter[i].Value.ToString();
                    
                        dt.Rows.Add(dr);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("error here");
                }
                finally
                {                }
            //MessageBox.Show(txtsearchbox.Text); 
            dataGridView1.DataSource = dt;
        }
        
        private void txtsearchbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            IFirebaseClient client = new FirebaseClient(config);
            FirebaseResponse response = client.Get("");
            JObject o = JObject.Parse(response.Body);
            
            // Add Responsibility columns
            dt.Columns.Add("#");
            dt.Columns.Add("Activities");
            dt.Columns.Add("Responsibility");
            dt.Columns.Add("Action By");
            dataGridView1.DataSource = dt;
            //AddValtoList(o);
            /*
            string checkdata = "";
            for (int i = 0; i < DName.Count; i++)
            {
                checkdata += String.Format("{0}. {1} : {2} ({3})\n", i + 1, DName[i], DValue[i], DPath[i]);
            }
            MessageBox.Show(checkdata);
            */
        }
        private void AddVal(string n, string v, string p, string r)
        {
            DName.Add(n);
            DValue.Add(v);
            DPath.Add(p);
            DResponsibility.Add(r);
        }
        private void AddValtoList(JObject o) //เรียกว่า Deserialization
        {
            List<string> l1 = o.Properties().Select(p => p.Name).ToList();
            foreach (var n1 in l1)
            {
                foreach (JProperty n2 in o[n1])
                {
                    //List<string> l3 = ((JObject)o[n1]["Task"]).Properties().Select(p => p.Name).ToList();
                    
                    foreach (JProperty n3 in o[n1][n2.Name])
                    {
                        foreach (JProperty n4 in o[n1][n2.Name][n3.Name])
                        {
                            if (n4.Value.Type.ToString() != "String") {

                                foreach (JProperty n5 in o[n1][n2.Name][n3.Name][n4.Name])
                                {
                                    if (n5.Value.Type.ToString() != "String")
                                    {
                                        foreach (JProperty n6 in o[n1][n2.Name][n3.Name][n4.Name][n5.Name])
                                        {
                                            if (n6.Value.Type.ToString() != "String")
                                            {
                                                foreach (JProperty n7 in o[n1][n2.Name][n3.Name][n4.Name][n5.Name][n6.Name])
                                                {
                                                    if (n7.Value.Type.ToString() != "String")
                                                    {
                                                        foreach (JProperty n8 in o[n1][n2.Name][n3.Name][n4.Name][n5.Name][n6.Name][n7.Name])
                                                        if (n8.Value.Type.ToString() != "String") 
                                                            {
                                                                AddVal(n8.Name, (string)n8.Value, n8.Path, (string)n7.Name);
                                                            }
                                                    }
                                                    else
                                                    {
                                                        AddVal(n7.Name, (string)n7.Value, n7.Path, (string)n6.Name);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                AddVal(n6.Name, (string)n6.Value, n6.Path, (string)n5.Name);
                                                Console.WriteLine(n6.Name);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AddVal(n5.Name, (string)n5.Value, n5.Path, (string)n4.Name);
                                        Console.WriteLine(n5.Name);
                                    }
                                }
                            
                            }
                            else
                            {
                                AddVal(n4.Name, (string)n4.Value, n4.Path, (string)n3.Name);
                            }
                        }
                    }
                }
            }
        }
    }

}