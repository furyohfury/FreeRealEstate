using System.Collections.Generic;
using Game.Extensions;
using TriInspector;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public sealed class Lane : MonoBehaviour
    {
        public readonly HashSet<Item> LinkedItems = new HashSet<Item>();
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                _meshRenderer.material.SetFloat(_speedKey, value / 4.8f);
            }
        }
        [field: SerializeField]
        public GameColor GameColor { get; private set; }
        public bool IsMoving { get; private set; } = true;
        /// <summary>
        /// Номер линии. Начинается с нуля
        /// </summary>
        [field: SerializeField]
        public int Number { get; set; }
        [field: SerializeField]
        public ScoreZone ScoreZone { get; private set; }
        
        [SerializeField]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        [RequiredGet(InChildren = true)]
        private LaneItemSpawner _laneItemSpawner;
        [SerializeField]
        private float _speed;

        private static readonly int _stripColorKey = Shader.PropertyToID("_StripColor");
        private static readonly int _speedKey = Shader.PropertyToID("_Speed");

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

        [Button]
        private void SetVisualColor(Color color)
        {
            _meshRenderer.material.SetColor(_stripColorKey, color);
        }
    }
}
