using System;
using Unity.Collections;
using Unity.Netcode;

namespace Game
{
    public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
    {
        public ulong NetworkObjID;
        public ulong clientID;
        public FixedString32Bytes Nickname;

        public PlayerData(ulong networkObjID, ulong clientID, string nickname)
        {
            NetworkObjID = networkObjID;
            this.clientID = clientID;
            Nickname = nickname;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref NetworkObjID);
            serializer.SerializeValue(ref clientID);
            serializer.SerializeValue(ref Nickname);
        }

        public bool Equals(PlayerData other)
        {
            return NetworkObjID == other.NetworkObjID && clientID == other.clientID && Nickname == other.Nickname;
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerData other && Equals(other);
        }

        public static bool operator ==(PlayerData lhs, PlayerData rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(PlayerData lhs, PlayerData rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NetworkObjID, clientID, Nickname);
        }
    }
}
