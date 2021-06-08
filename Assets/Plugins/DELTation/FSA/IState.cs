using System;

namespace DELTation.FSA
{
	public interface IState<T> where T : IEquatable<T>
	{
		bool IsTerminal { get; }
		bool Trigger(IFsa<T> fsa, T trigger, out IState<T> newState);
		event EventHandler<TriggerArgs<T>> TriggerReceived;
	}
}