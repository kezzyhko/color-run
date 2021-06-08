using DELTation.FSA.EditorUtils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/Visualization")]
	public sealed class EasyFsaVisualizer : MonoBehaviour
	{
		[SerializeField, Required("FSA is not attached."), LabelText("FSA")]
		private EasyFsa _fsa = default;

		[InfoBox("Visualization will only work if an FSA is attached to this GameObject.")]
		public bool Enabled = true;

		[HideIf("@!Enabled"), BoxGroup("Settings")]
		public bool InitialState = true;

		[HideIf("@!Enabled"), BoxGroup("Settings")]
		public bool CurrentState = true;

		[HideIf("@!Enabled"), BoxGroup("Settings")]
		public bool AllStates = true;

		[HideIf("@!Enabled"), BoxGroup("Settings")]
		public bool Transitions = true;

		private void OnDrawGizmos()
		{
			if (!Application.isEditor) return;

			if (!Enabled) return;
			if (FsaIsNotAttached) return;

			DrawInitialState();
			DrawCurrentState();

			var states = _fsa.GetComponentsInChildren<EasyState>();

			foreach (var state in states)
			{
				DrawState(state);
			}
		}

		private bool FsaIsNotAttached => _fsa == null;

		private void DrawInitialState()
		{
			if (!InitialState) return;
			if (!TryGetInitialState(out var initialState)) return;

			var statePosition = initialState.transform.position;
			Gizmos.color = Color.cyan;
			GizmosHelper.DrawArrow(statePosition + Vector3.left * 25, statePosition + Vector3.left * 10, 5f);
		}

		private void DrawCurrentState()
		{
			if (!CurrentState) return;
			if (!(_fsa.CurrentState is EasyStateBase currentState)) return;

			var size = Vector2.one * 25f;
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(currentState.transform.position, size);
		}

		private bool TryGetInitialState(out EasyStateBase initialState)
		{
			if (_fsa.InitialState is EasyStateBase currentInitialState)
			{
				initialState = currentInitialState;
				return true;
			}

			if (_fsa.SerializedInitialState != null)
			{
				initialState = _fsa.SerializedInitialState;
				return true;
			}

			initialState = default;
			return false;
		}

		private void DrawState(EasyState state)
		{
			var thisStatePosition = state.transform.position;

			if (Transitions)
				foreach (var transition in state.Transitions)
				{
					if (transition.State == null) continue;

					var otherStatePosition = transition.State.transform.position;
					Gizmos.color = Color.green;
					GizmosHelper.DrawArrow(thisStatePosition, otherStatePosition, 3f, 30f, 0.85f);

					if (transition.State == null) continue;

					var triggerTextPosition = Vector3.Lerp(thisStatePosition, otherStatePosition, 0.5f);
					var triggerName = transition.Trigger ? transition.Trigger.ToString() : "null";
					GizmosHelper.DrawText(triggerTextPosition, triggerName, Color.white, 12, 12);
				}

			if (AllStates)
			{
				const float radius = 10f;
				Gizmos.color = state.IsTerminal ? Color.red : Color.blue;
				Gizmos.DrawWireSphere(thisStatePosition, radius);
				GizmosHelper.DrawText(thisStatePosition + Vector3.forward * radius, state.name, Color.white, 18);
			}
		}
	}
}