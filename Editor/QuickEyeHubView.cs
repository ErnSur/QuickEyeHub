using UnityEngine.UIElements;
using UnityEngine;

namespace QuickEye.PackageHub
{
    public class QuickEyeHubView : VisualElement
    {
        public const string packageListViewName = "package-list";

        private ListView _packageList;
        private VisualTreeAsset _packageItem;

        private PackageLinks _model;


        public QuickEyeHubView(PackageLinks model)
        {
            _model = model;

            _packageItem = Resources.Load<VisualTreeAsset>("QuickEyeHub/PackageItem");
            styleSheets.Add(Resources.Load<StyleSheet>("QuickEyeHub/PackageHub"));

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
                button.clickable = new Clickable(() => PackageWizard.Install(_model.packages[i].url));
            }
            Add(_packageList);
        }

        VisualElement MakePackageItem()
        {
            var item = _packageItem.CloneTree();

            var button = item.Q<Button>();
            button.RemoveFromClassList("unity-button");
            button.RemoveFromClassList("unity-text-element");
            return item;
        }
    }
}
