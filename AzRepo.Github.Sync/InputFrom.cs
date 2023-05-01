using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzRepo.Github.Sync
{
    public partial class InputFrom : Form
    {
        public InputFrom()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PowershellScriptName = System.Configuration.ConfigurationManager.AppSettings["PowershellScriptName"];
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(startupPath).Parent.Parent.FullName;
            string PsFilePath = new DirectoryInfo(projectDirectory).GetFiles().Where(n => n.Name.Equals(PowershellScriptName)).SingleOrDefault().FullName;
            PowerShellHelper ps = new PowerShellHelper();
            RepoConfig repoConfig= new RepoConfig();
            ps.RunCustomPowerShellScriptWithSecurity(PsFilePath, repoConfig.GetRepoConfigList());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InputFrom input=new InputFrom();
            input.Close();
            ShellOutput output=new ShellOutput();
            output.ShowDialog();
            
        }
    }
}
