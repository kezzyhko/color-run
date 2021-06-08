using UnityEngine;
using UnityEngine.SceneManagement;

namespace DELTation.Bootstrap.Tests.Runtime
{
    public sealed class WaitForSceneLoaded : CustomYieldInstruction
    {
        public WaitForSceneLoaded(string sceneName, float newTimeout = 10)
        {
            _sceneName = sceneName;
            _timeout = newTimeout;
            _startTime = Time.realtimeSinceStartup;
        }

        public override bool keepWaiting
        {
            get
            {
                var scene = SceneManager.GetSceneByName(_sceneName);
                var sceneLoaded = scene.IsValid() && scene.isLoaded;

                if (Time.realtimeSinceStartup - _startTime >= _timeout) TimedOut = true;

                return !sceneLoaded && !TimedOut;
            }
        }

        private bool TimedOut { get; set; }

        private readonly string _sceneName;
        private readonly float _timeout;
        private readonly float _startTime;
    }
}