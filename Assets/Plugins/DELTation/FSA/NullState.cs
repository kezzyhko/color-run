using System;

namespace DELTation.FSA
{
	public sealed class NullState<T> : State<T> where T : IEquatable<T>
	{
		protected override bool HandleTrigger(IFsa<T> fsa, T trigger, out IState<T> state)
		{
			state = default;
			return false;
		}
	}
}