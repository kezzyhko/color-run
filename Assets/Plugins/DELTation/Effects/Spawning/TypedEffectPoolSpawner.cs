using DELTation.Spawning.Pooling;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.Effects.Spawning
{
	public sealed class TypedEffectPoolSpawner : PoolSpawner<Effect>, ITypedEffectSpawner
	{
		[SerializeField, Required, AssetSelector]
		private EffectType _type = default;

		public EffectType Type => _type;
	}
}