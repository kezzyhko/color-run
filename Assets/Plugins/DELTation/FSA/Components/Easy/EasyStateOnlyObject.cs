using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/State-only Object")]
	public sealed class EasyStateOnlyObject : MonoBehaviour, ITransitionListener<EasyTrigger>
	{
		[SerializeField, EnumToggleButtons] private Mode _mode = Mode.Inactive;

		[SerializeField, Required("Target object is required."), Tooltip("Game Object to activate/deactivate.")]
		private GameObject _targetObject = default;

		[InfoBox("Target object is ACTIVE in these states, INACTIVE in others.", VisibleIf = nameof(IsActive)),
		 InfoBox("Target object is INACTIVE in these states, ACTIVE in others.", VisibleIf = nameof(IsInactive)),
		 SerializeField, Required, ListDrawerSettings(Expanded = true)]
        
		private EasyStateBase[] _states = default;

		public void OnTransition(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState, IState<EasyTrigger> newState)
		{
			var active = ShouldBeActiveIn(newState);
			SetActive(active);
		}

		private bool ShouldBeActiveIn(IState<EasyTrigger> newState)
		{
			foreach (var state in _states)
			{
				if (ReferenceEquals(newState, state)) return _mode == Mode.Active;
			}

			return _mode == Mode.Inactive;
		}

		private void SetActive(bool active)
		{
			if (_targetObject.activeSelf == active) return;
			_targetObject.SetActive(active);
		}

		private bool IsActive => _mode == Mode.Active;
		private bool IsInactive => _mode == Mode.Inactive;

		private enum Mode
		{
			Active,
			Inactive
		}
	}
}