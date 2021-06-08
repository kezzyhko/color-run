using System;
using UnityEngine;

namespace DELTation.Spawning
{
	public class BasicDespawnableObject : MonoBehaviour, IDespawnable
	{
		public bool IsSpawned => gameObject.activeSelf;

		public virtual void Spawn(Vector3 position, Quaternion rotation)
		{
			transform.SetPositionAndRotation(position, rotation);
			gameObject.SetActive(true);
		}

		public virtual void Despawn()
		{
			gameObject.SetActive(false);
			OnDespawned();
		}

		protected virtual void OnDespawned()
		{
			Despawned?.Invoke(this, EventArgs.Empty);
		}

		private void OnDisable()
		{
			if (!gameObject.activeSelf)
				Despawn();
		}

		public event EventHandler Despawned;
	}
}