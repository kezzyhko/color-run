using System.Collections;
using DELTation.Bootstrap.Tests.Runtime.Mocks;
using DELTation.DIFramework;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace DELTation.Bootstrap.Tests.Runtime
{
    [TestFixture]
    public class BootstrapTests
    {
        private const string BootstrapSceneName = "0_Bootstrap";
        private const string GameSceneName = "1_Game";

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene(BootstrapSceneName);
            yield return new WaitForSceneLoaded(BootstrapSceneName);
        }

        [UnityTest]
        public IEnumerator BootstrapScene_OnStartAndAfterSomeTime_LoadsGameScene()
        {
            yield return WaitForSomeTime;

            Assert.That(SceneManager.GetActiveScene().name == GameSceneName);
        }

        private static object WaitForSomeTime => new WaitForSeconds(0.25f);
    }
}