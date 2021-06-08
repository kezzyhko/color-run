using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[AddComponentMenu(FsaMenu.RootFolder + "/Logger", int.MaxValue)]
	public class EasyFsaLogger : FsaLoggingComponent<EasyTrigger, EasyFsa> { }
}