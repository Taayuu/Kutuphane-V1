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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        
        public void griddoldur()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE where ADI like '" + textBox6.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView1.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }

        public void comboaktif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE WHERE DURUM=1 and ADI like '" + textBox6.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView1.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }
        public void combopasif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE WHERE DURUM=0 and ADI like '" + textBox6.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView1.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        void ad()
        {
            dataGridView1.Columns[1].HeaderText = "TC NO";
            dataGridView1.Columns[2].HeaderText = "ADI";
            dataGridView1.Columns[3].HeaderText = "SOYADI";
            dataGridView1.Columns[4].HeaderText = "DOĞUM TARİHİ";
            dataGridView1.Columns[5].HeaderText = "DOĞUM YERİ";
            dataGridView1.Columns[6].HeaderText = "TELEFON";
            dataGridView1.Columns[7].HeaderText = "CİNSİYET";
            dataGridView1.Columns[8].HeaderText = "KAYIT TARİHİ";
            dataGridView1.Columns[9].HeaderText = "OKUDUĞU KİTAP SAYISI";
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = 0.ToString();
            }
            comboaktif();
            dataGridView1.AllowUserToAddRows = false;
            textBox1.Focus();
            dataGridView1.Columns["SIRA_NO"].Visible = false;
            dataGridView1.Columns["DURUM"].Visible = false;
            dateTimePicker1.Value = new DateTime(1990,01,01);
            dateTimePicker2.Value = DateTime.Today;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            label10.Text = "";
            ad();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from KTPUYE WHERE TC_KIMLIK_NO='" + textBox1.Text + "' and TELEFON_NUMARASI='" + textBox5.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (MessageBox.Show(textBox2.Text + " ÜYESİ ZATEN VAR. YİNDE EKLENSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == "")
                    {
                        MessageBox.Show("LÜTFEN BİLGİLERİ DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                            dr.Close();
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "insert into KTPUYE (TC_KIMLIK_NO,ADI,SOYADI,DOGUM_TARIHI,DOGUM_YERI,TELEFON_NUMARASI,CINSIYET,KAYIT_TARIHI,OKUDUGU_KITAP_SAYISI,DURUM) values ( @TC_KIMLIK_NO,@ADI,@SOYADI,@DOGUM_TARIHI,@DOGUM_YERI,@TELEFON_NUMARASI,@CINSIYET,@KAYIT_TARIHI,@OKUDUGU_KITAP_SAYISI,@DURUM)";
                            cmd.Parameters.AddWithValue("@TC_KIMLIK_NO", textBox1.Text);
                            cmd.Parameters.AddWithValue("@ADI", textBox2.Text);
                            cmd.Parameters.AddWithValue("@SOYADI", textBox3.Text);
                            cmd.Parameters.AddWithValue("@DOGUM_TARIHI", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                            cmd.Parameters.AddWithValue("@DOGUM_YERI", textBox7.Text);
                            cmd.Parameters.AddWithValue("@TELEFON_NUMARASI", textBox5.Text);
                            cmd.Parameters.AddWithValue("@CINSIYET", comboBox1.Text);
                            cmd.Parameters.AddWithValue("@KAYIT_TARIHI", dateTimePicker2.Value.ToString("dd.MM.yyyy"));
                            cmd.Parameters.AddWithValue("@OKUDUGU_KITAP_SAYISI", 0);
                            cmd.Parameters.AddWithValue("@DURUM", 1);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            comboaktif();
                            label10.Text = textBox2.Text + " üyesi eklendi.";
                            timer1.Interval = 3000;
                        timer1.Start();
                    }
                }
                con.Close();
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("LÜTFEN BİLGİLERİ DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show(textBox2.Text + " ÜYESİ EKLENSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dr.Close();
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "insert into KTPUYE (TC_KIMLIK_NO,ADI,SOYADI,DOGUM_TARIHI,DOGUM_YERI,TELEFON_NUMARASI,CINSIYET,KAYIT_TARIHI,OKUDUGU_KITAP_SAYISI,DURUM) values ( @TC_KIMLIK_NO,@ADI,@SOYADI,@DOGUM_TARIHI,@DOGUM_YERI,@TELEFON_NUMARASI,@CINSIYET,@KAYIT_TARIHI,@OKUDUGU_KITAP_SAYISI,@DURUM)";
                        cmd.Parameters.AddWithValue("@TC_KIMLIK_NO", textBox1.Text);
                        cmd.Parameters.AddWithValue("@ADI", textBox2.Text);
                        cmd.Parameters.AddWithValue("@SOYADI", textBox3.Text);
                        cmd.Parameters.AddWithValue("@DOGUM_TARIHI", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                        cmd.Parameters.AddWithValue("@DOGUM_YERI", textBox7.Text);
                        cmd.Parameters.AddWithValue("@TELEFON_NUMARASI", textBox5.Text);
                        cmd.Parameters.AddWithValue("@CINSIYET", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@KAYIT_TARIHI", dateTimePicker2.Value.ToString("dd.MM.yyyy"));
                        cmd.Parameters.AddWithValue("@OKUDUGU_KITAP_SAYISI", 0);
                        cmd.Parameters.AddWithValue("@DURUM", 1);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        comboaktif();
                        label10.Text = textBox2.Text + " üyesi eklendi.";
                        timer1.Interval = 3000;
                        timer1.Start();
                    }
                }
            }con.Close();
              
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count!=0)
            {
                if (int.Parse(dataGridView1.Rows[0].Cells["DURUM"].Value.ToString()) == 1)
                {
                    if (MessageBox.Show(label11.Text + " ÜYESİ SİLİNSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        label10.Text = label11.Text + " üyesi silindi.";
                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "update KTPUYE set DURUM=0 where SIRA_NO=@SIRA_NO";
                        cmd.Parameters.AddWithValue("@SIRA_NO", textBox4.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        comboaktif();
                        timer1.Interval = 3000;
                        timer1.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("BU İŞLEM GERÇEKLEŞTİRİLEMİYOR", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("SİLİNECEK ÜYE YOK.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            //textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            //comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            //dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update KTPUYE set TC_KIMLIK_NO='" + textBox1.Text + "',ADI='" + textBox2.Text + "',SOYADI='" + textBox3.Text + "',DOGUM_TARIHI='" + dateTimePicker1.Value + "',DOGUM_YERI='" + textBox7.Text + "',TELEFON_NUMARASI='" + textBox5.Text + "',CINSIYET='" + comboBox1.Text + "',KAYIT_TARIHI='" + dateTimePicker2.Value + "' where SIRA_NO=" + textBox4.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox5.Text != "")
            {
                if (MessageBox.Show("EMİN MİSİNİZ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    menu.label7.Text = label12.Text;
                    menu.Show();
                    this.Hide();
                }
            }
            else
            {
                menu.label7.Text = label12.Text;
                menu.Show();
                this.Hide();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox7.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                textBox2.Focus();
            }
            if(e.KeyCode == Keys.Down)
            {
                textBox2.Focus();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox1.Focus();
            }
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox2.Focus();
            }
        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox7.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox7.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
            }
        }

        private void TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox5.Focus();
            }
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker1.Focus();
            }
        }

        private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker2.Focus();
            }
        }

        private void Button4_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void Form4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Button4_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8();
            frm8.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count!=0)
            {
                Form13 frm13 = new Form13();
                frm13.textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frm13.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frm13.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                frm13.textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frm13.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                frm13.textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                frm13.textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                frm13.comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                frm13.dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                frm13.label11.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                frm13.ShowDialog();
            }
            else
            {
                MessageBox.Show("GÜNCELLENECEK ÜYE YOK.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex==0)
            {
                griddoldur();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                comboaktif();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                combopasif();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (int.Parse(dataGridView1.Rows[i].Cells["DURUM"].Value.ToString()) == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label10.Text != "")
            {
                timer1.Interval = 3000;
                label10.Text = "";
                timer1.Stop();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox6.Focus();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                griddoldur();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                comboaktif();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                combopasif();
            }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void button1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                button2.Focus();
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                button1.Focus();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = 0.ToString();
            }
        }
    }
}
