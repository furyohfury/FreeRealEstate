using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using FirebaseSystem;

namespace Game.Meta.Authentication
{
	public sealed class FirebaseAuthenticationAdapter : IRegisterable, ILoginable, IUserData
	{
		private readonly FirebaseManager _firebaseManager;
		private FirebaseUser _firebaseUser;

		public FirebaseAuthenticationAdapter(FirebaseManager firebaseManager)
		{
			_firebaseManager = firebaseManager;
		}

		public async UniTask<IAuthResult> Register(string email, string password, string nickname, CancellationToken token = default)
		{
			try
			{
				var result = await _firebaseManager.Register(email, password, nickname)
				                                   .AttachExternalCancellation(token);
				if (result is AuthSuccess success)
				{
					_firebaseUser = success.User;
					return new SuccessAuthResult();
				}

				if (result is AuthFailure failure)
				{
					return new ErrorAuthResult(failure.Message);
				}

				return new NullAuthResult();
			}
			catch (OperationCanceledException e)
			{
				await UniTask.SwitchToMainThread();
				return new ErrorAuthResult(e.Message);
			}
		}

		public async UniTask<IAuthResult> Login(string email, string password, CancellationToken token = default)
		{
			try
			{
				var result = await _firebaseManager.SignIn(email, password)
				                                   .AttachExternalCancellation(token);;
				if (result is AuthSuccess success)
				{
					_firebaseUser = success.User;
					return new SuccessAuthResult();
				}

				if (result is AuthFailure failure)
				{
					return new ErrorAuthResult(failure.Message);
				}

				return new NullAuthResult();
			}
			catch (OperationCanceledException e)
			{
				await UniTask.SwitchToMainThread();
				return new ErrorAuthResult(e.Message);
			}
		}

		public UserData GetUserData()
		{
			var id = "ErrorId";
			var nickname = "ErrorNickname";
			if (_firebaseManager.TryGetCurrentUser(out var user))
			{
				id = user.UserId;
				nickname = user.DisplayName;
			}

			return new UserData()
			       {
				       ID = id, DisplayName = nickname
			       };
		}
	}
}