using System;
using UnityEngine;

namespace Game.ElementHandle
{
	[Serializable]
	public struct JudgementSetting
	{
		public JudgementType Type;
		[Range(0, 1)]
		public float ClickWindowFraction;
	}
}