using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickEye.PackageHub
{
    public class QuickEyeHubModel
    {
        public List<Package> packages = new List<Package>();

        public class Package : PackageLink
        {
            public bool downloaded;

            public Package(PackageLink l)
            {
                name = l.name;
                url = l.url;
            }
        }

        public async void Fetch(Action onEnd = null)
        {
            var newModel = await PackageData.FetchData();
            packages.Clear();
            packages.AddRange(newModel.packages.Select(l => new Package(l)));
            await FetchStatus();

            onEnd?.Invoke();
        }

        public async Task FetchStatus()
        {
            //var listRequest = Client.List();

            //while (listRequest.Status == StatusCode.InProgress) { }
            //var packageNames = listRequest.Result.Select(p => p.name);
            //packages.Select(p=> p.)
            //foreach (var item in )
            //{

            //} 
        }
    }
}
