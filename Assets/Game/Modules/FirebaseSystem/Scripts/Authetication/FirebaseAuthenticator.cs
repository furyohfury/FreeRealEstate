using System;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;

namespace FirebaseSystem
{
	public sealed class FirebaseAuthenticator
	{
		private readonly FirebaseAuth _auth;

		public FirebaseAuthenticator()
		{
#if UNITY_EDITOR
			_auth = FirebaseAuth.GetAuth(FirebaseApp.Create(AppOptions.LoadFromJsonConfig("{\n  \"project_info\": {\n    \"project_number\": \"844857907946\",\n    \"firebase_url\": \"https://dadadada-be9d8-default-rtdb.europe-west1.firebasedatabase.app\",\n    \"project_id\": \"dadadada-be9d8\",\n    \"storage_bucket\": \"dadadada-be9d8.firebasestorage.app\"\n  },\n  \"client\": [\n    {\n      \"client_info\": {\n        \"mobilesdk_app_id\": \"1:844857907946:android:cc3b87ef85e754b0165593\",\n        \"android_client_info\": {\n          \"package_name\": \"com.FuryOhFury.DADADADA\"\n        }\n      },\n      \"oauth_client\": [],\n      \"api_key\": [\n        {\n          \"current_key\": \"AIzaSyDgc-Ywq9eG1MTolp6Loh-k4lFeH31fqTU\"\n        }\n      ],\n      \"services\": {\n        \"appinvite_service\": {\n          \"other_platform_oauth_client\": []\n        }\n      }\n    }\n  ],\n  \"configuration_version\": \"1\"\n}"), "CustomFirebaseApp"));
#else
			_auth = FirebaseAuth.DefaultInstance;
#endif
		}

		public bool TryGetCurrentUser(out FirebaseUser user)
		{
			user = _auth.CurrentUser;
			return user != null;
		}

		public bool TryGetCurrentUserEmail(out string email)
		{
			if (TryGetCurrentUser(out var user))
			{
				email = user.Email;
				return true;
			}

			email = null;
			return false;
		}

		public bool TryGetCurrentUserDisplayName(out string displayName)
		{
			if (TryGetCurrentUser(out var user))
			{
				displayName = user.DisplayName;
				return true;
			}

			displayName = null;
			return false;
		}

		public async UniTask<IAuthResult> Register(string mail, string password, string displayName)
		{
			try
			{
				AuthResult authResult = await _auth
				                              .CreateUserWithEmailAndPasswordAsync(
					                              mail,
					                              password
					                              )
				                              .AsUniTask();
				var user = authResult.User;
				var profile = new UserProfile { DisplayName = displayName };
				await user.UpdateUserProfileAsync(profile).AsUniTask();

				Debug.Log($"Successfully registered user {user.DisplayName}");
				return new AuthSuccess(user);
			}
			catch (Exception e)
			{
				if (e.GetBaseException() is FirebaseException firebaseException)
				{
					AuthError errorCode = (AuthError)firebaseException.ErrorCode;
					var message = FirestoreUtils.GetUserFriendlyMessage(errorCode);
					Debug.LogWarning($"Couldn't register: {message}");
					return new AuthFailure(errorCode, message);
				}

				return new AuthFailure(AuthError.None, FirestoreUtils.GetUserFriendlyMessage(AuthError.None));
			}
		}

		public async UniTask<IAuthResult> SignIn(string mail, string password)
		{
			try
			{
				AuthResult task = await _auth
				                        .SignInWithEmailAndPasswordAsync(
					                        mail,
					                        password
					                        )
				                        .AsUniTask();
				FirebaseUser user = task.User;
				Debug.Log($"Successfully signed in as user {user.DisplayName}");

				return new AuthSuccess(user);
			}
			catch (Exception e)
			{
				if (e.GetBaseException() is FirebaseException firebaseException)
				{
					AuthError errorCode = (AuthError)firebaseException.ErrorCode;
					var message = FirestoreUtils.GetUserFriendlyMessage(errorCode);
					Debug.LogWarning($"Couldn't register: {message}");
					return new AuthFailure(errorCode, message);
				}

				return new AuthFailure(AuthError.None, FirestoreUtils.GetUserFriendlyMessage(AuthError.None));
			}
		}

		public async UniTask<bool> DeleteCurrentUser()
		{
			var currentUser = _auth.CurrentUser;
			if (currentUser == null)
			{
				Debug.LogError("No current user to delete");
				return false;
			}

			try
			{
				await currentUser.DeleteAsync().AsUniTask();
				return true;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return false;
			}
		}
	}
}