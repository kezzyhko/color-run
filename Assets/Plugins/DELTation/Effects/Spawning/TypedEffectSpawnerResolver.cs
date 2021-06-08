using System;
using System.Collections.Generic;
using System.Linq;
using DELTation.Spawning;
using UnityEngine;

namespace DELTation.Effects.Spawning
{
	public sealed class TypedEffectSpawnerResolver : MonoBehaviour, ITypedEffectSpawnerResolver
	{
		public ISpawner<Effect> Get(EffectType effectType)
		{
			if (_spawners.TryGetValue(effectType, out var spawner))
				return spawner;
			throw new ArgumentException($"Spawner of effects of type {effectType} is not registered.");
		}

		private void Awake()
		{
			var childSpawners = GetComponentsInChildren<ITypedEffectSpawner>();
			_spawners = childSpawners.ToDictionary(s => s.Type, s => (ISpawner<Effect>) s);
		}

		private IDictionary<EffectType, ISpawner<Effect>> _spawners;
	}
}