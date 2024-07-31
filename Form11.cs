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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

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

        private void Form11_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            griddoldur();
            comboaktif();
            dataGridView2.AllowUserToAddRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["DURUM"].Visible = false;
            //dataGridView2.Columns["DURUM"].Visible = false;
            dataGridView1.Columns["SIFRE"].Visible = false;
            dataGridView2.Columns["SIRA_NO"].Visible = false;
            groupBox2.Visible = false;
            label8.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 0;
            label12.Text = "";
            if (dataGridView2.Rows.Count != 0)
            {
                label14.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            }
            else
            {
                label14.Text = "";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("K.ADI VEYA ŞİFRE BOŞ OLAMAZ!!!");
            }
            else
            {
                MessageBoxManager.Yes = "Yönetici";
                MessageBoxManager.No = "Üye";
                MessageBoxManager.Register();
                if (MessageBox.Show("Kişi ne olarak eklensin?", "Kişi Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into KADI (KADI,SIFRE,DURUM) VALUES (@KADI,@SIFRE,@DURUM)");
                    cmd.Parameters.AddWithValue("@KADI", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SIFRE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@DURUM", 1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("YÖNETİCİ EKLENDİ", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    griddoldur();
                    MessageBoxManager.Unregister();
                }
                else if (MessageBox.Show("Kişi ne olarak eklensin?", "Kişi Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into KADI (KADI,SIFRE,DURUM) VALUES (@KADI,@SIFRE,@DURUM)");
                    cmd.Parameters.AddWithValue("@KADI", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SIFRE", textBox2.Text);
                    cmd.Parameters.AddWithValue("@DURUM", 0);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ÜYE EKLENDİ", "BİLGİLENDİRME", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    griddoldur();
                    MessageBoxManager.Unregister();
                }
                MessageBoxManager.Unregister();
            }
        }

        void griddoldur()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KADI where DURUM=1 or DURUM=0", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KADI");
            dataGridView1.DataSource = ds.Tables["KADI"];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxManager.Unregister();
            Form2 frm2 = new Form2();
            frm2.label7.Text = "1";
            frm2.Show();
            this.Hide();
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
            if (e.KeyCode == Keys.Up)
            {
                textBox1.Focus();
            }
            if (e.KeyCode == Keys.Left)
            {
                checkBox1.Focus();
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
                if (checkBox1.Checked == true)
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

        private void checkBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (checkBox2.Checked == true)
                {
                    checkBox2.Checked = false;
                    textBox4.Focus();
                }
                else
                {
                    checkBox1.Checked = true;
                    textBox4.Focus();
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            groupBox2.Visible = true;
            groupBox2.Enabled = true;
            groupBox1.Visible = false;
            groupBox1.Enabled = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox3.PasswordChar = '*';
                textBox4.PasswordChar = '*';
            }
            else
            {
                textBox3.PasswordChar = '\0';
                textBox4.PasswordChar = '\0';
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("ŞİFRE VE TEKRAR BOŞ OLAMAZ!!!");
            }
            else if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("ŞİFRELER AYNI DEĞİL!!!");
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update KADI set SIFRE='" + textBox4.Text + "'where Id=" + label8.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ŞİFRE DEĞİŞTİRİLDİ.");
                groupBox1.Visible = true;
                groupBox1.Enabled = true;
                groupBox2.Visible = false;
                groupBox2.Enabled = false;
                griddoldur();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            label8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label9.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            label12.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        void griddoldur1()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP where KITAP_ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView2.DataSource = ds.Tables["KITAP"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();
        }
        public void comboaktif()
        {

            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=1 AND KITAP_ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView2.DataSource = ds.Tables["KITAP"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();

        }

        void combopasif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=0 and KITAP_ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView2.DataSource = ds.Tables["KITAP"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();
        }
        void gizle()
        {
            if (dataGridView2.Columns[0] == dataGridView2.Columns["ID"])
            {
                dataGridView2.Columns["ID"].Visible = false;
            }
            if (dataGridView2.ColumnCount > 10)
            {
                if (dataGridView2.Columns[8] == dataGridView2.Columns["SIRA_NO2"])
                {
                    dataGridView2.Columns["SIRA_NO2"].Visible = false;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gizle();
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktif();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    griddoldur1();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasif();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 1)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktifuye();
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    griddolduruye();
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasifuye();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 2)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktifspt(); gizle();
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 0)
                {
                    griddoldurspt(); gizle();
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasifspt(); gizle();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 3)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    gridgeciken1(); gizle();
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 0)
                {
                    gridgeciken(); gizle();
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    gridgeciken2(); gizle();
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktif();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    griddoldur1();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasif();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktifuye();
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    griddolduruye();
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasifuye();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktifspt();
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    griddoldurspt();
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasifspt();
                }
            }
            ////////////////////////////////////
            if (comboBox1.SelectedIndex == 3)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    gridgeciken1(); gizle();
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 0)
                {
                    gridgeciken(); gizle();
                }
            }
            if (comboBox1.SelectedIndex == 3)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    gridgeciken2(); gizle();
                }
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (int.Parse(dataGridView2.Rows[i].Cells["DURUM"].Value.ToString()) == 0)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
            }
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (int.Parse(dataGridView2.Rows[i].Cells["DURUM"].Value.ToString()) == 2)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        void griddolduruye()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE where ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView2.DataSource = ds.Tables["KTPUYE"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();
        }

        public void comboaktifuye()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE WHERE DURUM=1 and ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView2.DataSource = ds.Tables["KTPUYE"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();
        }
        void combopasifuye()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE WHERE DURUM=0 and ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView2.DataSource = ds.Tables["KTPUYE"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();
        }
        void griddoldurspt()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET where ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView2.DataSource = ds.Tables["SEPET"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();

        }
        void combopasifspt()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET WHERE DURUM=0 and ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView2.DataSource = ds.Tables["SEPET"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();
        }
        void comboaktifspt()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET WHERE DURUM=1 and ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView2.DataSource = ds.Tables["SEPET"];
            textBox5.Visible = true;
            label13.Visible = true;
            button5.Visible = true;
            con.Close();
        }
        void gridgeciken()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET where Convert(date, IADE_TARIHI , 103) <= '" + DateTime.Now.ToString("MM.dd.yyyy") + "'", con);
            // da = new SqlDataAdapter("Select * From SEPET where Convert(date, IADE_TARIHI , 103) <= '06.22.2020'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView2.DataSource = ds.Tables["SEPET"];
            textBox5.Visible = false;
            label13.Visible = false;
            button5.Visible = false;
            con.Close();
        }
        void gridgeciken1()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET where DURUM=1 AND Convert(date, IADE_TARIHI , 103) <= '" + DateTime.Now.ToString("MM.dd.yyyy") + "'", con);
            // da = new SqlDataAdapter("Select * From SEPET where Convert(date, IADE_TARIHI , 103) <= '06.22.2020'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView2.DataSource = ds.Tables["SEPET"];
            textBox5.Visible = false;
            label13.Visible = false;
            button5.Visible = false;
            con.Close();
        }
        void gridgeciken2()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET where DURUM=0 AND Convert(date, IADE_TARIHI , 103) <= '" + DateTime.Now.ToString("MM.dd.yyyy") + "'", con);
            // da = new SqlDataAdapter("Select * From SEPET where Convert(date, IADE_TARIHI , 103) <= '06.22.2020'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView2.DataSource = ds.Tables["SEPET"];
            textBox5.Visible = false;
            label13.Visible = false;
            button5.Visible = false;
            con.Close();
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                cmdgncll();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                cmdgncllUYE();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                cmdgncllSPT();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                cmdgncllGCKN();
            }
        }
        void cmdgncll()
        {
            try
            {
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(ds, "KITAP");
                MessageBox.Show("KAYIT GÜNCELLENDİ");
                comboaktif();
            }
            catch (Exception hata)
            {
                label14.Text = hata.Message;
                if (label14.Text == hata.Message)
                {
                    label14.Text = "HERHANGİ BİR GÜNCELLEME YAPILMADI";
                }
            }

        }
        void cmdgncllUYE()
        {
            try
            {
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(ds, "KTPUYE");
                MessageBox.Show("KAYIT GÜNCELLENDİ");
                comboaktifuye();
            }
            catch (Exception hata)
            {
                label14.Text = hata.Message;
                if (label14.Text == hata.Message)
                {
                    label14.Text = "HERHANGİ BİR GÜNCELLEME YAPILMADI";
                }
            }
        }
        void cmdgncllSPT()
        {
            //try
            // {
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.Update(ds, "SEPET");
            MessageBox.Show("KAYIT GÜNCELLENDİ");
            comboaktifspt();
            // }
            //  catch (Exception hata)
            // {
            //    label14.Text = hata.Message;
            //     if (label14.Text == hata.Message)
            //     {
            label14.Text = "HERHANGİ BİR GÜNCELLEME YAPILMADI";
            //    }
            // }
        }
        void cmdgncllGCKN()
        {
            try
            {
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(ds, "SEPET");
                MessageBox.Show("KAYIT GÜNCELLENDİ");
                gridgeciken1();
            }
            catch (Exception hata)
            {
                label14.Text = hata.Message;
                if (label14.Text == hata.Message)
                {
                    label14.Text = "HERHANGİ BİR GÜNCELLEME YAPILMADI";
                }
            }
        }
        void yon()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KADI WHERE DURUM=1", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KADI");
            dataGridView1.DataSource = ds.Tables["KADI"];
            con.Close();
        }
        void uye()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KADI WHERE DURUM=0", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KADI");
            dataGridView1.DataSource = ds.Tables["KADI"];
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 1)
            {
                yon();
            }
            if (comboBox3.SelectedIndex == 2)
            {
                uye();
            }
            if (comboBox3.SelectedIndex == 0)
            {
                griddoldur();
            }
        }
        public void gridara1()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP where KITAP_ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView2.DataSource = ds.Tables["KITAP"];
            con.Close();
        }
        public void gridara2()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KTPUYE where ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView2.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }
        public void gridara3()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET where KITAP_ADI OR ADI like '" + textBox5.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView2.DataSource = ds.Tables["SEPET"];
            con.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktif();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    griddoldur1();
                }
            }
            if (comboBox1.SelectedIndex == 0)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasif();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 1)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktifuye();
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    griddolduruye();
                }
            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasifuye();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 2)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 1)
                {
                    comboaktifspt();
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                comboBox2.Visible = true;
                comboBox2.Enabled = true;
                if (comboBox2.SelectedIndex == 0)
                {
                    griddoldurspt();
                }
            }
            if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex == 2)
                {
                    combopasifspt();
                }
            }
            //////////////////////////////////////////////////
            if (comboBox1.SelectedIndex == 3)
            {
                gridgeciken();
                comboBox2.Visible = false;
                comboBox2.Enabled = false;
            }
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form11_MouseClick(object sender, MouseEventArgs e)
        {
            label14.Text = "";
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.Rows.Count!=0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    Form12 frm12 = new Form12();
                    frm12.textBox1.Text = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    frm12.textBox2.Text = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    frm12.textBox3.Text = this.dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    frm12.textBox4.Text = this.dataGridView2.CurrentRow.Cells[3].Value.ToString();
                    frm12.textBox5.Text = this.dataGridView2.CurrentRow.Cells[4].Value.ToString();
                    frm12.textBox6.Text = this.dataGridView2.CurrentRow.Cells[5].Value.ToString();
                    frm12.numericUpDown1.Text = this.dataGridView2.CurrentRow.Cells[6].Value.ToString();
                    frm12.label9.Text = this.dataGridView2.CurrentRow.Cells[7].Value.ToString();
                    frm12.ShowDialog();
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    Form13 frm13 = new Form13();
                    frm13.textBox4.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    frm13.textBox1.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    frm13.textBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    frm13.textBox3.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                    frm13.dateTimePicker1.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                    frm13.textBox7.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
                    frm13.textBox5.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
                    frm13.comboBox1.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
                    frm13.dateTimePicker2.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
                    frm13.label11.Text = dataGridView2.CurrentRow.Cells[10].Value.ToString();
                    frm13.ShowDialog();
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    label14.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    if (int.Parse(dataGridView2.CurrentRow.Cells["DURUM"].Value.ToString()) == 0)
                    {
                        MessageBoxManager.OK = "AKTİF YAP";
                        MessageBoxManager.Register();
                        if (MessageBox.Show("NE YAPMAK İSTERSİNİZ?", "SORU", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
                            SqlCommand cmd = new SqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "update SEPET set DURUM=1 where ID='" + label14.Text + "'";
                            cmd.ExecuteNonQuery();
                            con.Close();
                            comboaktifspt();
                            MessageBoxManager.Unregister();
                        }
                        else
                        {
                            con.Close();
                            MessageBoxManager.Unregister();
                        }
                        MessageBoxManager.Unregister();
                    }
                    else if (int.Parse(dataGridView2.CurrentRow.Cells["DURUM"].Value.ToString()) == 1)
                    {
                        MessageBoxManager.OK = "PASİF YAP";
                        MessageBoxManager.Register();
                        if (MessageBox.Show("NE YAPMAK İSTERSİNİZ?", "SORU", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
                            SqlCommand cmd = new SqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "update SEPET set DURUM=0 where ID='" + label14.Text + "'";
                            cmd.ExecuteNonQuery();
                            con.Close();
                            combopasifspt();
                            MessageBoxManager.Unregister();
                        }
                        else
                        {
                            MessageBoxManager.Unregister();
                            con.Close();
                        }
                        MessageBoxManager.Unregister();
                    }
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    label14.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    if (int.Parse(dataGridView2.CurrentRow.Cells["DURUM"].Value.ToString()) == 0)
                    {
                        MessageBoxManager.OK = "AKTİF YAP";
                        MessageBoxManager.Register();
                        if (MessageBox.Show("NE YAPMAK İSTERSİNİZ?", "SORU", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
                            SqlCommand cmd = new SqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "update SEPET set DURUM=1 where ID='" + label14.Text + "'";
                            cmd.ExecuteNonQuery();
                            con.Close();
                            gridgeciken1();
                            MessageBoxManager.Unregister();
                        }
                        else
                        {
                            MessageBoxManager.Unregister();
                            con.Close();
                        }
                        MessageBoxManager.Unregister();
                    }
                    else if (int.Parse(dataGridView2.CurrentRow.Cells["DURUM"].Value.ToString()) == 1)
                    {
                        MessageBoxManager.OK = "PASİF YAP";
                        MessageBoxManager.Register();
                        if (MessageBox.Show("NE YAPMAK İSTERSİNİZ?", "SORU", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
                            SqlCommand cmd = new SqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "update SEPET set DURUM=0 where ID='" + label14.Text + "'";
                            cmd.ExecuteNonQuery();
                            con.Close();
                            gridgeciken2();
                            MessageBoxManager.Unregister();
                        }
                        else
                        {
                            MessageBoxManager.Unregister();
                            con.Close();
                        }
                        MessageBoxManager.Unregister();
                    }
                }
            }

        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            label14.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(label12.Text + " ÜYESİ SİLİNSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from KADI where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", label8.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("ÜYE SİLİNDİ","BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                con.Close();
                griddoldur();
            }
            else
            {
                MessageBox.Show("BU İŞLEM GERÇEKLEŞTİRİLEMİYOR", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox1.Enabled = true;
            groupBox2.Visible = false;
            groupBox2.Enabled = false;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox5.Clear();
        }
    }
}
