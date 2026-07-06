using R3;
using TriInspector;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Game
{
    [GenerateSerializationForType(typeof(PlayerScoreData))]
    public sealed class ScoreSystem : NetworkBehaviour
    {
#if UNITY_EDITOR
        [ShowInPlayMode]
        public int[] Scores;
#endif
        public Observable<PlayerScoreData> OnScoreChanged => _onScoreChanged;
        public readonly NetworkList<PlayerScoreData> PlayerScores = new NetworkList<PlayerScoreData>();
        private ScoreSettingsConfig _scoreSettings;
        private SessionSystem _sessionSystem;
        private readonly Subject<PlayerScoreData> _onScoreChanged = new Subject<PlayerScoreData>();

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
                Debug.Log($"[ScoreSystem] OnNetworkSpawn() session has {_sessionSystem.PlayerDatas.Count} players.");
                foreach (PlayerData playerData in _sessionSystem.PlayerDatas)
                {
                    HandlePlayerJoined(playerData);
                }
                _sessionSystem.OnPlayerJoined += HandlePlayerJoined;
            }
        }

        private void HandlePlayerJoined(PlayerData player)
        {
            if (!IsServer)
                return;

            Debug.Log($"[ScoreSystem] handling player {player.Nickname} joined.");
            var scoreData = new PlayerScoreData(player, 0);
            PlayerScores.Add(scoreData);
            _onScoreChanged.OnNext(scoreData);
        }

        public void ScoreKillPoints(PlayerData killerPlayerData)
        {
            for (int i = 0, count = PlayerScores.Count; i < count; i++)
            {
                PlayerScoreData playerScoreData = PlayerScores[i];
                
                if (playerScoreData.PlayerData == killerPlayerData)
                {
                    playerScoreData.Score += _scoreSettings.KillPoints;
                    PlayerScores[i] = playerScoreData;
                    _onScoreChanged.OnNext(playerScoreData);

                    return;
                }
            }

            var scoreData = new PlayerScoreData(killerPlayerData, _scoreSettings.KillPoints);
            PlayerScores.Add(scoreData);
            _onScoreChanged.OnNext(scoreData);
        }

        public int GetPlace(PlayerData playerData)
        {
            int playerScore = 0;
            bool found = false;

            // Находим счет игрока
            foreach (var scoreData in PlayerScores)
            {
                if (scoreData.PlayerData.Equals(playerData))
                {
                    playerScore = scoreData.Score;
                    found = true;
                    break;
                }
            }

            if (!found)
                return -1; // игрок отсутствует

            // Подсчитываем, сколько игроков имеют больший счет
            int place = 1;

            foreach (var scoreData in PlayerScores)
            {
                if (scoreData.Score > playerScore)
                    place++;
            }

            return place;
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
