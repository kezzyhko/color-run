#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Plugins.BuildProcess
{
    public class BuildConfigurator : IPreprocessBuildWithReport
    {
        public static BuildConfig Config;
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            SetBuildConfigFile();
#if UNITY_IOS
                ConfigureIOSBuild();
#endif

#if UNITY_ANDROID
                ConfigureAndroidBuild();
#endif
        }

        private void ConfigureIOSBuild()
        {
            Debug.Log("Start iOS Build Configuration");

            PlayerSettings.iOS.applicationDisplayName = Config.displayedName;
            PlayerSettings.companyName = Config.companyName;

            PlayerSettings.applicationIdentifier = Config.IOSBuildConfiguration.packageName;
            
            PlayerSettings.bundleVersion = Config.IOSBuildConfiguration.version;
            PlayerSettings.iOS.buildNumber = Config.IOSBuildConfiguration.buildVersion;
            
            PlayerSettings.iOS.appleEnableAutomaticSigning = false;
            PlayerSettings.iOS.appleDeveloperTeamID = Config.IOSBuildConfiguration.teamID;
            PlayerSettings.iOS.iOSManualProvisioningProfileID = Config.IOSBuildConfiguration.provisionProfileID;
            PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Distribution;
            
            PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, 2); // 0 - None, 1 - ARM64, 2 - Universal.

            Debug.Log("iOS Build Successfully Configured");
        }

        private void ConfigureAndroidBuild()
        {
            //TODO: Make Android Build Configuration
        }

        private void SetBuildConfigFile()
        {
            Debug.Log("Looking for Build Config");

            var path = "Assets/Plugins/BuildProcess/Build Config.asset";
            Config = AssetDatabase.LoadAssetAtPath<BuildConfig>(path);
            if (Config == null)
            {
                Debug.LogError("Build Config is not found on path: " + path);
                throw new FileNotFoundException("Build Config is not found on path: " + path);
            }
            Debug.Log("Build Config Successfully Defined");
            
        }
    }
}
#endif