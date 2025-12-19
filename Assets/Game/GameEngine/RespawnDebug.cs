using System;
using TriInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine
{
	public sealed class RespawnDebug : MonoBehaviour
	{
		[SerializeField]
		GameObject _player;
		[SerializeField]
		private Transform _point;

		[Button]
		private void Respawn()
		{
			_player.transform.position = _point.position;
			_player.transform.rotation = _point.rotation;
			_player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			_player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Respawn();
			}

			if (Input.GetKeyDown(KeyCode.F))
			{
				Flip();
			}
		}

		private void Flip()
		{
			Respawn();
			_player.transform.rotation *= Quaternion.Euler(Random.Range(0, 90f), 0f, Random.Range(0, 90f));
		}
	}
}
