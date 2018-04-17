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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.usernamelabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordlabel.BackColor = System.Drawing.Color.Transparent;
            this.namelabel.BackColor = System.Drawing.Color.Transparent;
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text!= "")
            {
                MySqlConnection con = new MySqlConnection("datasource=127.0.0.1;port=3306;user=root;password=nick@12345;database=test");
                MySqlDataAdapter da = new MySqlDataAdapter("select count(*) from logininfo where username='" + textBox1.Text + "'and password='" + textBox2.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    this.Hide();
                    Form2 fm = new Form2();
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("Wrong Credentials");
                }
            }
            else
            {
                MessageBox.Show("One or more fields are empty");
            }

        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
