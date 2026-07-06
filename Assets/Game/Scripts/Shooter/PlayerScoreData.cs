using System;
using Unity.Netcode;

namespace Game
{
    public struct PlayerScoreData : IEquatable<PlayerScoreData>, INetworkSerializable
    {
        public PlayerData PlayerData;
        public int Score;

        public PlayerScoreData(PlayerData playerData, int score)
        {
            PlayerData = playerData;
            Score = score;
        }

        public bool Equals(PlayerScoreData other)
        {
            return PlayerData.Equals(other.PlayerData) && Score == other.Score;
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerScoreData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PlayerData, Score);
        }

        public static bool operator ==(PlayerScoreData left, PlayerScoreData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerScoreData left, PlayerScoreData right)
        {
            return !(left == right);
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref PlayerData);
            serializer.SerializeValue(ref Score);
        }
    }
}
