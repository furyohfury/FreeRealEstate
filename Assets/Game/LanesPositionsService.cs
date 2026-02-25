using System;
using UnityEngine;

namespace Game
{
    public sealed class LanesPositionsService : Singleton<LanesPositionsService>
    {
        [SerializeField]
        private LanePositionForNumber[] _lanePositions;

        public Transform[] GetLanesTransforms(int numberOfLanes)
        {
            for (int i = 0; i < _lanePositions.Length; i++)
            {
                if (numberOfLanes == _lanePositions[i].Number)
                {
                    return _lanePositions[i].Positions;
                }
            }

            throw new ArgumentException($"LanesPositions for {numberOfLanes} is not valid.");
        }

        [Serializable]
        private class LanePositionForNumber
        {
            public int Number;
            public Transform[] Positions;
        }
    }
}
