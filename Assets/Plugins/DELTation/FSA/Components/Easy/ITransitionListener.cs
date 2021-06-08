using System;
using JetBrains.Annotations;

namespace DELTation.FSA.Components.Easy
{
	public interface ITransitionListener<T> where T : IEquatable<T>
	{
		void OnTransition(IFsa<T> fsa,
			[CanBeNull] IState<T> oldState,
			[CanBeNull] IState<T> newState);
	}
}