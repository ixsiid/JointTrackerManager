using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace JointTrackerManager
{
    [DataContractAttribute]
    class GithubRelease
    {
        [DataMember(Name = "prerelease")]
        public bool PreRelease { get; private set; }
        [DataMember(Name = "tag_name")]
        public string TagName { get; private set; }
        [DataMember(Name = "published_at")]
        private string published_at { get; set; }
        public DateTime PublishedAt { get { return DateTime.Parse(published_at); } }
        [DataMember(Name = "assets")]
        public List<GithubAsset> Assets { get; set; }

        /*
        public string url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public GithubAuthor author { get; set; }
        public string node_id { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public bool draft { get; set; }
        public string created_at { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
        */
    }

    /*
    [DataContractAttribute]
    class GithubAuthor
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptioins_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }
    */

    [DataContractAttribute]
    class GithubAsset
    {
        [DataMember(Name = "browser_download_url")]
        public string DownloadUrl { get; set; }

        /*
        public string url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public GithubAuthor uploader { get; set; }
        public string content_type { get; set; }
        public string state { get; set; }
        public int size { get; set; }
        public int download_count { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        */
    }
}
