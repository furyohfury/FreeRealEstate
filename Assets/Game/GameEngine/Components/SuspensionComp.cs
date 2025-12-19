using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace GameEngine
{
	[Serializable]
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public struct SuspensionComp : IComponent
	{
		public float Damper;
		public float MaxSuspension;
		public float Stiffness;
		public float Radius;
	}
}
