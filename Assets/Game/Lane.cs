using System.Collections.Generic;
using Game.Extensions;
using TriInspector;
using UnityEngine;

namespace Game
{
    public sealed class Lane : MonoBehaviour
    {
        [field: SerializeField]
        public float Speed { get; set; }
        [field: SerializeField]
        public GameColor GameColor { get; private set; }
        public bool IsMoving { get; private set; } = true;
        public Vector3 SpawnPos => _itemsSpawnPos.position;
        public Quaternion SpawnRot => _itemsSpawnPos.rotation;
        /// <summary>
        /// Номер линии. Начинается с нуля
        /// </summary>
        [field: SerializeField]
        public int Number { get; set; }
        [field: SerializeField]
        public ScoreZone ScoreZone { get; private set; }

        [SerializeField]
        private Transform _itemsSpawnPos;
        [SerializeField]
        [RequiredGet]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        [RequiredGet(InChildren = true)]
        private LaneItemSpawner _laneItemSpawner;
        public readonly HashSet<Item> LinkedItems = new HashSet<Item>();

        public void AddItem(Item item)
        {
            LinkedItems.Add(item);
        }

        public void RemoveItem(Item item)
        {
            LinkedItems.Remove(item);
        }

        public void Highlight()
        {
            // TODO VFX
            SetVisualColor(Color.aquamarine);
        }

        public void DisableHighlight()
        {
            // TODO VFX
            SetVisualColor(GameColor);
        }

        public void StartSpawning()
        {
            _laneItemSpawner.SwitchSpawnState(true);
        }

        public void StopSpawning()
        {
            _laneItemSpawner.SwitchSpawnState(false);
        }

        public void SetSpawnInterval(float interval)
        {
            _laneItemSpawner.SpawnInterval = interval;
        }

        public void SetRandomSpawnOffset(float spawnOffset)
        {
            _laneItemSpawner.RandomSpawnOffset = spawnOffset;
        }

        public void SetColor(GameColor color)
        {
            GameColor = color;
            SetVisualColor(color);
        }

        private void SetVisualColor(GameColor color)
        {
            SetVisualColor(color.ToColor());
        }

        private void SetVisualColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
    }
}
