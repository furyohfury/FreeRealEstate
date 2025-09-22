using Firebase.Auth;

namespace FirebaseSystem
{
	public sealed class AuthFailure : IAuthResult
	{
		public AuthError ErrorCode;
		public string Message;

		public AuthFailure(AuthError errorCode, string message)
		{
			ErrorCode = errorCode;
			Message = message;
		}
	}
}