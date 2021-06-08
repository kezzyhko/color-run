using System;

namespace DELTation.FSA
{
	public struct TriggerArgs<T> where T : IEquatable<T>
	{
		public IFsa<T> Fsa;
		public T Trigger;
	}
}