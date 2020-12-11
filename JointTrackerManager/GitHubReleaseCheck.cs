using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO.Compression;

namespace JointTrackerManager
{
    class GitHubReleaseCheck
    {
        private string dir;
        private string latestReleaseURL;

        public GitHubReleaseCheck(string userName, string repositoryName, string dir)
        {
            latestReleaseURL = string.Join("/", "https://api.github.com", "repos", userName, repositoryName, "releases", "latest");
            this.dir = dir;
        }

        public void Start() { Task.Run(Process); }

        private void Process()
        {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var localVersion = Directory.GetDirectories(dir)
                                        .Select(x =>
                                        {
                                            long datetime;
                                            string name = Path.GetFileName(x);
                                            string time = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0];
                                            bool success = long.TryParse(time, out datetime);
                                            return new { success, dir = x, datetime };
                                        })
                                        .Where(x => x.success)
                                        .Prepend(new { success = false, dir = "", datetime = 0L })
                                        .OrderBy(x => x.datetime)
                                        .Last();
            
            // リポジトリ上の最新版を調べる前に、ローカルのバージョンを通知
            if (localVersion.success) OnComplete?.Invoke(false, localVersion.dir);

            Task.Run(() =>
            {
                var req = (HttpWebRequest)HttpWebRequest.Create(latestReleaseURL);
                req.ContentType = "application/json;charset=UTF-8";
                req.UserAgent = "Mozilla/5.0";
                WebResponse res;
                try
                {
                    res = req.GetResponse();
                } catch(Exception e)
                {
                    OnFailed?.Invoke(e.Message);
                    return;
                }
                var serializer = new DataContractJsonSerializer(typeof(GithubRelease));

                // releases/latest に変える
                GithubRelease release = (GithubRelease)serializer.ReadObject(res.GetResponseStream());

                long latestTicks = release.PublishedAt.Ticks;
                if (localVersion.datetime < latestTicks)
                {
                    var dst = dir + "\\" + latestTicks + "_" + release.TagName;
                    var cli = new WebClient();
                    var tempFile = Path.GetTempFileName();
                    cli.DownloadFile(release.Assets[0].DownloadUrl, tempFile);
                    ZipFile.ExtractToDirectory(tempFile, dst);
                    new DirectoryInfo(dst).CreationTime = release.PublishedAt;

                    OnComplete?.Invoke(true, dst);
                }
            });
        }

        public delegate void ReleaseCheckCompleteEventHandler(bool completed, string directory);
        public delegate void ReleaseCheckFailedEventHandler(string directory);

        public event ReleaseCheckCompleteEventHandler OnComplete;
        public event ReleaseCheckFailedEventHandler OnFailed;
    }
}
