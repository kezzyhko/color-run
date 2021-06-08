using UnityEngine;

namespace DELTation.Events.Generic
{
	public abstract class GlobalEvent<T> : ScriptableObject
	{
		public void Raise(T args) => InnerEvent?.Invoke(args);

		public void AddListener(GlobalEventHandler<T> action) => InnerEvent += action;
		public void RemoveListener(GlobalEventHandler<T> action) => InnerEvent -= action;

		private event GlobalEventHandler<T> InnerEvent;

		public const string AssetPath = GlobalEvent.AssetPath;
	}
}