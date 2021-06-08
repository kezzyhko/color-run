using System;

namespace DELTation.FSA.Logging
{
	public interface IFsaLogger
	{
		void Start();
		void Stop();
	}

	public class FsaLogger<T> : IFsaLogger where T : IEquatable<T>
	{
		private readonly IFsa<T> _fsa;
		private readonly LogAction _logAction;

		private readonly EventHandler _onRan;
		private readonly EventHandler _onStopped;
		private readonly EventHandler<T> _onTriggerReceived;
		private readonly EventHandler<StateChangeArgs<T>> _onStateChanged;

		public FsaLogger(IFsa<T> fsa, LogAction logAction)
		{
			_fsa = fsa;
			_logAction = logAction;

			_onRan = OnRan;
			_onStopped = OnStopped;
			_onTriggerReceived = OnTriggerReceived;
			_onStateChanged = OnStateChanged;
		}

		public void Start()
		{
			_fsa.Ran += _onRan;
			_fsa.Stopped += _onStopped;
			_fsa.TriggerReceived += _onTriggerReceived;
			_fsa.StateChanged += _onStateChanged;
		}

		public void Stop()
		{
			_fsa.Ran -= _onRan;
			_fsa.Stopped -= _onStopped;
			_fsa.TriggerReceived -= _onTriggerReceived;
			_fsa.StateChanged -= _onStateChanged;
		}

		protected virtual void OnRan(object sender, EventArgs args)
		{
			_logAction($"FSA {_fsa} has been ran from state {_fsa.InitialState}.");
		}

		protected virtual void OnStopped(object sender, EventArgs args)
		{
			_logAction($"FSA {_fsa} has been stopped.");
		}

		protected virtual void OnTriggerReceived(object sender, T trigger)
		{
			_logAction($"FSA {_fsa} has received trigger {trigger.ToString()}.");
		}

		protected virtual void OnStateChanged(object sender, StateChangeArgs<T> args)
		{
			_logAction($"State of the FSA {_fsa} has been changed from {args.OldState} to {args.NewState}.");
		}
	}
}