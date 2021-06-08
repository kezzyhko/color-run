using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/State")]
	public class EasyState : EasyStateBase
	{
		[SerializeField, ListDrawerSettings(Expanded = true)]
		private List<Transition> _transitions = new List<Transition>();

		protected override bool TriggersTransition(IFsa<EasyTrigger> fsa, EasyTrigger trigger,
			out IState<EasyTrigger> newState)
		{
			foreach (var transition in _transitions)
			{
				if (!transition.Trigger.Equals(trigger)) continue;

				newState = transition.State;
				return true;
			}

			return base.TriggersTransition(fsa, trigger, out newState);
		}

		internal IEnumerable<Transition> Transitions =>
			(IEnumerable<Transition>) _transitions ?? Array.Empty<Transition>();

		[Serializable]
		internal struct Transition
		{
#pragma warning disable 0649

			[Required, AssetList, Tooltip("When transition happens.")]
			public EasyTrigger Trigger;

			[Required, Tooltip("Destination state of the transition.")]
			public EasyStateBase State;

#pragma warning restore 0649
		}
	}
}