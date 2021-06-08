using System;
using Object = UnityEngine.Object;

namespace DELTation.Validation
{
	public abstract class ComponentValidationErrorBase : Exception
	{
		protected ComponentValidationErrorBase(string message) : base(message) { }

		protected static string FormatMessage(Object context, Type componentType) =>
			$"Component of type {componentType.Name} was not found in {context.name}";
	}
}