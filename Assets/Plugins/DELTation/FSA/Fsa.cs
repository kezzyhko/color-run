using System;
using JetBrains.Annotations;

namespace DELTation.FSA
{
	public class Fsa<T> : IFsa<T> where T : IEquatable<T>
	{
		public Fsa([NotNull] IState<T> initialState) =>
			InitialState = initialState ?? throw new ArgumentNullException(nameof(initialState));

		public Fsa() { }

		private IState<T> _initialState;

		public bool IsRunning { get; private set; }
		public bool IsInTerminalState => CurrentState != null && CurrentState.IsTerminal;

		public IState<T> InitialState
		{
			get => _initialState;
			set
			{
				if (IsRunning)
					throw new InvalidOperationException("Initial state cannot be changed when FSA is running.");
				_initialState = value;
			}
		}

		public IState<T> CurrentState { get; private set; }

		public void Run()
		{
			if (IsRunning) return;

			IsRunning = true;
			CurrentState = InitialState ?? throw new InvalidOperationException("Initial state is not set.");

			OnRan();
			OnStateChanged(null, InitialState);
		}

		public void Stop()
		{
			if (!IsRunning) return;

			IsRunning = false;

			OnStopped();
		}

		public void Trigger(T trigger)
		{
			if (!IsRunning) return;

			if (CurrentState.Trigger(this, trigger, out var newState))
			{
				var oldState = CurrentState;
				CurrentState = newState;
				OnStateChanged(oldState, newState);
			}

			OnTriggerReceived(trigger);
		}

		public event EventHandler Ran;
		public event EventHandler Stopped;
		public event EventHandler<T> TriggerReceived;
		public event EventHandler<StateChangeArgs<T>> StateChanged;

		protected virtual void OnRan()
		{
			Ran?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnStopped()
		{
			Stopped?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnTriggerReceived(T trigger)
		{
			TriggerReceived?.Invoke(this, trigger);
		}

		protected virtual void OnStateChanged(IState<T> oldState, IState<T> newState)
		{
			var args = new StateChangeArgs<T> {OldState = oldState, NewState = newState};
			StateChanged?.Invoke(this, args);
		}
	}
}