using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/FSA"), DisallowMultipleComponent]
	public class EasyFsa : FsaComponent<EasyTrigger, EasyStateBase>
	{
		[Tooltip("If set, FSA will run on start (if possible)."), SerializeField]
        
		private bool _runOnStart = true;

		private void Start()
		{
			if (!_runOnStart) return;

			if (InitialState == null)
				Debug.LogError("Initial state is not set, cannot run now.", this);
			else
				Run();
		}
	}
}