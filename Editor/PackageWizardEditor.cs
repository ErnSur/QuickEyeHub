using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace QuickEye.PackageHub
{
    [CustomEditor(typeof(PackageWizard))]
    public class PackageWizardEditor : Editor
    {
        private PropertyField _identifier,_displayName;

        private PackageWizard packageWizard;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            _identifier = new PropertyField(serializedObject.FindProperty(nameof(packageWizard.identifier)));

            _displayName = new PropertyField(serializedObject.FindProperty("_displayName"));
            _displayName.SetEnabled(false);

            root.Add(_identifier);
            root.Add(_displayName);
            return root;
        }
    }
}
