using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.PackageManager.UI;
using UnityEditor.PackageManager;
using System;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using UnityEngine;
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
            var newModel = await PackageData.FetchPackagesData();
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

    public class QuickEyeHubController : IPackageManagerExtension
    {
        private QuickEyeHubView _view;

        private PackageLinks _model = new PackageLinks();

        [InitializeOnLoadMethod]
        private static void RegisterExtension()
        {
            PackageManagerExtensions.RegisterExtension(new QuickEyeHubController());
        }

        public VisualElement CreateExtensionUI()
        {
            return _view = new QuickEyeHubView(_model);
        }

        public void OnPackageSelectionChange(UnityEditor.PackageManager.PackageInfo packageInfo)
        {
            var thisPackageIsSelected = packageInfo.name == PackageData.packageUniqueName;

            if (thisPackageIsSelected)
            {
                FetchModel();
            }

            _view.style.display = thisPackageIsSelected ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public async void FetchModel(Action onEnd = null)
        {
            var newModel = await PackageData.FetchPackagesData();
            _model.packages = newModel.packages;
            _view.Refresh();
            onEnd?.Invoke();
        }

        public void OnPackageAddedOrUpdated(UnityEditor.PackageManager.PackageInfo packageInfo)
        { }
        public void OnPackageRemoved(UnityEditor.PackageManager.PackageInfo packageInfo)
        { }
    }
}
