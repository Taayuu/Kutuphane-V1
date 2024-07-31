using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IYC_KUTUPHANE
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", "/k" + label5.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ExeDosyaYolu = Application.StartupPath.ToString();
            Process.Start(ExeDosyaYolu + "\\sqlLocalDB.msi");
        }
    }
}
