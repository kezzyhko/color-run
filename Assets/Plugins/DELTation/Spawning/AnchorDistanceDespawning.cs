using DELTation.UnityUtilities;
using UnityEngine;

namespace DELTation.Spawning
{
	public sealed class AnchorDistanceDespawning : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _despawnDistance = 75f;
		[SerializeField, Min(0f)] private float _minLifetime = 10f;

		private void Update()
		{
			if (Time.time < _enableTime + _minLifetime) return;
			if (!_despawnable.IsSpawned) return;

			var distance = GetDistanceToTarget();
			if (distance >= _despawnDistance)
				_despawnable.Despawn();
		}

		private void OnEnable()
		{
			_enableTime = Time.time;
		}

		private float GetDistanceToTarget() => VectorExtensions.DistanceXZ(transform.position, _spawnAnchor.Position);

		public void Construct(IDespawnable despawnable, SpawnAnchor spawnAnchor)
		{
			_despawnable = despawnable;
			_spawnAnchor = spawnAnchor;
		}

		private IDespawnable _despawnable;
		private SpawnAnchor _spawnAnchor;
		private float _enableTime;
	}
}