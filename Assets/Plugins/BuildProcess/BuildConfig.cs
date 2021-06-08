using System;
using System.IO;
using System.Xml;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.BuildProcess
{
    [CreateAssetMenu(fileName = "Builder Config")]
    public class BuildConfig : ScriptableObject
    {
        public string displayedName = "Game Name";
        public string companyName = "Indie GameDev Club";
        public AndroidBuildConfiguration androidConfiguration;
        public IOSBuildConfiguration IOSBuildConfiguration;


    }

    [Serializable]
    public class AndroidBuildConfiguration
    {
        public bool releaseBuild;
        public string packageName = "com.igdclub.gamename";
        public string version = "0.1";
        public int bundleVersion = 0;

        [FilePath, ShowIf(nameof(releaseBuild)), ValidateInput(nameof(KeyStoreAndPasswordDefined), "Key Store file and password should be defined")]
        public string keyStoreFile;
        [ShowIf(nameof(releaseBuild)), ValidateInput(nameof(KeyStoreAndPasswordDefined), "Key Store file and password should be defined")]
        public string password;

        public bool KeyStoreAndPasswordDefined()
        {
            return !string.IsNullOrEmpty(keyStoreFile) && !string.IsNullOrEmpty(password);
        }
    }

    [Serializable]
    public class IOSBuildConfiguration
    {
        public string packageName = "com.igdclub.gamename";
        public string version = "0.1";
        public string buildVersion = "0";
        public string teamID = "A4X587W55Y";
        [ValidateInput(nameof(CheckAndSetProvision))] public string provisionProfileID;
        public bool facebookIsUsed;
        [ShowIf(nameof(facebookIsUsed))] public bool niceVibrationIsUsed;

        public bool CheckAndSetProvision()
        {
            return !string.IsNullOrEmpty(provisionProfileID);
        }
    }
}
