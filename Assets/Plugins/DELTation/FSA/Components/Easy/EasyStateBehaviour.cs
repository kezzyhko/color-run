using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	public abstract class EasyStateBehaviour : MonoBehaviour, IStateBehaviour<EasyTrigger>
	{
		[SerializeField, Tooltip("Behaviour is being updating if and only if it is enabled.")]
		private bool _enabled = true;

		[SerializeField, Required("State is not attached."), ListDrawerSettings(Expanded = true),
		 Tooltip("States, during which this behaviour is being updated.")]
		private List<EasyStateBase> _acceptedStates = new List<EasyStateBase>();

		protected void Awake()
		{
			OnAwaken();
		}

		protected virtual void OnAwaken() { }

		public bool Enabled
		{
			get => _enabled;
			set => _enabled = value;
		}

		public void TryInvokeUpdate(float deltaTime, IFsa<EasyTrigger> fsa)
		{
			if (!Enabled) return;
			if (!Accepts(fsa.CurrentState)) return;

			OnUpdate(deltaTime, fsa);
		}

		protected virtual void OnUpdate(float deltaTime, IFsa<EasyTrigger> fsa) { }

		public void TryInvokeFixedUpdate(float deltaTime, IFsa<EasyTrigger> fsa)
		{
			if (!Enabled) return;
			if (!Accepts(fsa.CurrentState)) return;

			OnFixedUpdate(deltaTime, fsa);
		}

		protected virtual void OnFixedUpdate(float deltaTime, IFsa<EasyTrigger> fsa) { }

		protected virtual bool Accepts(IState<EasyTrigger> state)
		{
			foreach (var acceptedState in _acceptedStates)
			{
				if (acceptedState.Equals(state))
					return true;
			}

			return false;
		}

		public void AddAcceptedState(EasyStateBase state)
		{
			_acceptedStates.Add(state);
		}

		public void RemoveAcceptedState(EasyStateBase state)
		{
			_acceptedStates.Remove(state);
		}
	}
}