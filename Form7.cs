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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void griddoldur()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP where KITAP_ADI like '" + textBox7.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            DataTable dt = new DataTable();
            da.Fill(dt);
            label2.Text = dt.Rows.Count.ToString();
            con.Close();
            label6.Visible = false;
            label5.Visible = false;
            label4.Visible = false;
            label3.Visible = false;
            
        }

     

        void vrln()
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT  * From SEPET WHERE DURUM=1";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label4.Text = dt.Rows.Count.ToString();
        }
        void ad()
        {
            dataGridView1.Columns[1].HeaderText = "KİTAP ADI";
            dataGridView1.Columns[2].HeaderText = "YAZAR ADI";
            dataGridView1.Columns[3].HeaderText = "KİTAP TÜRÜ";
            dataGridView1.Columns[4].HeaderText = "SAYFA SAYISI";
            dataGridView1.Columns[5].HeaderText = "YAYIN EVİ";
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            label2.Text = "0"; label7.Text = "0"; label6.Text = "0"; label4.Text = "0";
            dataGridView1.AllowUserToAddRows = false;
            this.ActiveControl = textBox7;
            comboBox1.SelectedIndex = 1; 
            comboaktif();
            vrln();
            dataGridView1.Columns["SIRA_NO"].Visible = false;
            dataGridView1.Columns["durum"].Visible = false;
            int sayi, sayi1, toplam2;
            sayi = Convert.ToInt32(label7.Text);
            sayi1 = Convert.ToInt32(label4.Text);
            toplam2 = sayi - sayi1;
            label6.Text = toplam2.ToString();
            ad();
            ;        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            this.Hide();
        }
        void combopasif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=0 and KITAP_ADI like '" + textBox7.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            DataTable dt = new DataTable();
            da.Fill(dt);
            label2.Text = dt.Rows.Count.ToString();
            con.Close();
            label6.Visible = false;
            label5.Visible = false;
            label4.Visible = false;
            label3.Visible = false;
        }
        void comboaktif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From KITAP WHERE DURUM=1 and KITAP_ADI like '" + textBox7.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KITAP");
            dataGridView1.DataSource = ds.Tables["KITAP"];
            DataTable dt = new DataTable();
            da.Fill(dt);
            label2.Text = dt.Rows.Count.ToString();
            con.Close();
            label6.Visible = true;
            label5.Visible = true;
            label4.Visible = true;
            label3.Visible = true;
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
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (int.Parse(dataGridView1.Rows[i].Cells["DURUM"].Value.ToString()) == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox7.Focus();
        }
    }
}
