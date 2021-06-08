using System;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	public abstract class EasyStateBase : MonoBehaviour, IState<EasyTrigger>
	{
		[SerializeField] private bool _isTerminal = default;

		public virtual bool IsTerminal => _isTerminal;

		public bool Trigger(IFsa<EasyTrigger> fsa, EasyTrigger trigger, out IState<EasyTrigger> newState)
		{
			var triggered = TriggersTransition(fsa, trigger, out newState);
			OnTriggerReceived(fsa, trigger);

			return triggered;
		}

		protected virtual bool TriggersTransition(IFsa<EasyTrigger> fsa, EasyTrigger trigger,
			out IState<EasyTrigger> newState)
		{
			newState = default;
			return false;
		}

		public event EventHandler<TriggerArgs<EasyTrigger>> TriggerReceived;

		protected virtual void OnTriggerReceived(IFsa<EasyTrigger> fsa, EasyTrigger trigger)
		{
			var args = new TriggerArgs<EasyTrigger> {Fsa = fsa, Trigger = trigger};
			TriggerReceived?.Invoke(this, args);
		}
	}
}