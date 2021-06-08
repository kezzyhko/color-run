using UnityEngine;

namespace DELTation.Bootstrap.Tests.Runtime.Mocks
{
    public class Dependent : MonoBehaviour
    {
        public BootstrapDependency BootstrapDependency { get; private set; }

        public GameDependency GameDependency { get; private set; }

        public void Construct(BootstrapDependency bootstrapDependency, GameDependency gameDependency)
        {
            BootstrapDependency = bootstrapDependency;
            GameDependency = gameDependency;
        }
    }
}