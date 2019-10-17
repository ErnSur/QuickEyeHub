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
    [CreateAssetMenu(menuName = "Package Hub/Package Library")]
    public class PackageLibrary : ScriptableObject
    {
        public const string _packageListJsonGistUrl = "https://gist.githubusercontent.com/ErnSur/6ce729828c7f6304a10b605addbb3a06/raw/2544c78e4373b5d02121254e65e455bcefe91be3/quickeyehub-manifest.json";

        public PackageLinks packages;

        private CancellationTokenSource cancellationTS;

        private HttpResponseHeaders _responseHeaders;

        [ContextMenu("Fetch")]
        public void FetchData()
        {
            FetchPackagesData(_packageListJsonGistUrl);
        }

        [ContextMenu("Save")]
        public void SaveData()
        {
            var json = JsonUtility.ToJson(packages, true);
            Debug.Log($"o:{packages}, json: {json}");

            packages = JsonUtility.FromJson<PackageLinks>(json);
        }

        private async Task FetchPackagesData(string rawUrl)
        {
            using (var client = new WebClient())
            {
                var task = client.DownloadStringTaskAsync(rawUrl);
                await task;

                packages = JsonUtility.FromJson<PackageLinks>(task.Result);
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
}
