using System;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace DELTation.Events.Generic.Specific
{
	[Serializable]
	public sealed class UnityObjectEvent : UnityEvent<Object> { }
}