using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/Debugger")]
	public class EasyFsaDebugger : MonoBehaviour
	{
		[SerializeField, Required("FSA is not attached."), LabelText("FSA")]
		private EasyFsa _fsa = default;

		[Button, HideIf(nameof(ShouldHideTriggerButton))]
		private void FireTrigger()
		{
			if (_fsa == null)
			{
				Debug.LogError("FSA is not attached.", this);
				return;
			}

			if (_triggerToFire == null)
			{
				Debug.LogError("Trigger is not set.", this);
				return;
			}

			if (!_fsa.IsRunning)
			{
				Debug.LogWarning("FSA is not running.", this);
				return;
			}

			Debug.Log($"Firing trigger {_triggerToFire.name}...", this);
			_fsa.Trigger(_triggerToFire);
		}

		[ShowInInspector, LabelText("Trigger"), AssetList, HideIf(nameof(ShouldHideTriggerField))]
		private EasyTrigger _triggerToFire = default;

		private bool ShouldHideTriggerButton => _fsa == null || _triggerToFire == null;
		private bool ShouldHideTriggerField => _fsa == null;
	}
}