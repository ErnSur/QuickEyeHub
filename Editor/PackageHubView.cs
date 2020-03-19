using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace QuickEye.PackageHub.New
{
    public class PackageHubView : VisualElement
    {
        public const string PackageItemUXMLPath = "QuickEyeHub/PackageItem";
        public const string PackageHubUSSPath = "QuickEyeHub/PackageHub";
        public const string packageListViewName = "package-list";
        public const string PackageHubViewName = "package-hub";

        public event Action<PackageLink> PackageItemClickEvent;

        private ListView _packageList;
        private VisualTreeAsset _packageItemPrototype;

        private PackageLinks _model;

        public PackageHubView(PackageLinks model)
        {
            name = PackageHubViewName;
            _model = model;

            _packageItemPrototype = Resources.Load<VisualTreeAsset>(PackageItemUXMLPath);
            styleSheets.Add(Resources.Load<StyleSheet>(PackageHubUSSPath));

            style.flexGrow = 1;
            AddPackageList();
        }

        public void Refresh()
        {
            _packageList.itemsSource = _model.packages;
            _packageList.Refresh();
        }

        private void AddPackageList()
        {
            _packageList = new ListView(_model.packages, 20, MakePackageItem, BindPackageItem);
            _packageList.name = packageListViewName;

            void BindPackageItem(VisualElement e, int i)
            {
                var button = e.Q<Button>();
                button.text = _model.packages[i].name;
                button.clickable = new Clickable(() => PackageItemClickEvent?.Invoke(_model.packages[i]));
            }
            Add(_packageList);
        }

        private VisualElement MakePackageItem()
        {
            var item = _packageItemPrototype.CloneTree();

            var button = item.Q<Button>();
            button.RemoveFromClassList("unity-button");
            button.RemoveFromClassList("unity-text-element");
            return item;
        }
    }
}
