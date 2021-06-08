using System;
using System.Collections.Generic;
using DELTation.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.Spawning.Pooling
{
	public class Pool : MonoBehaviour
	{
		[SerializeField, ValidateInput(nameof(ValidatePrefab))]
		private EntityBase _prefab = default;

		[SerializeField, Min(1)] private int _initialCapacity = 10;

		[ShowInInspector, HideInEditorMode] public int Capacity => _allObjects.Count;

		public IEntity GetObject(Vector3 position, Quaternion rotation)
		{
			var despawnable = _freeObjects.Count == 0 ? CreateNewObjectDespawned(position, rotation) : _freeObjects[0];

			despawnable.Spawn(position, rotation);
			_freeObjects.Remove(despawnable);
			_freeObjectsSet.Remove(despawnable);
			return _providersOfObjects[despawnable];
		}

		private IDespawnable CreateNewObjectDespawned(Vector3 position, Quaternion rotation)
		{
			var componentCache = Instantiate(_prefab, position, rotation, transform);
			var despawnable = componentCache.Get<IDespawnable>();
			despawnable.Despawned += _onDespawned;
			_allObjects.Add(despawnable);
			_freeObjects.Add(despawnable);
			_freeObjectsSet.Add(despawnable);
			_providersOfObjects[despawnable] = componentCache;
			despawnable.Despawn();
			return despawnable;
		}

		private void Awake()
		{
			_onDespawned = (sender, args) =>
			{
				var despawnable = (IDespawnable) sender;
				if (_freeObjectsSet.Contains(despawnable)) return;

				_freeObjects.Add(despawnable);
				_freeObjectsSet.Add(despawnable);
			};

			for (var i = 0; i < _initialCapacity; i++)
			{
				CreateNewObjectDespawned(Vector3.zero, Quaternion.identity);
			}
		}

		private void OnDestroy()
		{
			foreach (var despawnable in _allObjects)
			{
				despawnable.Despawned -= _onDespawned;
			}
		}

		private EventHandler _onDespawned;

		private readonly IDictionary<IDespawnable, IEntity> _providersOfObjects =
			new Dictionary<IDespawnable, IEntity>();

		private readonly List<IDespawnable> _allObjects = new List<IDespawnable>();
		private readonly List<IDespawnable> _freeObjects = new List<IDespawnable>();
		private readonly HashSet<IDespawnable> _freeObjectsSet = new HashSet<IDespawnable>();

		private bool ValidatePrefab(ref string message)
		{
			if (_prefab == null)
			{
				message = "Prefab cannot be null.";
				return false;
			}

			if (_prefab.TryGetComponent(out IDespawnable _)) return true;

			message += "Prefab is not despawnable.";
			return false;
		}
	}
}