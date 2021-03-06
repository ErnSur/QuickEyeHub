﻿using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace QuickEye.PackageHub
{
    public static class PackageData
    {
        public const string packageListJsonGistUrl = "https://gist.githubusercontent.com/ErnSur/6ce729828c7f6304a10b605addbb3a06/raw/quickeyehub-manifest.json";

        public const string packageUniqueName = "com.quickeye.quickeyehub";

        public static async Task<PackageLinks> FetchData()
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
