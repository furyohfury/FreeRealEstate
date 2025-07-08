using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	[Serializable]
	public sealed class PikminControlComponent
	{
		[ShowInInspector]
		private HashSet<GameObject> _pikmins = new();

		[Button]
		public void AddPikmin(GameObject pikmin)
		{
			_pikmins.Add(pikmin);
		}

		[Button]
		public void RemovePikmin(GameObject pikmin)
		{
			_pikmins.Remove(pikmin);
		}

		public void SetTargetToPikmins(GameObject target, bool isPlayer)
		{
			foreach (var pikmin in _pikmins.ToArray())
			{
				if (pikmin.TryGetComponent(out IPikminTarget iPikminTarget) == false)
				{
					throw new NullReferenceException($"No IPikminTarget on {pikmin.name}");
				}

				if (iPikminTarget.TrySetTarget(target) && !isPlayer)
				{
					RemovePikmin(pikmin);
				}
			}
		}
	}
}