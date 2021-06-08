using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DELTation.Validation
{
	public static class ValidationExtensions
	{
		public static void RequireFromAnchor<T>([NotNull] this Component context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			context.RequireInParent(out IAnchor anchor);
			anchor.gameObject.RequireInChildren(out component);
		}

		public static void RequireFromAnchor<T>([NotNull] this GameObject context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			context.RequireInParent(out IAnchor anchor);
			anchor.gameObject.RequireInChildren(out component);
		}

		public static void RequireFromAnchor([NotNull] this Component context, [NotNull] Type type,
			out Component component)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (type == null) throw new ArgumentNullException(nameof(type));

			context.RequireInParent(out IAnchor anchor);
			anchor.gameObject.RequireInChildren(type, out component);
		}

		public static void RequireFromAnchor([NotNull] this GameObject context, [NotNull] Type type,
			out Component component)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (type == null) throw new ArgumentNullException(nameof(type));

			context.RequireInParent(out IAnchor anchor);
			anchor.gameObject.RequireInChildren(out component);
		}

		public static void Require<T>([NotNull] this GameObject context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			if (context.TryGetComponent(out component)) return;

			throw new ComponentValidationError(context, typeof(T));
		}

		public static void Require<T>([NotNull] this Component context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			if (context.TryGetComponent(out component)) return;

			throw new ComponentValidationError(context, typeof(T));
		}

		private static void Require([NotNull] this Component context, [NotNull] Type type, out Component component)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (type == null) throw new ArgumentNullException(nameof(type));

			if (context.TryGetComponent(type, out component)) return;

			throw new ComponentValidationError(context, type);
		}

		public static void RequireInParent<T>([NotNull] this GameObject context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			component = context.GetComponentInParent<T>();
			if (component != null) return;

			throw new ParentComponentValidationError(context, typeof(T));
		}

		public static void RequireInParent<T>([NotNull] this Component context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			component = context.GetComponentInParent<T>();
			if (component != null) return;

			throw new ParentComponentValidationError(context, typeof(T));
		}

		public static void RequireInParent([NotNull] this Component context, [NotNull] Type type,
			out Component component)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (type == null) throw new ArgumentNullException(nameof(type));

			component = context.GetComponentInParent(type);
			if (component != null) return;

			throw new ParentComponentValidationError(context, type);
		}

		public static void RequireInChildren<T>([NotNull] this GameObject context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			component = context.GetComponentInChildren<T>();
			if (component != null) return;

			throw new ChildrenComponentValidationError(context, typeof(T));
		}

		public static void RequireInChildren<T>([NotNull] this Component context, out T component) where T : class
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			component = context.GetComponentInChildren<T>();
			if (component != null) return;

			throw new ChildrenComponentValidationError(context, typeof(T));
		}

		private static void RequireInChildren([NotNull] this Component context, [NotNull] Type type,
			out Component component)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (type == null) throw new ArgumentNullException(nameof(type));

			component = context.GetComponentInChildren(type);
			if (component != null) return;

			throw new ChildrenComponentValidationError(context, type);
		}

		private static void RequireInChildren([NotNull] this GameObject context, [NotNull] Type type,
			out Component component)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (type == null) throw new ArgumentNullException(nameof(type));

			component = context.GetComponentInChildren(type);
			if (component != null) return;

			throw new ChildrenComponentValidationError(context, type);
		}

		public static void RequireAnywhere<T>([NotNull] this Object context, out T component) where T : Object
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			component = Object.FindObjectOfType<T>();
			if (component != null) return;

			throw new AnywhereComponentValidationError(typeof(T), context);
		}

		private static void RequireAnywhere([NotNull] this Object context, [NotNull] Type type, out Object component)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (type == null) throw new ArgumentNullException(nameof(type));

			component = Object.FindObjectOfType(type);
			if (component != null) return;

			throw new AnywhereComponentValidationError(type, context);
		}

		public static void RequireAnywhere<T>(out T component) where T : Object
		{
			component = Object.FindObjectOfType<T>();
			if (component != null) return;

			throw new AnywhereComponentValidationError(typeof(T));
		}

		public static void ResolveDependencies([NotNull] this Component context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			var type = context.GetType();

			foreach (var field in type.GetDependencyFields())
			{
				var attribute = field.GetCustomAttribute<DependencyAttribute>();
				var value = Resolve(context, field.FieldType, attribute.Source);
				field.SetValue(context, value);
			}

			foreach (var property in type.GetDependencyProperties())
			{
				var attribute = property.GetCustomAttribute<DependencyAttribute>();
				var value = Resolve(context, property.PropertyType, attribute.Source);

				if (property.CanWrite)
					property.SetValue(context, value);
				else
					throw new InvalidOperationException($"Property {property} of {type} has no setter.");
			}
		}

		public static IEnumerable<FieldInfo> GetDependencyFields(this Type type)
		{
			return type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
				.Where(f => Attribute.IsDefined(f, typeof(DependencyAttribute)));
		}

		public static IEnumerable<PropertyInfo> GetDependencyProperties(this Type type)
		{
			return type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
				.Where(f => Attribute.IsDefined(f, typeof(DependencyAttribute)));
		}

		private static object Resolve(Component context, Type type, Source source)
		{
			switch (source)
			{
				case Source.Local:
				{
					context.Require(type, out var component);
					return component;
				}

				case Source.FromParents:
				{
					context.RequireInParent(type, out var component);
					return component;
				}

				case Source.FromChildren:
				{
					context.RequireInChildren(type, out var component);
					return component;
				}

				case Source.Global when typeof(Object).IsAssignableFrom(type):
				{
					context.RequireAnywhere(type, out var component);
					return component;
				}

				case Source.Global: throw NewGlobalDependencyIllegalTypeException(type, context);

				case Source.Anchor:
				{
					context.RequireFromAnchor(type, out var component);
					return component;
				}

				default: throw new ArgumentOutOfRangeException(nameof(source), source, null);
			}
		}

		public static Exception NewGlobalDependencyIllegalTypeException(Type type, object context) =>
			throw new InvalidOperationException(
				$"Global dependencies must derive from {typeof(Object)} but the type was {type}. Context: {context}.");
	}
}