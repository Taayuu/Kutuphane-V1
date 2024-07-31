using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IYC_KUTUPHANE
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        

        private void Button1_Click(object sender, EventArgs e)
        {
            Form3 detay = new Form3();
            detay.label13.Text = label7.Text;    
            detay.Show();
                this.Hide();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form4 üyeler = new Form4();
            üyeler.Show();
            this.Hide();
            üyeler.label12.Text = label7.Text;      
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form5 emanet = new Form5();
            emanet.label23.Text = label7.Text;
            emanet.Show();
                this.Hide();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
            
                
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8();
            frm8.ShowDialog();
            
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Form9 frm9 = new Form9();
            frm9.ShowDialog();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form10 frm10 = new Form10();
            frm10.label5.Text = label7.Text;
            frm10.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form14 frm14 = new Form14();
            frm14.label1.Text = label7.Text;
            frm14.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form11 frm11 = new Form11();
            frm11.Show();
            this.Hide();

        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt16(label7.Text)==1|| Convert.ToInt16(label7.Text) == 2)
            {
                button10.Visible = true;
                button10.Enabled = true;
            }
            else
            {
                button10.Visible = false;
                button10.Enabled = false;
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                button7.Focus();
            }
            if (e.KeyCode == Keys.Right)
            {
                button2.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                button4.Focus();
            }
        }
    }
}
