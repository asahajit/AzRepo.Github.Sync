using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AzRepo.Github.Sync
{
    public class PowerShellHelper
    {
        public PowerShellHelper() { }

        public bool RunPowerShellScript(string script_path,List<string> scriptParameters)
        {
            
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;
            ps.AddCommand(script_path);

            foreach (string scriptParameter in scriptParameters)
            {
                ps.AddArgument(scriptParameter);
            }

            Collection<PSObject> psObjects;
            psObjects = pipeline.Invoke();
            return false;
        }
        public string RunPowerShellCommand(string cmd)
        {
            try
            {
                Runspace runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                Pipeline pipeline = runspace.CreatePipeline();
                pipeline.Commands.AddScript(cmd);
                pipeline.Commands.Add("Out-String");
                Collection<PSObject> result = pipeline.Invoke();
                runspace.Close();
                StringBuilder sb = new StringBuilder();
                foreach (PSObject obj in result)
                    sb.Append(obj.ToString());
                return sb.ToString();
            }
            catch (Exception ex)
            {


                return ex.Message;
            }
            
            
        }


        public bool RunCustomPowerShellScriptWithSecurity(string script_path, List<RepoConfig> RepoList)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;
            
            string GitHubSourcePAT = System.Configuration.ConfigurationManager.AppSettings["GitHubSourcePAT"];
            string ADODestinationPAT = System.Configuration.ConfigurationManager.AppSettings["ADODestinationPAT"];

            foreach (RepoConfig repoConfig in RepoList)
            {

                string argument = string.Format(" -GitHubSourcePAT {0} -ADODestinationPAT {1} -AzureRepoName {2} -ADOCloneURL {3} -GitHubCloneURL {4}", 
                    GitHubSourcePAT, ADODestinationPAT, repoConfig.GitHubRepoName, repoConfig.ADOCloneURL, repoConfig.GitHubCloneURL);
                string command = string.Concat(script_path, argument);
                RunPowerShellCommand("Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser");
                RunPowerShellCommand(command);
            }
            
            return false;
        }

        public bool RunCustomPowerShellScriptUI(string script_path,string ADOCloneURL,string GitHubCloneURL,string GitRepoName)
        {
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;

            string GitHubSourcePAT = System.Configuration.ConfigurationManager.AppSettings["GitHubSourcePAT"];
            string ADODestinationPAT = System.Configuration.ConfigurationManager.AppSettings["ADODestinationPAT"];

            string argument = string.Format(" -GitHubSourcePAT {0} -ADODestinationPAT {1} -AzureRepoName {2} -ADOCloneURL {3} -GitHubCloneURL {4}",
                GitHubSourcePAT, ADODestinationPAT, GitRepoName, ADOCloneURL, GitHubCloneURL);
            string command = string.Concat(script_path, argument);
            RunPowerShellCommand("Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser");
            RunPowerShellCommand(command);
            return false;
        }
    }
}
