using DELTation.Spawning;

namespace DELTation.Effects.Spawning
{
	public interface ITypedEffectSpawnerResolver
	{
		ISpawner<Effect> Get(EffectType effectType);
	}
}