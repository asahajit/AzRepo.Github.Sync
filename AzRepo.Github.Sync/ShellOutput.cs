using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AzRepo.Github.Sync
{
    public partial class ShellOutput : Form
    {
        public ShellOutput()
        {
            InitializeComponent();
            PowerShellHelper ps = new PowerShellHelper();
            //textBox1.Text = await ps.RunPowerShellCommand("get-process");
            richTextBox1.Text = "get-process";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PowerShellHelper ps = new PowerShellHelper();
            textBox1.Text = ps.RunPowerShellCommand(richTextBox1.Text);
            if (richTextBox1.Text=="Test")
            {

            }
        }
    }
}
