using System;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace QuickEye.PackageHub.New
{
    public class PackageHubExtension : IPackageManagerExtension
    {
        private PackageLinks _packageLinks = new PackageLinks();
        private PackageHubView _view;

        [InitializeOnLoadMethod]
        private static void RegisterExtension() =>
            PackageManagerExtensions.RegisterExtension(new PackageHubExtension());

        public VisualElement CreateExtensionUI()
        {
            _view = new PackageHubView(_packageLinks);
            _view.PackageItemClickEvent += (p) => PackageWizard.Install(p.url);

            return _view;
        }

        public void OnPackageSelectionChange(PackageInfo packageInfo)
        {
            var thisPackageIsSelected = packageInfo?.name == PackageData.packageUniqueName;

            if (thisPackageIsSelected)
            {
                FetchModel();
            }

            if (_view != null)
            {
                _view.style.display = thisPackageIsSelected ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        public async void FetchModel(Action onEnd = null)
        {
            var newModel = await PackageData.FetchData();
            _packageLinks.packages = newModel.packages;
            _view?.Refresh();
            onEnd?.Invoke();
        }

        public void OnPackageAddedOrUpdated(PackageInfo packageInfo) { }
        public void OnPackageRemoved(PackageInfo packageInfo) { }
    }
}
