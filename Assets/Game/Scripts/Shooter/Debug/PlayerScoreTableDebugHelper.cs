using System;
using TriInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shooter
{
    public sealed class PlayerScoreTableDebugHelper : MonoBehaviour
    {
        [Inject]
        private PlayerScoreTable _playerScoreTable;

        private void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                AddScoreTableItem(i);
            }
        }

        [Button]
        public void AddScoreTableItem(int id)
        {
            _playerScoreTable.AddScore(id);
        }

        [Button]
        public void Move()
        {
            _playerScoreTable.SortItems(new PlayerViewData[]
                                        {
                                            new PlayerViewData(2, 0),
                                            new PlayerViewData(0, 1),
                                            new PlayerViewData(1, 2),
                                        });
        }
    }
}
