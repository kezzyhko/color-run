using UnityEngine;

namespace DELTation.Spawning
{
	public interface ISpawner<out T>
	{
		T Spawn(Vector3 position, Quaternion rotation);
	}
}