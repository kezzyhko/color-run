using UnityEngine.Events;

namespace DELTation.Events
{
	public sealed class GlobalEventListener_UnityEvent : GlobalEventListener
	{
		public UnityEvent Event = default;

		protected override void OnEvent()
		{
			Event?.Invoke();
		}
	}
}