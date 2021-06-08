using DELTation.Events;

namespace DELTation.Effects.Events
{
	public sealed class OnGlobalEvent_ActivateCameraShake : GlobalEventListener
	{
		protected override void OnEvent()
		{
			_cameraShake.Activate();
		}

		public void Construct(CameraShake cameraShake) => _cameraShake = cameraShake;

		private CameraShake _cameraShake;
	}
}