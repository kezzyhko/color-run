using DELTation.Spawning;
using UnityEngine;

namespace DELTation.Effects.Spawning
{
	[RequireComponent(typeof(ParticleSystem))]
	public class DespawnableParticleSystem : BasicDespawnableObject
	{
		public override void Spawn(Vector3 position, Quaternion rotation)
		{
			base.Spawn(position, rotation);
			_particleSystem.Clear();
			_particleSystem.Play();
		}

		private void OnParticleSystemStopped()
		{
			Despawn();
		}

		private void Awake()
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}

		private ParticleSystem _particleSystem;
	}
}