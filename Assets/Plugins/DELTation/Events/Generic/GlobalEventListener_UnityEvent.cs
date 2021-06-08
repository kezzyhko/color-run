using UnityEngine.Events;

namespace DELTation.Events.Generic
{
	public class GlobalEventListener_UnityEvent<TArgs, TEvent, TUnityEvent> : GlobalEventListener<TArgs, TEvent>
		where TUnityEvent : UnityEvent<TArgs> where TEvent : GlobalEvent<TArgs>
	{
		public TUnityEvent Event = default;

		protected override void OnEvent(TArgs args)
		{
			Event?.Invoke(args);
		}
	}
}