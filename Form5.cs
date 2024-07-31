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
using System.Linq.Expressions;

namespace IYC_KUTUPHANE
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        public void gridara1()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP where DURUM=1 AND KITAP_ADI like '" + textBox1.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            con.Close();
        }
        void aduye()
        {
            dataGridView2.Columns[1].HeaderText = "TC NO";
            dataGridView2.Columns[2].HeaderText = "ADI";
            dataGridView2.Columns[3].HeaderText = "SOYADI";
            dataGridView2.Columns[4].HeaderText = "DOĞUM TARİHİ";
            dataGridView2.Columns[5].HeaderText = "DOĞUM YERİ";
            dataGridView2.Columns[6].HeaderText = "TELEFON";
            dataGridView2.Columns[7].HeaderText = "CİNSİYET";
            dataGridView2.Columns[8].HeaderText = "KAYIT TARİHİ";
            dataGridView2.Columns[9].HeaderText = "OKUDUĞU KİTAP SAYISI";
        }
        void ad()
        {
            dataGridView1.Columns[1].HeaderText = "KİTAP ADI";
            dataGridView1.Columns[2].HeaderText = "YAZAR ADI";
            dataGridView1.Columns[3].HeaderText = "KİTAP TÜRÜ";
            dataGridView1.Columns[4].HeaderText = "SAYFA SAYISI";
            dataGridView1.Columns[5].HeaderText = "YAYIN EVİ";
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            dateTimePicker1.Value = DateTime.Today;
            comboaktif();
            comboaktifuye();
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(15);
            dataGridView1.Columns["DURUM"].Visible = false;
            dataGridView1.Columns["SIRA_NO"].Visible = false;
            dataGridView2.Columns["SIRA_NO"].Visible = false;
            dataGridView2.Columns["DURUM"].Visible = false;
            this.ActiveControl = dataGridView1;
            // dateTimePicker1.MaxDate = DateTime.Today;
            // dateTimePicker1.MinDate = DateTime.Today;   
            aduye(); ad();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            gridara1();
            if (textBox1.Text=="")
            {
                comboaktif();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (dataGridView2.Rows.Count!=0&&dataGridView1.Rows.Count!=0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from SEPET WHERE DURUM=1 AND KITAP_ADI='" + textBox7.Text + "' AND TC_NO='" + textBox3.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (MessageBox.Show("ÜYE ZATEN BU KİTABA SAHİP. DEVAM EDİLSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        if (int.Parse(dataGridView1.CurrentRow.Cells["STOK"].Value.ToString()) > 0)
                        {
                            if (MessageBox.Show("Kitabı emanet vermek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                dr.Close();
                                cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandText = "insert into SEPET(SIRA_NO,KITAP_ADI,YAZAR_ADI,KITAP_TURU,SAYFA_SAYISI,ADET,SIRA_NO2,TC_NO,ADI,SOYADI,TELEFON_NO,ALINMA_TARIHI,IADE_TARIHI,DURUM) VALUES (@SIRA_NO,@KITAP_ADI,@YAZAR_ADI,@KITAP_TURU,@SAYFA_SAYISI,@ADET,@SIRA_NO2,@TC_NO,@ADI,@SOYADI,@TELEFON_NO,@ALINMA_TARIHI,@IADE_TARIHI,@DURUM)";
                                cmd.Parameters.AddWithValue("SIRA_NO", textBox11.Text);
                                cmd.Parameters.AddWithValue("KITAP_ADI", textBox7.Text);
                                cmd.Parameters.AddWithValue("YAZAR_ADI", textBox8.Text);
                                cmd.Parameters.AddWithValue("KITAP_TURU", textBox9.Text);
                                cmd.Parameters.AddWithValue("SAYFA_SAYISI", textBox10.Text);
                                cmd.Parameters.AddWithValue("ADET", textBox12.Text);
                                cmd.Parameters.AddWithValue("DURUM", 1);
                                cmd.Parameters.AddWithValue("SIRA_NO2", label22.Text);
                                cmd.Parameters.AddWithValue("TC_NO", textBox3.Text);
                                cmd.Parameters.AddWithValue("ADI", textBox4.Text);
                                cmd.Parameters.AddWithValue("SOYADI", textBox5.Text);
                                cmd.Parameters.AddWithValue("TELEFON_NO", textBox6.Text);
                                cmd.Parameters.AddWithValue("ALINMA_TARIHI", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                                cmd.Parameters.AddWithValue("IADE_TARIHI", dateTimePicker2.Value.ToString("dd.MM.yyyy"));
                                cmd.ExecuteNonQuery();
                                SqlCommand cmd2 = new SqlCommand("UPDATE KITAP SET STOK=STOK-'" + int.Parse(textBox12.Text) + "' WHERE SIRA_NO='" + textBox11.Text + "'");
                                cmd2.Connection = con;
                                cmd2.ExecuteNonQuery();
                                con.Close();
                                if (MessageBox.Show("Kitap emanet verildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    comboaktif();
                                    comboaktifuye();
                                    con.Close();
                                }
                            }
                            else
                            {
                                con.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("STOKTA YETERLİ KİTAP OLMADIĞINDAN İŞLEM GERÇEKLEŞTİRİLEMİYOR", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            con.Close();
                        }
                    }
                    else
                    {
                        con.Close();
                    }
                }
                else
                {

                    if (int.Parse(dataGridView1.CurrentRow.Cells["STOK"].Value.ToString()) > 0)
                    {
                        if (MessageBox.Show("Kitabı emanet vermek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dr.Close();
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "insert into SEPET(SIRA_NO,KITAP_ADI,YAZAR_ADI,KITAP_TURU,SAYFA_SAYISI,ADET,SIRA_NO2,TC_NO,ADI,SOYADI,TELEFON_NO,ALINMA_TARIHI,IADE_TARIHI,DURUM) VALUES (@SIRA_NO,@KITAP_ADI,@YAZAR_ADI,@KITAP_TURU,@SAYFA_SAYISI,@ADET,@SIRA_NO2,@TC_NO,@ADI,@SOYADI,@TELEFON_NO,@ALINMA_TARIHI,@IADE_TARIHI,@DURUM)";
                            cmd.Parameters.AddWithValue("SIRA_NO", textBox11.Text);
                            cmd.Parameters.AddWithValue("KITAP_ADI", textBox7.Text);
                            cmd.Parameters.AddWithValue("YAZAR_ADI", textBox8.Text);
                            cmd.Parameters.AddWithValue("KITAP_TURU", textBox9.Text);
                            cmd.Parameters.AddWithValue("SAYFA_SAYISI", textBox10.Text);
                            cmd.Parameters.AddWithValue("ADET", textBox12.Text);
                            cmd.Parameters.AddWithValue("DURUM", 1);
                            cmd.Parameters.AddWithValue("SIRA_NO2", label22.Text);
                            cmd.Parameters.AddWithValue("TC_NO", textBox3.Text);
                            cmd.Parameters.AddWithValue("ADI", textBox4.Text);
                            cmd.Parameters.AddWithValue("SOYADI", textBox5.Text);
                            cmd.Parameters.AddWithValue("TELEFON_NO", textBox6.Text);
                            cmd.Parameters.AddWithValue("ALINMA_TARIHI", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                            cmd.Parameters.AddWithValue("IADE_TARIHI", dateTimePicker2.Value.ToString("dd.MM.yyyy"));
                            cmd.ExecuteNonQuery();
                            SqlCommand cmd2 = new SqlCommand("UPDATE KITAP SET STOK=STOK-'" + int.Parse(textBox12.Text) + "' WHERE SIRA_NO='" + textBox11.Text + "'");
                            cmd2.Connection = con;
                            cmd2.ExecuteNonQuery();
                            con.Close();
                            if (MessageBox.Show("Kitap emanet verildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                comboaktif();
                                comboaktifuye();
                                con.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("STOKTA YETERLİ KİTAP OLMADIĞINDAN İŞLEM GERÇEKLEŞTİRİLEMİYOR", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("KİTAP VEYA ÜYE SEÇİLİ DEĞİL.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            //}
            //catch (Exception hata)
            //{
                //label24.Text = hata.Message;
                //if (label24.Text==hata.Message)
                //{
                //    MessageBox.Show("BİR KİTAP SEÇİNİZ","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //    con.Close();
                //}
                //con.Close();  
               
            //}
            //con.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.label7.Text = label23.Text;
            frm2.Show();
            this.Hide();
        }
        public void gridara2()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE where DURUM=1 AND ADI like '" + textBox2.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView2.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            gridara2();
            if (textBox2.Text=="")
            {
                comboaktifuye();
            }
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           label22.Text= dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            label11.Text = dataGridView2.CurrentRow.Cells["OKUDUGU_KITAP_SAYISI"].Value.ToString();
        }

        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox11.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            label17.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value);
        }

        private void TextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.ShowDialog();
            var frm9 = (Form9)Application.OpenForms["Form9"];
            if (frm9 != null)
                frm9.comboaktif();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9();
            f9.label8.Text = textBox7.Text;
            f9.timer1.Start();
            f9.label7.Text = textBox3.Text;
            f9.ShowDialog();
            var frm9 = (Form9)Application.OpenForms["Form9"];
            if (frm9 != null)
                frm9.comboaktif();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox12.Text) < 0 || Convert.ToInt32(textBox12.Text) > 3)
                {
                    MessageBox.Show("En Az 1 En Fazla 3 Kitap Verilebilir!");
                    textBox12.Text = "";
                    textBox12.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Sayısal bir değer girin");
                textBox12.Text = "";
                textBox12.Focus();
            }
        }
        void combopasif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=0", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            con.Close();
        }
        public void comboaktif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=1", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                gridara1();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                comboaktif();
            }
            if (comboBox1.SelectedIndex == 2)
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
        public void comboaktifuye()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE WHERE DURUM=1", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView2.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }
        void combopasifuye()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE WHERE DURUM=0", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView2.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Focus();
        }
    }
}
