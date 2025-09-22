using Firebase.Auth;

namespace FirebaseSystem
{
	public sealed class AuthSuccess : IAuthResult
	{
		public FirebaseUser User;

		public AuthSuccess(FirebaseUser user)
		{
			User = user;
		}
	}
}