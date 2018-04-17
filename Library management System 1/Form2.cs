using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Library_management_System_1
{
    public partial class Form2 : Form
    {
        int flag = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private MySqlDataReader con_func1(string query)
        {
            MySqlConnection con = new MySqlConnection("datasource=127.0.0.1;port=3306;user=root;password=nick@12345;database=test");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            return cmd.ExecuteReader();

        }
        private int con_func2(string query)
        {
            MySqlConnection con = new MySqlConnection("datasource=127.0.0.1;port=3306;user=root;password=nick@12345;database=test");
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            return cmd.ExecuteNonQuery();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (searchtextbox.Text != "")
            {
                String query = @"Select * from book where bkname='" + searchtextbox.Text + "'";
                MySqlDataReader sqlrdr = con_func1(query);
                int a = 0;
                while (sqlrdr.Read())
                {
                    if (a == Convert.ToInt32(sqlrdr["quantity"]))
                    {
                        label3.Text = "NO";
                    }
                    else
                    {
                        label3.Text = "YES";
                        flag = 1;
                        label5.Text = sqlrdr["racknum"].ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("search field is empty");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (searchtextbox.Text!="" && stdnametextbox.Text !="")
            {
                if (flag == 1)
                {
                        string query = "Select * from book where bkname='" + searchtextbox.Text + "'";
                        MySqlDataReader sqlrdr = con_func1(query);
                        sqlrdr.Read();
                        double isbn = Convert.ToInt64(sqlrdr["isbnnum"]);
                        int quant = Convert.ToInt32(sqlrdr["quantity"]);
                        string stdname = stdnametextbox.Text;
                        string query2 = "insert into issue values(" + isbn + ",'" + stdname + "',curdate())";
                        String query3 = "Select * from student where stdname='" + stdnametextbox.Text + "'";
                        MySqlDataReader sqlrdr2 = con_func1(query3);
                        sqlrdr2.Read();
                        string stdname2 = sqlrdr2["stdname"].ToString();
                        int z = Convert.ToInt32(sqlrdr2["stnumbk"]);
                        if (stdname.Equals(stdname2))
                        {
                            if (z!=3)
                            {
                                if (quant != 0)
                                {
                                    z++;
                                    quant--;
                                    String query5 = "update student set stnumbk='"+z+"' where stdname='"+stdname+"'";
                                    String query4 = "update book set quantity='"+quant+"' where isbnnum='"+isbn+"'";
                                    int c = con_func2(query2);
                                    int g = con_func2(query4);
                                    int j = con_func2(query5);
                                    if (c == 1 && g == 1 && j==1)
                                    {
                                        MessageBox.Show("Book issued succesfully");
                                        foreach (TextBox tb in this.Controls.OfType<TextBox>())
                                        tb.Text = string.Empty;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error");
                                    }
                                }
                                else
                                {
                                MessageBox.Show("No books of the mentioned name left");
                                }
                            }
                            else
                            {
                            MessageBox.Show("Maximum limit of borrowing books reached");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid student name");
                        }
                }
                else
                {
                    MessageBox.Show("this book cannot be issued");
                }
            }
            else
            {
                MessageBox.Show("One or more fields are empty");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (newbooktextbox.Text != "" && newauthortextbox.Text != "" && newisbntextbox.Text != "" && newquanttextbox.Text!= "" && newracktextbox.Text!= "")
            {
                String name = newbooktextbox.Text;
                String author = newauthortextbox.Text;
                double isbn = Convert.ToInt64(newisbntextbox.Text);
                int rack = Convert.ToInt32(newracktextbox.Text);
                int quantity = Convert.ToInt32(newquanttextbox.Text);
                String query1 = "insert into book values('" + name + "','" + author + "'," + isbn + "," + quantity + "," + rack + ")";
                int a = con_func2(query1);
                if (a == 1)
                {
                    MessageBox.Show("Succesfully Added");
                    foreach (TextBox tb in this.Controls.OfType<TextBox>())
                    {
                        tb.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Unsuccessful");
                }
            }
            else
            {
                MessageBox.Show("One or more fields are empty");
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (newadmintextbox.Text != "" && newpasswordtextbox.Text != "")
            {
                String username = newadmintextbox.Text;
                String password = newpasswordtextbox.Text;
                String query1 = "insert into logininfo values('" + username + "','" + password + "')";
                int a = con_func2(query1);
                if (a == 1)
                {
                    MessageBox.Show("User added succesfully");
                    foreach(TextBox tb in Controls.OfType<TextBox>())
                    {
                        tb.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                MessageBox.Show("One or more fields are empty");
            }
        }

        private void deletebookbutton_Click(object sender, EventArgs e)
        {
            if(deletebox.Text!="")
            {
                String bkname = deletebox.Text;
                String query = "delete from book where bkname='"+bkname+"'";
                int n = con_func2(query);
                if(n==1)
                {
                    MessageBox.Show("Successfully deleted");
                    foreach (TextBox tb in Controls.OfType<TextBox>())
                    {
                        tb.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }

            }
            else
            {
                MessageBox.Show("Book name is empty");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 fn = new Form3();
            fn.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f0 = new Form4();
            f0.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 fP = new Form5();
            fP.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (reisbntextbox.Text!="" && restudenttextbox.Text !="")
            {
                String isbn = reisbntextbox.Text;
                String studname = restudenttextbox.Text;
                String query1 = "delete from issue where bkisbn='"+isbn+"' and stdname='"+studname+"'";
                String query2= "Select* from book where isbnnum = '" +isbn+ "'";
                String query3= "Select* from student where stdname = '" +studname+ "'";
                MySqlDataReader dr1 = con_func1(query2);
                dr1.Read();
                int quant = Convert.ToInt32(dr1["quantity"]);
                MySqlDataReader dr2 = con_func1(query3);
                dr2.Read();
                int stnumbk = Convert.ToInt32(dr2["stnumbk"]);
                int s = con_func2(query1);
                quant++;
                stnumbk--;
                String query4= "update student set stnumbk='"+stnumbk+"' where stdname='"+studname+"'";
                String query5= "update book set quantity='"+quant+"' where isbnnum='"+isbn+"'";
                int c = con_func2(query4);
                int d = con_func2(query5);

                if (s>=1&&c==1&&d==1)
                {
                    MessageBox.Show("Book Successfully Received");
                    foreach (TextBox tb in Controls.OfType<TextBox>())
                    {
                        tb.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                MessageBox.Show("One or more fields are empty");
            }
        }
    }
}
