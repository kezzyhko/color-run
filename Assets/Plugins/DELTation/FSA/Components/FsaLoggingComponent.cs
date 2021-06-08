using System;
using DELTation.FSA.Logging;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components
{
	public abstract class FsaLoggingComponent<TTrigger, TFsa> : MonoBehaviour, IFsaLogger
		where TTrigger : IEquatable<TTrigger>
		where TFsa : MonoBehaviour, IFsa<TTrigger>
	{
		[SerializeField, Required("FSA is not attached."), LabelText("FSA")]
		private TFsa _fsa = default;

		[CanBeNull] private IFsaLogger _logger;

		protected void Awake()
		{
			if (_fsa == null)
			{
				Debug.LogError("FSA was not set in the inspector.", this);
				return;
			}

			_logger = new FsaLogger<TTrigger>(_fsa, Log);

			OnAwaken();
		}

		protected virtual void Log(string message)
		{
			if (!enabled) return;

			Debug.Log(message);
		}

		protected virtual void OnAwaken() { }

		protected void OnEnable()
		{
			_logger?.Start();
			OnEnabled();
		}

		protected virtual void OnEnabled() { }

		protected void OnDisable()
		{
			_logger?.Stop();
			OnDisabled();
		}

		protected virtual void OnDisabled() { }

		void IFsaLogger.Start()
		{
			_logger?.Start();
		}

		void IFsaLogger.Stop()
		{
			_logger?.Stop();
		}
	}
}