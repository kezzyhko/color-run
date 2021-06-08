using System;

namespace DELTation.FSA.Components.Easy
{
	public interface IStateBehaviour<T> where T : IEquatable<T>
	{
		bool Enabled { get; set; }
		void TryInvokeUpdate(float deltaTime, IFsa<T> fsa);
		void TryInvokeFixedUpdate(float deltaTime, IFsa<T> fsa);
	}
}