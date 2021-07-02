using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace DELTation.Bootstrap.Editor
{
    [InitializeOnLoad]
    public static class BootstrapLoadMenu
    {
        private const int BootstrapSceneIndex = 0;
        private const string RequestedKey = "Bootstrap.Editor.LoadRequested";
        private const string MenuPath = "Bootstrap/Run";

        private static class ToolbarStyles
        {
            public static readonly GUIStyle CommandButtonStyle;

            static ToolbarStyles() =>
                CommandButtonStyle = new GUIStyle("Command")
                {
                    fontSize = 12,
                    alignment = TextAnchor.MiddleCenter,
                    imagePosition = ImagePosition.ImageAbove,
                    fontStyle = FontStyle.Bold,
                };
        }

        static BootstrapLoadMenu()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
            EditorApplication.update += () =>
            {
                if (EditorApplication.isPlaying)
                {
                    ToolbarExtender.LeftToolbarGUI.Remove(OnToolbarGUI);
                }
                else
                {
                    ToolbarExtender.LeftToolbarGUI.Remove(OnToolbarGUI);
                    ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
                }
            };
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("B▶", "Start from Bootstrap"), ToolbarStyles.CommandButtonStyle))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                IsRequested = true;
                EditorApplication.ExecuteMenuItem("Edit/Play");
            }

            GUILayout.Space(15);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadDefaultScene()
        {
            if (!IsRequested) return;

            IsRequested = false;
            Debug.LogWarning(
                "Bootstrap Scene was not initially loaded (this may cause game scene awaking two times). Loading..."
            );
            EditorSceneManager.LoadScene(BootstrapSceneIndex);
        }

        private static bool IsRequested
        {
            get => PlayerPrefs.GetInt(RequestedKey, 0) != 0;
            set
            {
                PlayerPrefs.SetInt(RequestedKey, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
    }
}