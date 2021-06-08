using UnityEngine;

namespace DELTation.Events
{
	[CreateAssetMenu(menuName = AssetPath + "No Args")]
	public sealed class GlobalEvent : ScriptableObject
	{
		public void Raise() => InnerEvent?.Invoke();

		public void AddListener(GlobalEventHandler action) => InnerEvent += action;
		public void RemoveListener(GlobalEventHandler action) => InnerEvent -= action;

		private event GlobalEventHandler InnerEvent;

		public const string AssetPath = "Event/";
	}
}