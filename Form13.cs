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

namespace IYC_KUTUPHANE
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update KTPUYE set TC_KIMLIK_NO='" + textBox1.Text + "',ADI='" + textBox2.Text + "',SOYADI='" + textBox3.Text + "',DOGUM_TARIHI='" + dateTimePicker1.Value + "',DOGUM_YERI='" + textBox7.Text + "',TELEFON_NUMARASI='" + textBox5.Text + "',CINSIYET='" + comboBox1.Text + "',KAYIT_TARIHI='" + dateTimePicker2.Value + "' where SIRA_NO=" + textBox4.Text + "";
            cmd.ExecuteNonQuery();
            MessageBox.Show(textBox1.Text+" ÜYESİ GÜNCELLENDİ","BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            con.Close();
            // Form3 frm3 = new Form3(); VAR DERSEN GEREK KALMIYOR.
            var frm4 = (Form4)Application.OpenForms["Form4"];
            if (frm4 != null)
                frm4.comboaktif();
            var frm11 = (Form11)Application.OpenForms["Form11"];
            if (frm11 != null)
                frm11.comboaktifuye();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
        private bool Kapatsorgu;
        DialogResult dr = DialogResult.No;

        private void Form13_FormClosing(object sender, FormClosingEventArgs e)
        {
                if (!Kapatsorgu)
                {
                    dr = MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Kapatsorgu = dr == DialogResult.Yes;
                }
                if (dr == DialogResult.Yes)
                {
                    if (Kapatsorgu)
                    {

                        this.Hide();
                    }
                    Kapatsorgu = false;
                }
                else
                {
                    e.Cancel = true;
                }
            
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            if (label11.Text=="0")
            {
                button1.Enabled = true;
                button1.Visible = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox1.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox7.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox7.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox5.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox7.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                
            }
            if (e.KeyCode == Keys.Down)
            {
                dateTimePicker1.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                
            }
            if (e.KeyCode == Keys.Down)
            {
                dateTimePicker2.Focus();
            }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                dateTimePicker1.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                
            }
            if (e.KeyCode == Keys.Down)
            {
                button3.Focus();
            }
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                dateTimePicker2.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox1.Focus();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update KTPUYE set DURUM=1 where SIRA_NO=" + textBox4.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ÜYE AKTİF EDİLDİ","BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            var frm4 = (Form4)Application.OpenForms["Form4"];
            if (frm4 != null)
                frm4.comboaktif();
            frm4.comboBox2.SelectedIndex = 1;
        }
    }
}
