using UnityEngine;
using UnityEditor.PackageManager;

namespace QuickEye.PackageHub
{
    public static class PackageWizard
    {
        public static void Install(string identifier)
        {
            Debug.Log($"Install Started, uri: {identifier}");

            var request = Client.Add(identifier);

            while (request.Status == StatusCode.InProgress) { }

            if(request.Status == StatusCode.Failure)
            {
                Debug.Log($"{request.Error.errorCode}: {request.Error.message}");
            }
        }
    }
}
