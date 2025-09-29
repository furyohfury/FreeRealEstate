using System;
using UnityEngine;

namespace Audio
{
	public sealed class CallbackOnStart : MonoBehaviour
	{
		private Action _callback;

		public void SetCallback(Action callback) => _callback = callback;
		
		private void Start()
		{
			_callback?.Invoke();
			Destroy(this);
		}
	}
}