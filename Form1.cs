using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Diagnostics;

namespace IYC_KUTUPHANE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");

        //sqlnotes
        //(localdb)\\MSSQLLocalDB //*filePath*\db1.mdf //|DataDirectory|\\yusuf.mdf 

        private void Label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           // try
           // {
                string DURUM;
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from KADI WHERE KADI='" + textBox1.Text + "' AND SIFRE='" + textBox2.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    DURUM = dr["DURUM"].ToString();
                    dr.Close();
                    Form2 frm2 = new Form2();
                    frm2.label7.Text = DURUM;
                    frm2.Show();
                    this.Hide();
                    dr.Close();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    con.Close();
                }
                con.Close();
            //}
           // catch (Exception hata)
            //{
                //label5.Text = hata.Message;
               // MessageBox.Show("İlk kurulum ayarlarınızı yaptığınızdan emin olun. Sorun çözülmezse destekçi ile iletişime geçin."+hata,"UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //}
            
        }

        private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from KADI WHERE KADI='" + textBox1.Text + "' AND SIFRE='" + textBox2.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Form2 frm2 = new Form2();
                    frm2.Show();
                    this.Hide();
                    dr.Close();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dr.Close();
                    con.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            checkBox1.Checked = true;
            
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                textBox2.Focus();
            }
            if (e.KeyCode==Keys.Down)
            {
                textBox2.Focus();
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                button1.Focus();
            }
            if (e.KeyCode==Keys.Up)
            {
                textBox1.Focus();

            }
            if (e.KeyCode == Keys.Left)
            {
                checkBox1.Focus();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
            }
            else
            {
                textBox2.Focus();
            }
            
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '*';
            }
            else
            {
                textBox2.PasswordChar = '\0';
            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (checkBox1.Checked==true)
                {
                    checkBox1.Checked = false;
                    textBox2.Focus();
                }
                else
                {
                    checkBox1.Checked = true;
                    textBox2.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Process.Start("cmd.exe", "/k" + label4.Text);
            //string ExeDosyaYolu = Application.StartupPath.ToString();
            //Process.Start(ExeDosyaYolu + "\\sqlLocalDB.msi");
            

            //Process p = new Process();

            //p.StartInfo.FileName = path;
            //p.StartInfo.CreateNoWindow = true;

            ////Process gizli çalışsın.

            //p.Start();

            ////İşlem bitene kadar bekle

            //p.WaitForExit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
        }
    }
}
