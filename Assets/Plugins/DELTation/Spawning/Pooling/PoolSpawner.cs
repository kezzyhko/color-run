using DELTation.Spawning;
using UnityEngine;

namespace DELTation.Spawning.Pooling
{
	[RequireComponent(typeof(Pool))]
	public class PoolSpawner<T> : MonoBehaviour, ISpawner<T> where T : class
	{
		public T Spawn(Vector3 position, Quaternion rotation)
		{
			var provider = _pool.GetObject(position, rotation);
			return provider.Get<T>();
		}

		private void Awake()
		{
			_pool = GetComponent<Pool>();
		}

		private Pool _pool;
	}
}