using UnityEngine;

namespace Game
{
    public sealed class LanesSessionSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private Lane _prefab;

        private void Awake()
        {
            foreach (Transform sceneLane in _container)
            {
                Destroy(sceneLane.gameObject);
            }
        }

        public void SpawnLanes()
        {
            ClearExistingLanes();
            SessionParams sessionParams = GameParamsService.Instance.SessionParams;
            int lanesNumber = sessionParams.LanesNumber;
            Transform[] lanesTransforms = LanesPositionsService.Instance.GetLanesTransforms(lanesNumber);
            Lane[] lanes = new Lane[lanesNumber];

            for (int i = 0; i < lanesTransforms.Length; i++)
            {
                var lane = Instantiate(_prefab, lanesTransforms[i].position, lanesTransforms[i].rotation, _container);

                lane.name = $"Lane_{i}";
                lane.SetColor(sessionParams.GameColors[i]);
                lane.Number = i;
                lane.Speed = sessionParams.lanesLaneSpeedFormula.GetLanesSpeed(0);
                lane.SetSpawnInterval(sessionParams.itemSpawnIntervalFormula.GetInterval());
                lane.SetRandomSpawnOffset(sessionParams.itemSpawnIntervalFormula.GetRandomSpawnOffset());
                lanes[i] = lane;
            }

            LaneSystem.Instance.Lanes = lanes;
        }

        private void ClearExistingLanes()
        {
            foreach (Transform laneTransform in _container)
            {
                Destroy(laneTransform.gameObject);
            }
        }
    }
}
