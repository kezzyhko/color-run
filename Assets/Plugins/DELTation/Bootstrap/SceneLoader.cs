using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DELTation.Bootstrap
{
    public sealed class SceneLoader : MonoBehaviour
    {
        [SerializeField] [Min(0)] private int _sceneBuildIndex = 1;
        [SerializeField] private LoadingMode _loadingMode = LoadingMode.OnStart;
        [SerializeField] [ShowIf(nameof(AfterSeveralFrames))] [Min(1)]
        private int _frames = 1;
        [SerializeField] [ShowIf(nameof(AfterSomeTime))] [Min(0f)]
        private float _time = 0f;

        private bool AfterSeveralFrames => _loadingMode == LoadingMode.AfterSeveralFrames;
        private bool AfterSomeTime => _loadingMode == LoadingMode.AfterSomeTime;

        private IEnumerator Start()
        {
            switch (_loadingMode)
            {
                case LoadingMode.OnStart:
                    LoadScene();
                    break;

                case LoadingMode.AfterSeveralFrames:

                    for (var i = 0; i < _frames; i++)
                    {
                        yield return null;
                    }

                    LoadScene();

                    break;

                case LoadingMode.AfterSomeTime:
                    yield return new WaitForSecondsRealtime(_time);
                    LoadScene();
                    break;

                default:
                    Debug.LogError("Invalid loading mode.", this);
                    yield break;
            }
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_sceneBuildIndex);
        }

        private enum LoadingMode
        {
            OnStart,
            AfterSeveralFrames,
            AfterSomeTime,
        }
    }
}