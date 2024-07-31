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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.label7.Text = label1.Text;
            frm2.Show();
            this.Hide();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
         
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisitLink();
        }
        private void VisitLink()
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://taayuu.blogspot.com");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void VisitLink2()
        {
            linkLabel2.LinkVisited = true;
            System.Diagnostics.Process.Start("mailto:yusuftarik@msn.com");
        }
        private void VisitLink4()
        {
            linkLabel4.LinkVisited = true;
            System.Diagnostics.Process.Start("mailto:konya@iyc.org.tr");
        }
        private void VisitLink3()
        {
            linkLabel3.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.iyckonya.org/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisitLink2();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisitLink3();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisitLink4();
        }
    }
}
