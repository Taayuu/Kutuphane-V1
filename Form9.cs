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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        SqlConnection con;
        void griddoldur()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET", con); 
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView1.DataSource = ds.Tables["SEPET"];
            con.Close();
            DataTable dt = new DataTable();
            da.Fill(dt);
            label2.Text = dt.Rows.Count.ToString();
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
        private void Form9_Load(object sender, EventArgs e)
        {
            comboaktif();
            comboBox1.SelectedIndex = 1;
            dataGridView1.AllowUserToAddRows = false;
            //comboaktif();
            dataGridView1.Columns["SIRA_NO"].Visible = false;
            dataGridView1.Columns["SIRA_NO2"].Visible = false;
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["DURUM"].Visible = false;
            //SECİLİ();
            ad();
            
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            this.Hide(); 
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count!=0)
            {
                if (int.Parse(dataGridView1.CurrentRow.Cells["DURUM"].Value.ToString()) == 1)
                {
                    if (MessageBox.Show("Kitabı almak istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "update SEPET set DURUM=0 where ID=@ID";
                        cmd.Parameters.AddWithValue("@ID", label3.Text);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("UPDATE KITAP SET STOK=STOK+'" + int.Parse(label4.Text) + "' WHERE SIRA_NO='" + label5.Text + "'");
                        cmd2.Connection = con;
                        cmd2.ExecuteNonQuery();
                        SqlCommand cmd3 = new SqlCommand("UPDATE KTPUYE SET OKUDUGU_KITAP_SAYISI=OKUDUGU_KITAP_SAYISI+'" + int.Parse(label4.Text) + "' WHERE SIRA_NO='" + label6.Text + "'");
                        cmd3.Connection = con;
                        cmd3.ExecuteNonQuery();
                        con.Close();
                        if (MessageBox.Show("Kitap alındı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            comboaktif();
                            var frm5 = (Form5)Application.OpenForms["Form5"];
                            if (frm5 != null)
                                frm5.comboaktif();
                            frm5.comboaktifuye();
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
                MessageBox.Show("ALINACAK EMANET YOK.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
            
        }

        private void dataGridView1_AllowUserToDeleteRowsChanged(object sender, EventArgs e)
        {
            //   if (MessageBox.Show("Kitabı almak istiyormusunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            label3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            label5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label6.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
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
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (int.Parse(dataGridView1.Rows[i].Cells["DURUM"].Value.ToString()) == 2)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //}
        void combopasif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET WHERE DURUM=0", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView1.DataSource = ds.Tables["SEPET"];
            con.Close();
            DataTable dt = new DataTable();
            da.Fill(dt);
            label2.Text = dt.Rows.Count.ToString();
        }
        public void comboaktif()
        {
            con = new SqlConnection("Data Source=(localdb)\\ysf;AttachDbFilename=|DataDirectory|\\yusuf.mdf;Initial Catalog=yusuf;Integrated Security=true;");
            da = new SqlDataAdapter("Select * From SEPET WHERE DURUM=1", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "SEPET");
            dataGridView1.DataSource = ds.Tables["SEPET"];
            con.Close();
            DataTable dt = new DataTable();
            da.Fill(dt);
            label2.Text = dt.Rows.Count.ToString();
        }
       public void SECİLİ()
        {
            timer1.Stop();
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from SEPET WHERE DURUM=1 AND TC_NO='" + label7.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                cmd.Connection = con;
                da = new SqlDataAdapter("Select * From SEPET WHERE DURUM=1 AND TC_NO='" + label7.Text + "'", con);
                ds = new DataSet();
                da.Fill(ds, "SEPET");
                dataGridView1.DataSource = ds.Tables["SEPET"];
                con.Close();
                label1.Text = ""; label2.Text="";
                timer1.Stop();
            }
            else if (MessageBox.Show("KİŞİYE AİT EMANET BULUNMAMAKTADIR.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
                con.Close();
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count!=0)
            {
                if (int.Parse(dataGridView1.CurrentRow.Cells["DURUM"].Value.ToString()) == 1)
                {
                    if (MessageBox.Show("KAYIT İPTAL EDİLSİN Mİ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand();
                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "update SEPET set DURUM=2 where ID=@ID";
                        cmd.Parameters.AddWithValue("@ID", label3.Text);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("UPDATE KITAP SET STOK=STOK+'" + int.Parse(label4.Text) + "' WHERE SIRA_NO='" + label5.Text + "'");
                        cmd2.Connection = con;
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        if (MessageBox.Show("KAYIT SİLİNDİ.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            comboaktif();
                            var frm5 = (Form5)Application.OpenForms["Form5"];
                            if (frm5 != null)
                                frm5.comboaktif();
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
                MessageBox.Show("BİLDİRİLECEK EMANET YOK.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label7.Text != "")
            {
                timer1.Interval = 100;
                SECİLİ();
            }
        }
    }
}
