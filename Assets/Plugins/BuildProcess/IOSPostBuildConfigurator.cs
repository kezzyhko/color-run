#if UNITY_EDITOR && UNITY_IOS

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;


namespace Plugins.BuildProcess
{
    public static class IOSPostBuildConfigurator
    {
        
        [PostProcessBuildAttribute(0)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
            
            
            
            if (BuildConfigurator.Config.IOSBuildConfiguration.facebookIsUsed)
            {
                SetConfigsForNiceVibrationsAndFacebook(pathToBuiltProject);
                InstallPod(pathToBuiltProject);
            }
            
        }

        private static void SetConfigsForNiceVibrationsAndFacebook(string path)
        {
            var projPath = PBXProject.GetPBXProjectPath(path);
            var proj = new PBXProject();
            proj.ReadFromFile(projPath);
            
            var targetGuid = proj.GetUnityMainTargetGuid();
 
            foreach (var framework in new[] { targetGuid, proj.GetUnityFrameworkTargetGuid() })
            {
                proj.SetBuildProperty(framework, "ENABLE_BITCODE", "NO");
                proj.SetBuildProperty(framework, "EMBEDDED_CONTENT_CONTAINS_SWIFT", "YES");
                proj.SetBuildProperty(framework, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
                proj.SetBuildProperty(framework, "SWIFT_VERSION", "5.0");
            }
            proj.WriteToFile(projPath);
        }

        private static void InstallPod(string path)
        {
            var proc = new System.Diagnostics.Process ();
            proc.StartInfo.FileName = "./Assets/Plugins/BuildProcess/pods.command";
            proc.StartInfo.Arguments = path;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();

        }
        
        
        
        
    }
}

#endif