﻿using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.PackageManager;

namespace QuickEye.PackageHub
{
    [CreateAssetMenu(menuName = "Package Hub/Package Wizard")]
    public class PackageWizard : ScriptableObject
    {
        public string identifier;

        [SerializeField]
        private string _displayName;

        [OnOpenAsset]
        private static bool Install(int instanceID, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as PackageWizard;
            Debug.Log($"Install {obj.name}");
            var request = Client.Add(obj.identifier);
            
            while (request.Status == StatusCode.InProgress)
            {
            }

            var result = request.Result;
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(instanceID), result.name);
            
            obj._displayName = result.displayName;

            return true;
        }
    }
}
