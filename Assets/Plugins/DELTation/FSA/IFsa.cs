using System;

namespace DELTation.FSA
{
	public interface IFsa<T> where T : IEquatable<T>
	{
		IState<T> InitialState { get; set; }
		IState<T> CurrentState { get; }
		bool IsRunning { get; }
		bool IsInTerminalState { get; }

		void Run();
		void Stop();
		void Trigger(T trigger);

		event EventHandler Ran;
		event EventHandler Stopped;
		event EventHandler<T> TriggerReceived;
		event EventHandler<StateChangeArgs<T>> StateChanged;
	}
}