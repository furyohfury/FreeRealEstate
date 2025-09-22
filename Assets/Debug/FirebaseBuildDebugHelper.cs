using FirebaseSystem;
using TMPro;
using UnityEngine;

namespace GameDebug
{
	public class FirebaseBuildDebugHelper : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _message;

		public async void Start()
		{
			FirebaseManager firebaseManager = new();
			var register = await firebaseManager.Register("test@gmail.com", "123456", "TestUser");
			if (register is AuthFailure registerFailure)
			{
				var login = await firebaseManager.SignIn("test@gmail.com", "123456");
				if (login is AuthFailure signFailure)
				{
					_message.SetText("cant login: " + signFailure.Message);
				}
				else
				{
					_message.SetText("loggedin");
				}
			}
			else
			{
				_message.SetText("registered");
			}
		}
	}
}