using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.Events
{
	public abstract class GlobalEventListener : MonoBehaviour
	{
		[SerializeField, Required, AssetSelector]
		private GlobalEvent _globalEvent = default;

		protected void OnEnable()
		{
			_globalEvent.AddListener(_onEvent);
			OnEnabled();
		}

		protected virtual void OnEnabled() { }

		protected void OnDisable()
		{
			_globalEvent.RemoveListener(_onEvent);
			OnDisabled();
		}

		protected virtual void OnDisabled() { }

		protected void Awake()
		{
			_onEvent = OnEvent;
			OnAwaken();
		}

		protected virtual void OnAwaken() { }

		protected abstract void OnEvent();

		private GlobalEventHandler _onEvent;
	}
}