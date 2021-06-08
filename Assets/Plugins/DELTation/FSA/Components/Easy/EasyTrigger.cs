using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DELTation.FSA.Components.Easy
{
	[CreateAssetMenu(menuName = FsaMenu.RootFolder + "/Trigger", fileName = "Trigger")]
	public sealed class EasyTrigger : ScriptableObject, IEquatable<EasyTrigger>
	{
		[SerializeField, Tooltip("If set, the specified name will be used instead of the actual asset's name.")]
		private bool _overrideName = false;

		[SerializeField, Required("Name is required."), HideIf("@!" + nameof(_overrideName))]
		private string _overridenName = "Trigger";

		public bool Equals(EasyTrigger other) => ReferenceEquals(this, other);

		public override bool Equals(object obj) => ReferenceEquals(this, obj);

		public override int GetHashCode() => base.GetHashCode();

		public override string ToString() => _overrideName ? _overridenName : name;
	}
}