using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace QuickEye.PackageHub
{
    [CustomEditor(typeof(PackageLibrary))]
    public class PackageLibraryEditor : Editor
    {
        private PropertyField _identifier, _displayName;

        private ListView _packageList;
        private ScrollView _scrollView;

        [SerializeField]
        private VisualTreeAsset _packageItem;

        public override VisualElement CreateInspectorGUI()
        //public VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            CreatePackageList();

            root.Add(_packageList);
            root.Add(_displayName);

            root.style.flexGrow = 1;

            return root;
        }

        private void CreatePackageList()
        {
            var packages = (target as PackageLibrary).packages.packages;

            _packageList = new ListView(packages, 20, MakePackageItem, BindPackageItem);
            _packageList.style.flexGrow = 1;
            _packageList.style.height = 200;
            //_packageList.style.backgroundColor = Color.green;

            void BindPackageItem(VisualElement e, int i)
            {
                var button = e.Q<Button>();
                button.text = packages[i].name;
                button.clickable = new Clickable(() => PackageWizard.Install(packages[i].url));
            }
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
