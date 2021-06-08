using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/Behaviour Updater"), RequireComponent(typeof(EasyFsa)),
	 DisallowMultipleComponent]
	public sealed class EasyFsaBehaviourUpdater : MonoBehaviour
	{
		[Tooltip("Invoke Update callback.")] public bool UpdateEnabled = true;

		[Tooltip("Invoke FixedUpdate callback.")]
		public bool FixedUpdateEnabled = true;

		[Tooltip("Invoke OnStateChanged callback.")]
		public bool StateChangeEnabled = true;

		private EasyFsa _fsa;
		private IStateBehaviour<EasyTrigger>[] _behaviours;
		private ITransitionListener<EasyTrigger>[] _stateChangeListeners;

		private EventHandler<StateChangeArgs<EasyTrigger>> _onStateChanged;

		private void Awake()
		{
			_fsa = GetComponent<EasyFsa>();
			_behaviours = _fsa.GetComponentsInChildren<IStateBehaviour<EasyTrigger>>();
			_stateChangeListeners = _fsa.GetComponentsInChildren<ITransitionListener<EasyTrigger>>();

			_onStateChanged = (sender, args) =>
			{
				if (!StateChangeEnabled) return;

				foreach (var listener in _stateChangeListeners)
				{
					listener.OnTransition(_fsa, args.OldState, args.NewState);
				}
			};
		}

		private void Update()
		{
			if (!_fsa.IsRunning) return;
			if (!UpdateEnabled) return;

			var deltaTime = Time.deltaTime;

			foreach (var behaviour in _behaviours)
			{
				behaviour.TryInvokeUpdate(deltaTime, _fsa);
			}
		}

		private void FixedUpdate()
		{
			if (!_fsa.IsRunning) return;
			if (!FixedUpdateEnabled) return;

			var deltaTime = Time.fixedDeltaTime;

			foreach (var behaviour in _behaviours)
			{
				behaviour.TryInvokeFixedUpdate(deltaTime, _fsa);
			}
		}

		private void OnEnable()
		{
			_fsa.StateChanged += _onStateChanged;
		}

		private void OnDisable()
		{
			_fsa.StateChanged -= _onStateChanged;
		}

		[ShowInInspector]
		private IEnumerable<IStateBehaviour<EasyTrigger>> UpdatedBehaviours =>
			_behaviours ?? GetComponentsInChildren<IStateBehaviour<EasyTrigger>>();

		[ShowInInspector]
		private IEnumerable<ITransitionListener<EasyTrigger>> UpdatedStateChangeListeners =>
			_stateChangeListeners ?? GetComponentsInChildren<ITransitionListener<EasyTrigger>>();
	}
}