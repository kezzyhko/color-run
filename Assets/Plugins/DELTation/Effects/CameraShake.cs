using Cinemachine;
using UnityEngine;

namespace DELTation.Effects
{
	public sealed class CameraShake : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _amplitudeGain = 1f;
		[SerializeField, Min(0f)] private float _duration = 1f;
		[SerializeField] private AnimationCurve _amplitudeOverTime = AnimationCurve.Linear(0f, 1f, 1f, 0f);

		private void Update()
		{
			_remainingTime -= Time.deltaTime;
			UpdateAmplitude();
			if (_remainingTime > 0f) return;

			enabled = false;
		}

		private void UpdateAmplitude()
		{
			var t = Mathf.Clamp01(1f - _remainingTime / _duration);
			_noise.m_AmplitudeGain = _amplitudeOverTime.Evaluate(t) * _amplitudeGain;
		}

		public void Activate()
		{
			enabled = true;
			_remainingTime = _duration;
			UpdateAmplitude();
		}

		public void Construct(CinemachineVirtualCamera vc)
		{
			_noise = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		}

		private float _remainingTime;
		private CinemachineBasicMultiChannelPerlin _noise;
	}
}