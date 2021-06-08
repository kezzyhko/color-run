using System;
using JetBrains.Annotations;

namespace DELTation.FSA
{
	public struct StateChangeArgs<T> where T : IEquatable<T>
	{
		[CanBeNull] public IState<T> OldState;

		[CanBeNull] public IState<T> NewState;

		public override bool Equals(object obj) =>
			obj is StateChangeArgs<T> otherArgs &&
			Equals(otherArgs);

		public bool Equals(StateChangeArgs<T> other) =>
			Equals(OldState, other.OldState) &&
			Equals(NewState, other.NewState);

		public override int GetHashCode()
		{
			unchecked
			{
				return ((OldState != null ? OldState.GetHashCode() : 0) * 397) ^
				       (NewState != null ? NewState.GetHashCode() : 0);
			}
		}
	}
}