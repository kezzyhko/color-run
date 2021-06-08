using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/Stop When Enter Terminal State")]
	public sealed class EasyFsaTerminalStopper : EasyTransitionListener
	{
		protected override bool AcceptsNew(IState<EasyTrigger> newState) =>
			base.AcceptsNew(newState) && newState != null && newState.IsTerminal;

		protected override void TransitionAction(IFsa<EasyTrigger> fsa, IState<EasyTrigger> oldState,
			IState<EasyTrigger> newState)
		{
			fsa.Stop();
		}
	}
}