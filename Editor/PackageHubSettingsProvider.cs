using System.Collections.Generic;
using UnityEditor;

namespace QuickEye.PackageHub
{
    public class PackageHubSettingsProvider : SettingsProvider
    {
        public PackageHubSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
            
        }

        //[SettingsProvider]
        //public static PackageHubSettingsProvider GetInstance()
        //{
        //    return new PackageHubSettingsProvider();
        //}
    }
}
