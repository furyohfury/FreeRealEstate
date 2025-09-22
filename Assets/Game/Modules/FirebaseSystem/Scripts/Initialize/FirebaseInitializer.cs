using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Firebase;
using UnityEngine;

namespace FirebaseSystem
{
	public sealed class FirebaseInitializer
	{
		private const int MILLISECONDS_DELAY = 10000;

		public async UniTask<bool> Init()
		{
			var cts = new CancellationTokenSource(MILLISECONDS_DELAY);
			try
			{
				var status = await FirebaseApp
				                   .CheckAndFixDependenciesAsync()
				                   .AsUniTask()
				                   .AttachExternalCancellation(cts.Token);

				if (status != DependencyStatus.Available)
				{
					Debug.LogError($"Could not resolve all Firebase dependencies: {status.ToString()}");
					return false;
				}

				Debug.Log($"Successfully initialized with status {status.ToString()}");
				return true;
			}
			catch (OperationCanceledException)
			{
				Debug.LogError($"Could not resolve all Firebase dependencies: Operation cancelled after {MILLISECONDS_DELAY} ms");
				return false;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return false;
			}
		}
	}
}