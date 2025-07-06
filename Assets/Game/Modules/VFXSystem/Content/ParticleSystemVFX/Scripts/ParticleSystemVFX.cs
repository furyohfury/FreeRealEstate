using System;
using UnityEngine;
using VFX;

namespace Game.VFX
{
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleSystemVFX : MonoBehaviour, IVFX
	{
		[SerializeField]
		private ParticleSystem _particleSystem;
		[SerializeField]
		private bool _playOnAwake;

		public event Action<IVFX> OnVFXEnd;

		public void Play()
		{
			_particleSystem.Play();
		}

		public void Move(Vector3 pos, Quaternion rot)
		{
			transform.SetPositionAndRotation(pos, rot);
		}

		public void Remove()
		{
			Destroy(gameObject);
		}

		private void OnParticleSystemStopped()
		{
			OnVFXEnd?.Invoke(this);
		}

		private void OnValidate()
		{
			if (_particleSystem != null)
			{
				var main = _particleSystem.main;
				if (main.playOnAwake == true)
				{
					main.playOnAwake = false;
					Debug.Log($"Play on awake on particles system {_particleSystem.gameObject.name} is set to false");
				}

				if (main.stopAction != ParticleSystemStopAction.Callback)
				{
					main.stopAction = ParticleSystemStopAction.Callback;
					Debug.Log($"stopAction on particles system {_particleSystem.gameObject.name} is set to Callback");
				}
			}
		}
	}
}