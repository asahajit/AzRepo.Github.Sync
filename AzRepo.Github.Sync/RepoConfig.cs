using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRepo.Github.Sync
{
    public class RepoConfig
    {

        public List<RepoConfig>  GetRepoConfigList() {
            string RepoConfigFileName = System.Configuration.ConfigurationManager.AppSettings["RepoConfigFileName"];
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(startupPath).Parent.Parent.FullName;
            string RepoConfigPath = new DirectoryInfo(projectDirectory).GetFiles().Where(n => n.Name.Equals(RepoConfigFileName)).SingleOrDefault().FullName;
            string configjson = File.ReadAllText(RepoConfigPath);
            List <RepoConfig> RepoConfigList = JsonConvert.DeserializeObject<List<RepoConfig>>(configjson);
            RepoConfigList.ForEach(n => n.GitHubCloneURL = n.GitHubCloneURL.Replace("https://", string.Empty));
            RepoConfigList.ForEach(n => n.ADOCloneURL = n.ADOCloneURL.Replace("https://", string.Empty));
            RepoConfigList.ForEach(n => n.ADOCloneURL = n.ADOCloneURL.Split('@').Last());
            return RepoConfigList;

        }

        public string GitHubRepoName { get; set; }
        public string ADOCloneURL { get; set; }
        public string GitHubCloneURL { get; set; }
        public string LastSynced { get; set; }
        public bool isSyncEnabled { get; set; }
        public string IgnoreFiles { get; set; }
    }
}
