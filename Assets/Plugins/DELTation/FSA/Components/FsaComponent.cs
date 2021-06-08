using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components
{
	public abstract class FsaComponent<TTrigger, TInitialState> : MonoBehaviour, IFsa<TTrigger>
		where TTrigger : IEquatable<TTrigger>
		where TInitialState : IState<TTrigger>
	{
		[Tooltip("State, which the FSA starts from."), SerializeField, Required]
        
		private TInitialState _initialState = default;

		internal TInitialState SerializedInitialState => _initialState;

		public IState<TTrigger> InitialState
		{
			get => _fsa?.InitialState;
			set
			{
				if (_fsa == null) return;

				_fsa.InitialState = value;
			}
		}

		public IState<TTrigger> CurrentState => _fsa?.CurrentState;
		public bool IsRunning => _fsa?.IsRunning ?? false;
		public bool IsInTerminalState => _fsa?.IsInTerminalState ?? false;

		public void Run()
		{
			if (_fsa == null)
			{
				Debug.LogError("FSA was not initialized yet.", this);
				return;
			}

			_fsa.Run();
		}

		public void Stop()
		{
			if (_fsa == null)
			{
				Debug.LogError("FSA was not initialized yet.", this);
				return;
			}

			_fsa.Stop();
		}

		public void Trigger(TTrigger trigger)
		{
			if (_fsa == null)
			{
				Debug.LogError("FSA was not initialized yet.", this);
				return;
			}

			_fsa.Trigger(trigger);
		}

		public event EventHandler Ran;
		public event EventHandler Stopped;
		public event EventHandler<TTrigger> TriggerReceived;
		public event EventHandler<StateChangeArgs<TTrigger>> StateChanged;

		[CanBeNull] private IFsa<TTrigger> _fsa;

		private EventHandler _onRan;
		private EventHandler _onStopped;
		private EventHandler<TTrigger> _onTriggerReceived;
		private EventHandler<StateChangeArgs<TTrigger>> _onStateChanged;

		protected void Awake()
		{
			_fsa = _initialState == null ? new Fsa<TTrigger>() : new Fsa<TTrigger>(_initialState);

			_onRan = (sender, state) => OnRan();
			_onStopped = (sender, args) => OnStopped();
			_onTriggerReceived = (sender, args) => OnTriggerReceived(args);
			_onStateChanged = (sender, args) => OnStateChanged(args);

			OnAwaken();
		}

		protected virtual void OnAwaken() { }

		protected void OnEnable()
		{
			if (_fsa != null)
			{
				_fsa.Ran += _onRan;
				_fsa.Stopped += _onStopped;
				_fsa.TriggerReceived += _onTriggerReceived;
				_fsa.StateChanged += _onStateChanged;
			}

			OnEnabled();
		}

		protected virtual void OnEnabled() { }

		protected void OnDisable()
		{
			if (_fsa != null)
			{
				_fsa.Ran -= _onRan;
				_fsa.Stopped -= _onStopped;
				_fsa.TriggerReceived -= _onTriggerReceived;
				_fsa.StateChanged -= _onStateChanged;
			}

			OnDisabled();
		}

		protected virtual void OnDisabled() { }

		protected virtual void OnRan()
		{
			Ran?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnStopped()
		{
			Stopped?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnTriggerReceived(TTrigger trigger)
		{
			TriggerReceived?.Invoke(this, trigger);
		}

		protected virtual void OnStateChanged(StateChangeArgs<TTrigger> args)
		{
			StateChanged?.Invoke(this, args);
		}
	}
}