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
    public partial class Form8 : Form
    {
        public Form8()
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
            da = new SqlDataAdapter("Select * From KTPUYE where ADI like '" + textBox6.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KTPUYE");
            dataGridView1.DataSource = ds.Tables["KTPUYE"];
            con.Close();
        }
        void tplm()
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT  * From KTPUYE";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label2.Text = dt.Rows.Count.ToString();
        }
        void vrln()
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT  * From SEPET";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label4.Text = dt.Rows.Count.ToString();
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
        private void Form8_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBox6;
            tplm();
            dataGridView1.AllowUserToAddRows = false;
            comboaktif();
            comboBox1.SelectedIndex = 1;
            vrln();
            int sayi, sayi1, toplam;
            sayi = Convert.ToInt32(label2.Text);
            sayi1 = Convert.ToInt32(label4.Text);
            toplam = sayi - sayi1;
            label6.Text = toplam.ToString();
            ad();
            dataGridView1.Columns[0].Visible = false; dataGridView1.Columns[10].Visible = false;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            this.Hide();
             
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (int.Parse(dataGridView1.Rows[0].Cells["DURUM"].Value.ToString()) == 0)
            {
                dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Gray;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox6.Focus();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
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
