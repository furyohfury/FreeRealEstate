using Firebase.Auth;
using Firebase.Firestore;

namespace FirebaseSystem
{
	public static class FirestoreUtils
	{
		public static string GetUserFriendlyMessage(FirestoreError errorCode)
		{
			return errorCode.ToString();
		}

		public static string GetUserFriendlyMessage(AuthError errorCode)
		{
			return errorCode switch
			{
				AuthError.EmailAlreadyInUse => "Email already in use", AuthError.WeakPassword => "Weak password"
				, AuthError.InvalidEmail => "Wrong email format", AuthError.Failure => "Unexpected failure"
				, AuthError.WrongPassword => "Wrong password", AuthError.UserNotFound => "User not found", _ => errorCode.ToString()
			};
		}
	}
}