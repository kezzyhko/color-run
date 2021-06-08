using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	public abstract class EasyTransitionListener : MonoBehaviour, ITransitionListener<EasyTrigger>
	{
		public bool AcceptAnyOld = true;

		[SerializeField, Required("State is not attached."), ListDrawerSettings(Expanded = true),
		 HideIf(nameof(AcceptAnyOld))]
		private List<EasyStateBase> _acceptedOldStates = new List<EasyStateBase>();

		public bool AcceptAnyNew = true;

		[SerializeField, Required("State is not attached."), ListDrawerSettings(Expanded = true),
		 HideIf(nameof(AcceptAnyNew))]
		private List<EasyStateBase> _acceptedNewStates = new List<EasyStateBase>();

		public void OnTransition(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState, IState<EasyTrigger> newState)
		{
			if (AcceptsOld(oldState) && AcceptsNew(newState))
				TransitionAction(fsa, oldState, newState);
		}

		protected abstract void TransitionAction(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState,
			IState<EasyTrigger> newState);

		protected virtual bool AcceptsOld(IState<EasyTrigger> oldState)
		{
			if (AcceptAnyOld) return true;

			foreach (var acceptedState in _acceptedOldStates)
			{
				if (acceptedState.Equals(oldState)) return true;
			}

			return false;
		}

		protected virtual bool AcceptsNew(IState<EasyTrigger> newState)
		{
			if (AcceptAnyNew) return true;

			foreach (var acceptedState in _acceptedNewStates)
			{
				if (acceptedState.Equals(newState)) return true;
			}

			return false;
		}

		public void AddAcceptedOldState(EasyStateBase oldState)
		{
			_acceptedOldStates.Add(oldState);
		}

		public void RemoveAcceptedOldState(EasyStateBase oldState)
		{
			_acceptedOldStates.Remove(oldState);
		}

		public void AddAcceptedNewState(EasyStateBase newState)
		{
			_acceptedNewStates.Add(newState);
		}

		public void RemoveAcceptedNewState(EasyStateBase newState)
		{
			_acceptedNewStates.Remove(newState);
		}
	}

	public abstract class EasyTransitionListener<TOld, TNew> : EasyTransitionListener
		where TOld : IState<EasyTrigger>
		where TNew : IState<EasyTrigger>
	{
		protected sealed override void TransitionAction(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState,
			IState<EasyTrigger> newState)
		{
			if (oldState is TOld typedOldState && newState is TNew typedNewState)
				TransitionAction(fsa, typedOldState, typedNewState);
		}

		protected abstract void TransitionAction(IFsa<EasyTrigger> fsa, TOld oldState, TNew newState);
	}
}