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
        public const string _packageListJsonGistUrl = "https://gist.githubusercontent.com/rdsubhas/ed77e9547d989dabe061/raw/6d7775eaacd9beba826e0541ba391c0da3933878/gnc-js-api";

        public PackageLinks identifiers;

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
            var json = JsonUtility.ToJson(identifiers, true);
            Debug.Log($"o:{identifiers}, json: {json}");

            identifiers = JsonUtility.FromJson<PackageLinks>(json);
        }

        private async Task FetchPackagesData(string rawUrl)
        {
            
            using (WebClient client = new WebClient())
            {

                var task = client.DownloadStringTaskAsync(rawUrl);
                await task;

                Debug.Log(task.Result);
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
            public string gitUrl;
            public string displayName;
        }
    }
}
