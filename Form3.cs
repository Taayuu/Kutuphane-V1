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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        void griddoldur()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP where KITAP_ADI like '" + textBox7.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            con.Close();
        }
        public void comboaktif()
        {

            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=1 and KITAP_ADI like '" + textBox7.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            con.Close();

        }

        void combopasif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=0 and KITAP_ADI like '" + textBox7.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            con.Close();
        }
        void ad()
        {
            dataGridView1.Columns[1].HeaderText = "KİTAP ADI";
            dataGridView1.Columns[2].HeaderText = "YAZAR ADI";
            dataGridView1.Columns[3].HeaderText = "KİTAP TÜRÜ";
            dataGridView1.Columns[4].HeaderText = "SAYFA SAYISI";
            dataGridView1.Columns[5].HeaderText = "YAYIN EVİ";
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1; //textBox1.Focus();
            dataGridView1.AllowUserToAddRows = false;
            numericUpDown1.Minimum = 1;
            comboBox1.SelectedIndex = 1;
            ad();
            numericUpDown1.Value = 1;
            comboaktif();
            label9.Text = "";
            dataGridView1.Columns["SIRA_NO"].Visible = false;
            dataGridView1.Columns["durum"].Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                if (MessageBox.Show("EMİN MİSİNİZ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    menu.label7.Text = label13.Text;
                    menu.Show();
                    this.Hide();
                }
            }
            else
            {
                menu.label7.Text = label13.Text;
                menu.Show();
                this.Hide();
            }


        }
        bool muk;
        void mukerrer()
        {
            //con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=yusuf;Integrated Security=True;");
            //da = new SqlDataAdapter("Select * From KITAP WHERE SIRA_NO='" + label11.Text + "' || KITAP_ADI='" + textBox1.Text + "' || YAZAR_ADI='" + textBox2.Text + "'|| KITAP_TURU='" + textBox3.Text + "'|| SAYFA_SAYISI='" + textBox4.Text + "'|| YAYIN_EVI='" + textBox5.Text + "'", con);
            //ds = new DataSet();
            //con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    muk = false;
            //}
            //else
            //{
            //    muk = true;
            //}
            //con.Close();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from KITAP WHERE KITAP_ADI='" + textBox1.Text + "' and YAZAR_ADI='" + textBox2.Text + "'and SAYFA_SAYISI='" + textBox4.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (MessageBox.Show(textBox1.Text + " KİTABI ZATEN VAR. YİNEDE EKLENSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (textBox1.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                    {
                        MessageBox.Show("LÜTFEN BİLGİLERİ DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            dr.Close();
                            label9.Text = "";
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "insert into KITAP (KITAP_ADI,YAZAR_ADI,KITAP_TURU,SAYFA_SAYISI,YAYIN_EVI,STOK,DURUM) values ( @KITAP_ADI,@YAZAR_ADI,@KITAP_TURU,@SAYFA_SAYISI,@YAYIN_EVI,@STOK,@DURUM)";
                            cmd.Parameters.AddWithValue("@KITAP_ADI", textBox1.Text);
                            cmd.Parameters.AddWithValue("@YAZAR_ADI", textBox2.Text);
                            cmd.Parameters.AddWithValue("@KITAP_TURU", textBox3.Text);
                            cmd.Parameters.AddWithValue("@SAYFA_SAYISI", textBox4.Text);
                            cmd.Parameters.AddWithValue("@YAYIN_EVI", textBox5.Text);
                            cmd.Parameters.AddWithValue("@STOK", numericUpDown1.Value);
                            cmd.Parameters.AddWithValue("@DURUM", 1);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            comboaktif();
                            label9.Text = textBox1.Text + " kitabı eklendi.";
                            timer1.Interval = 3000;
                            timer1.Enabled = true;
                            //Form7 frm7 = new Form7();
                            //string num = numericUpDown1.Value.ToString();
                            //label12.Text = label12.Text + num;
                        }
                        catch (Exception aaa)
                        {
                            label9.Text = aaa.Message;
                            con.Close();
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
                con.Close();
            }
            else
            {
                if (textBox1.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("LÜTFEN BİLGİLERİ DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show(textBox1.Text + " KİTABI EKLENSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dr.Close();
                            label9.Text = "";
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "insert into KITAP (KITAP_ADI,YAZAR_ADI,KITAP_TURU,SAYFA_SAYISI,YAYIN_EVI,STOK,DURUM) values ( @KITAP_ADI,@YAZAR_ADI,@KITAP_TURU,@SAYFA_SAYISI,@YAYIN_EVI,@STOK,@DURUM)";
                            cmd.Parameters.AddWithValue("@KITAP_ADI", textBox1.Text);
                            cmd.Parameters.AddWithValue("@YAZAR_ADI", textBox2.Text);
                            cmd.Parameters.AddWithValue("@KITAP_TURU", textBox3.Text);
                            cmd.Parameters.AddWithValue("@SAYFA_SAYISI", textBox4.Text);
                            cmd.Parameters.AddWithValue("@YAYIN_EVI", textBox5.Text);
                            cmd.Parameters.AddWithValue("@STOK", numericUpDown1.Value);
                            cmd.Parameters.AddWithValue("@DURUM", 1);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            comboaktif();
                            label9.Text = textBox1.Text + " kitabı eklendi.";
                            timer1.Interval = 3000;
                            timer1.Enabled = true;
                        }
                        catch (Exception aaa)
                        {
                            label9.Text = aaa.Message;
                            con.Close();
                        }
                        finally
                        {
                            // con.Close();
                        }
                    }
                    con.Close();
                }
            }
            con.Close();


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count!=0)
            {
                if (int.Parse(dataGridView1.Rows[0].Cells["DURUM"].Value.ToString()) == 1)
                {
                    if (MessageBox.Show(label11.Text + " KİTABI SİLİNSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        label9.Text = label11.Text + " kitabı silindi.";
                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "update KITAP set DURUM=0 where SIRA_NO=" + textBox6.Text + "";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        comboaktif();
                        timer1.Interval = 3000;
                        timer1.Enabled = true;
                        //Form7 frm7 = new Form7();
                        //int num = Convert.ToInt32(dataGridView1.CurrentRow.Cells["STOK"].Value);
                        //int txt = Convert.ToInt32(frm7.label7.Text);
                        //int sonuc = txt - num;
                        //frm7.label7.Text = sonuc.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("BU İŞLEM GERÇEKLEŞTİRİLEMİYOR", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("SİLİNECEK KİTAP YOK.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label11.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            //numericUpDown1.Value = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[6].Value);
        }


        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            numericUpDown1.Value = 1;
            textBox1.Focus();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            //
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox1.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
            }
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox4.Focus();
            }
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox5.Focus();
            }
        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
            {
                textBox4.Focus();
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

        private void NumericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }

        private void Button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                button3.Focus();
            }
            if (e.KeyCode == Keys.Left)
            {
                button1.Focus();
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                comboaktif();
            }
            if (comboBox1.SelectedIndex == 0)
            {
                griddoldur();
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

        private void textBox6_Leave(object sender, EventArgs e)
        {

        }

        private void textBox6_Leave_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count!=0)
            {
                Form12 frm12 = new Form12();
                frm12.textBox1.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frm12.textBox2.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frm12.textBox3.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                frm12.textBox4.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frm12.textBox5.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                frm12.textBox6.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
                frm12.numericUpDown1.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
                frm12.label9.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
                frm12.ShowDialog();
            }
            else
            {
                MessageBox.Show("GÜNCELLENECEK KİTAP YOK.","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label9.Text != null)
            {
                timer1.Interval = 3000;
                label9.Text = "";
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //if (numericUpDown1.Value==0)
            //{
            //    numericUpDown1.Value = 1;
            //}
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Left)
            {
                this.ActiveControl = textBox1;
            }
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                button2.Focus();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox7.Focus();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                griddoldur();
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
    }
}
