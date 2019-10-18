using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace QuickEye.PackageHub
{
    public static class PackageData
    {
        public const string packageListJsonGistUrl = "https://gist.githubusercontent.com/ErnSur/6ce729828c7f6304a10b605addbb3a06/raw/2544c78e4373b5d02121254e65e455bcefe91be3/quickeyehub-manifest.json";

        public const string packageUniqueName = "com.quickeye.quickeyehub";

        public static async Task<PackageLinks> FetchPackagesData()
        {
            using (var client = new WebClient())
            {
                var task = client.DownloadStringTaskAsync(packageListJsonGistUrl);
                await task;

                return JsonUtility.FromJson<PackageLinks>(task.Result);
            }
        }
    }

    [Serializable]
    public class PackageLinks
    {
        public PackageLink[] packages;
    }

    [Serializable]
    public class PackageLink
    {
        public string name;
        public string url;
    }
}
