using System;

namespace DELTation.FSA
{
	public abstract class State<T> : IState<T> where T : IEquatable<T>
	{
		public bool IsTerminal { get; protected set; }

		public bool Trigger(IFsa<T> fsa, T trigger, out IState<T> newState)
		{
			var hasNewState = HandleTrigger(fsa, trigger, out newState);

			OnTriggerReceived(fsa, trigger);

			return hasNewState;
		}

		public event EventHandler<TriggerArgs<T>> TriggerReceived;

		protected abstract bool HandleTrigger(IFsa<T> fsa, T trigger, out IState<T> state);

		protected virtual void OnTriggerReceived(IFsa<T> fsa, T trigger)
		{
			var args = new TriggerArgs<T> {Fsa = fsa, Trigger = trigger};
			TriggerReceived?.Invoke(this, args);
		}
	}
}