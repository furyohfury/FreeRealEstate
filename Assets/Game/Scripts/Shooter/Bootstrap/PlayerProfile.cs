using UnityEngine;
using Zenject;

namespace Game
{
    public sealed class PlayerProfile : IInitializable
    {
        public string Nickname { get; private set; }

        public void Initialize()
        {
            Load();
        }

        public void SetNickname(string nickname)
        {
            Nickname = nickname;
            PlayerPrefs.SetString("Nickname", nickname);
        }

        private void Load()
        {
            Nickname = PlayerPrefs.GetString("Nickname", "");
        }
    }
}
