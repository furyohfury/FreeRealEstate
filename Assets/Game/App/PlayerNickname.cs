using Unity.Netcode;

namespace Game.App
{
	public sealed class PlayerNickname
	{
		public string Nickname
		{
			get
			{
#if UNITY_EDITOR
				if (string.IsNullOrEmpty(_nickname))
				{
					return NetworkManager.Singleton.IsHost
						? "HostTestPlayer"
						: "ClientTestPlayer";
				}
#endif
				return _nickname;
			}
			set => _nickname = value;
		}

		private string _nickname;
	}
}