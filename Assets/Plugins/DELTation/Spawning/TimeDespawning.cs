using UnityEngine;

namespace DELTation.Spawning
{
	public sealed class TimeDespawning : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _lifetime = 60f;

		private void Update()
		{
			if (!_despawnable.IsSpawned) return;

			if (Time.time > _enableTime + _lifetime)
				_despawnable.Despawn();
		}

		private void OnEnable()
		{
			_enableTime = Time.time;
		}

		public void Construct(IDespawnable despawnable)
		{
			_despawnable = despawnable;
		}

		private IDespawnable _despawnable;
		private float _enableTime;
	}
}