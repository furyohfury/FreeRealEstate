using System;
using Unity.Netcode;

namespace Game
{
    public struct PlayerScoreData : IEquatable<PlayerScoreData>, INetworkSerializable
    {
        public ulong ClientId;
        public int Score;

        public PlayerScoreData(ulong clientId, int score)
        {
            ClientId = clientId;
            Score = score;
        }

        public bool Equals(PlayerScoreData other)
        {
            return ClientId == other.ClientId && Score == other.Score;
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerScoreData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClientId, Score);
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ClientId);
            serializer.SerializeValue(ref Score);
        }
    }
}
