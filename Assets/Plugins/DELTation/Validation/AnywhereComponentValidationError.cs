using System;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace DELTation.Validation
{
	public sealed class AnywhereComponentValidationError : ComponentValidationErrorBase
	{
		public AnywhereComponentValidationError(Type componentType, [CanBeNull] Object context = null) :
			base(FormatMessage(componentType, context)) { }

		private static string FormatMessage(Type componentType, [CanBeNull] Object context = null)
		{
			var baseMessage = $"Component of type {componentType.Name} was not found";

			if (context != null) baseMessage += $" (called from {context.name})";

			baseMessage += ".";

			return baseMessage;
		}
	}
}