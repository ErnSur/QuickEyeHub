using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.PackageManager.UI;
using System;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace QuickEye.PackageHub
{
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

        public void OnPackageSelectionChange(PackageInfo packageInfo)
        {
            var thisPackageIsSelected = packageInfo.name == PackageData.packageUniqueName;

            if (thisPackageIsSelected)
            {
                FetchModel();
            }

            if(_view != null)
            {
                _view.style.display = thisPackageIsSelected ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        public async void FetchModel(Action onEnd = null)
        {
            var newModel = await PackageData.FetchData();
            _model.packages = newModel.packages;
            _view?.Refresh();
            onEnd?.Invoke();
        }

        public void OnPackageAddedOrUpdated(PackageInfo packageInfo) { }
        public void OnPackageRemoved(PackageInfo packageInfo) { }
    }
}
