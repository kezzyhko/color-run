using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.Events.Generic
{
	public abstract class GlobalEventListener<TArgs, TEvent> : MonoBehaviour where TEvent : GlobalEvent<TArgs>
	{
		[SerializeField, Required, AssetSelector]
		private TEvent _globalEvent = default;

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

		protected abstract void OnEvent(TArgs args);

		private GlobalEventHandler<TArgs> _onEvent;
	}
}