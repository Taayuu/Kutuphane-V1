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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update KITAP set KITAP_ADI='" + textBox2.Text + "',YAZAR_ADI='" + textBox3.Text + "',KITAP_TURU='" + textBox4.Text + "',SAYFA_SAYISI='" + textBox5.Text + "',YAYIN_EVI='" + textBox6.Text + "',STOK='" + numericUpDown1.Value + "' where SIRA_NO=" + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(textBox2.Text+" KİTABI GÜNCELLENDİ","BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            // Form3 frm3 = new Form3(); VAR DERSEN GEREK KALMIYOR.
            var frm3 = (Form3)Application.OpenForms["Form3"];
            if (frm3 != null)
                frm3.comboaktif();
            var frm11 = (Form11)Application.OpenForms["Form11"];
            if (frm11 != null)
                frm11.comboaktif();
            //Form7 frm7 = new Form7();
            //int num = Convert.ToInt32(numericUpDown1.Value);
            //int data = Convert.ToInt32(frm3.dataGridView1.CurrentRow.Cells["STOK"].Value);
            //int txt = Convert.ToInt32(frm7.label7.Text);
            //int sonuc = num - data;
            //int sonuc2 = data - num;
            //if (num>data)
            //{
            //    frm7.label7.Text = sonuc.ToString();
            //}
            //else if (data>num)
            //{
            //    frm7.label7.Text = sonuc2.ToString();
            //}
            //else if (num==data)
            //{

            //}
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
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
                textBox4.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
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

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox4.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox6.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox6.Focus();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                numericUpDown1.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                numericUpDown1.Focus();
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.Focus();
            }
            if (e.KeyCode == Keys.Right)
            {
                button4.Focus();
            }
        }

        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                //numericUpDown1.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update KITAP set KITAP_ADI='" + textBox2.Text + "',YAZAR_ADI='" + textBox3.Text + "',KITAP_TURU='" + textBox4.Text + "',SAYFA_SAYISI='" + textBox5.Text + "',YAYIN_EVI='" + textBox6.Text + "',STOK='" + numericUpDown1.Value + "' where SIRA_NO=" + textBox1.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                // Form3 frm3 = new Form3(); VAR DERSEN GEREK KALMIYOR.
                var frm3 = (Form3)Application.OpenForms["Form3"];
                if (frm3 != null)
                    frm3.comboaktif();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox2.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private bool Kapatsorgu;
        DialogResult dr = DialogResult.No;

        private void Form12_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Form12_Load(object sender, EventArgs e)
        {
            textBox2.Focus();
            if (label9.Text == "0")
            {
                button1.Visible = true;
                button1.Enabled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update KITAP set DURUM=1 where SIRA_NO=" + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("ÜYE AKTİF EDİLDİ", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var frm3 = (Form3)Application.OpenForms["Form3"];
            if (frm3 != null)
                frm3.comboaktif();
            frm3.comboBox1.SelectedIndex = 1;
        }
    }
}
