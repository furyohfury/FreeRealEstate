using TriInspector;
using Unity.Netcode;
using Zenject;

namespace Game
{
    [GenerateSerializationForType(typeof(PlayerScoreData))]
    public sealed class ScoreSystem : NetworkBehaviour
    {
        [ShowInPlayMode]
        public int[] Scores;
        public NetworkList<PlayerScoreData> PlayerScores = new NetworkList<PlayerScoreData>();
        private ScoreSettingsConfig _scoreSettings;
        private SessionSystem _sessionSystem;

        [Inject]
        public void Construct(ScoreSettingsConfig scoreSettings, SessionSystem sessionSystem)
        {
            _scoreSettings = scoreSettings;
            _sessionSystem = sessionSystem;
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _sessionSystem.OnPlayerJoined += HandlePlayerJoined;
            }
        }

        private void HandlePlayerJoined(PlayerData player)
        {
            if (!IsServer)
                return;
            SetScore(player.clientID, 0);
        }

        public void SetScore(ulong clientId, int score)
        {
            for (int i = 0, count = PlayerScores.Count; i < count; i++)
            {
                PlayerScoreData playerScoreData = PlayerScores[i];

                if (PlayerScores[i].ClientId == clientId)
                {
                    playerScoreData.Score = score;
                    PlayerScores[i] = playerScoreData;

                    return;
                }
            }

            PlayerScores.Add(new PlayerScoreData(clientId, score));
        }

        public void ScoreKillPoints(ulong killerClientId)
        {
            for (int i = 0, count = PlayerScores.Count; i < count; i++)
            {
                PlayerScoreData playerScoreData = PlayerScores[i];

                if (PlayerScores[i].ClientId == killerClientId)
                {
                    playerScoreData.Score += _scoreSettings.KillPoints;
                    PlayerScores[i] = playerScoreData;

                    return;
                }
            }

            PlayerScores.Add(new PlayerScoreData(killerClientId, _scoreSettings.KillPoints));
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Scores.Length != PlayerScores.Count)
                Scores = new int[PlayerScores.Count];

            for (int i = 0, count = PlayerScores.Count; i < count; i++)
            {
                Scores[i] = PlayerScores[i].Score;
            }
        }
        #endif

        public override void OnNetworkDespawn()
        {
            if (IsServer)
                _sessionSystem.OnPlayerJoined -= HandlePlayerJoined;
        }
    }
}
