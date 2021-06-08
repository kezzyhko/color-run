using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DELTation.FSA
{
	public sealed class ConfigurableState<T> : State<T>, IEnumerable<T>
		where T : IEquatable<T>
	{
		private readonly Dictionary<T, Transition<T>> _parametrizedTransitions = new Dictionary<T, Transition<T>>();
		private readonly Dictionary<T, IState<T>> _simpleTransitions = new Dictionary<T, IState<T>>();

		protected override bool HandleTrigger(IFsa<T> fsa, T trigger, out IState<T> state)
		{
			if (_parametrizedTransitions.TryGetValue(trigger, out var transitionStateFactory))
			{
				state = transitionStateFactory(fsa, trigger, this);
				return true;
			}

			return _simpleTransitions.TryGetValue(trigger, out state);
		}

		public void Add(T trigger, Transition<T> transition)
		{
			_parametrizedTransitions[trigger] = transition;
		}

		public void Add(T trigger, IState<T> state)
		{
			_simpleTransitions[trigger] = state;
		}

		public void Remove(T trigger)
		{
			if (_parametrizedTransitions.ContainsKey(trigger))
				_parametrizedTransitions.Remove(trigger);

			if (_simpleTransitions.ContainsKey(trigger))
				_simpleTransitions.Remove(trigger);
		}

		public bool ReactsOn(T trigger) =>
			_parametrizedTransitions.ContainsKey(trigger) ||
			_simpleTransitions.ContainsKey(trigger);

		public void ClearTriggers()
		{
			_parametrizedTransitions.Clear();
			_simpleTransitions.Clear();
		}

		public void MakeTerminal()
		{
			IsTerminal = true;
		}

		public void MakeNonTerminal()
		{
			IsTerminal = false;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private IEnumerator<T> GetEnumerator() =>
			_parametrizedTransitions.Keys.Union(_simpleTransitions.Keys).GetEnumerator();
	}

	public delegate IState<T> Transition<T>(IFsa<T> fsa, T trigger, IState<T> originalState)
		where T : IEquatable<T>;
}