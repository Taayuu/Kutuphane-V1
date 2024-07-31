using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IYC_KUTUPHANE
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        SqlConnection con;

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.label7.Text = label5.Text;
            frm2.Show();
            this.Hide();
        }
        //DateTime trh1=new DateTime()
        //int result= DateTime.Compare()
        void gridgeciken()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET where DURUM=1 AND Convert(date, IADE_TARIHI , 103) <= '" + DateTime.Now.ToString("MM.dd.yyyy") +"'" , con);
            // da = new SqlDataAdapter("Select * From SEPET where Convert(date, IADE_TARIHI , 103) <= '06.22.2020'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView1.DataSource = ds.Tables["SEPET"];
            con.Close();
        }
        void ad()
        {
            dataGridView1.Columns[2].HeaderText = "KİTAP ADI";
            dataGridView1.Columns[3].HeaderText = "YAZAR ADI";
            dataGridView1.Columns[4].HeaderText = "KİTAP TÜRÜ";
            dataGridView1.Columns[5].HeaderText = "SAYFA SAYISI";
            dataGridView1.Columns[6].HeaderText = "ADET";
            dataGridView1.Columns[9].HeaderText = "TC NO";
            dataGridView1.Columns[10].HeaderText = "ADI";
            dataGridView1.Columns[11].HeaderText = "SOYADI";
            dataGridView1.Columns[12].HeaderText = "TELEFON NO";
            dataGridView1.Columns[13].HeaderText = "ALINMA TARİHİ";
            dataGridView1.Columns[14].HeaderText = "İADE TARİHİ";
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            gridgeciken();
            // dateTimePicker2.Value = dateTimePicker1.Value.AddDays(15);
            dateTimePicker1.Value = DateTime.Now;
            // dateTimePicker1.MinDate = DateTime.Today;
            dataGridView1.Columns["SIRA_NO"].Visible = false;
            dataGridView1.Columns["SIRA_NO2"].Visible = false;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["DURUM"].Visible = false;
            ad();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count!=0)
            {
                if (int.Parse(dataGridView1.Rows[0].Cells["DURUM"].Value.ToString()) == 1)
                {
                    if (MessageBox.Show("Kitabı almak istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "update SEPET set DURUM=0 where ID=@ID";
                        cmd.Parameters.AddWithValue("@ID", label1.Text);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("UPDATE KITAP SET STOK=STOK+'" + int.Parse(label2.Text) + "' WHERE SIRA_NO='" + label3.Text + "'");
                        cmd2.Connection = con;
                        cmd2.ExecuteNonQuery();
                        SqlCommand cmd3 = new SqlCommand("UPDATE KTPUYE SET OKUDUGU_KITAP_SAYISI=OKUDUGU_KITAP_SAYISI+'" + int.Parse(label2.Text) + "' WHERE SIRA_NO='" + label4.Text + "'");
                        cmd3.Connection = con;
                        cmd3.ExecuteNonQuery();
                        con.Close();
                        if (MessageBox.Show("Kitap alındı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            gridgeciken();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("BU İŞLEM GERÇEKLEŞTİRİLEMİYOR", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("GECİKMİŞ EMANET YOK", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
            
            
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            label3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label4.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (int.Parse(dataGridView1.Rows[i].Cells["DURUM"].Value.ToString()) == 1)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }
    }
}
