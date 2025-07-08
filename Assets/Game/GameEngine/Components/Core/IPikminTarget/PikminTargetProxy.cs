using System;
using Game;
using UnityEngine;

namespace GameEngine
{
	[RequireComponent(typeof(Collider))]
	public sealed class PikminTargetProxy : MonoBehaviour, IPikminTarget
	{
		public GameObject Target => _iPikminTarget.Target;
		
		[SerializeField]
		private GameObject _source;
		private IPikminTarget _iPikminTarget;

		private void Awake()
		{
			if (_source.TryGetComponent(out IPikminTarget iPikminTarget) == false)
			{
				throw new NullReferenceException($"No IPikminTarget on {_source.name}");
			}

			_iPikminTarget = iPikminTarget;
		}

		public bool TrySetTarget(GameObject target)
		{
			return _iPikminTarget.TrySetTarget(target);
		}
	}
}