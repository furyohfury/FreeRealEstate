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
            // Add3();
        }

        // [Button]
        // private void Add3()
        // {
        //     for (int i = 0; i < 3; i++)
        //     {
        //         AddScoreTableItem(i);
        //     }
        // }

        // [Button]
        // public void AddScoreTableItem(int id)
        // {
        //     _playerScoreTable.AddScore(id);
        // }
        //
        // [Button]
        // public void Move3()
        // {
        //     _playerScoreTable.SortItems(new PlayerViewData[]
        //                                 {
        //                                     new PlayerViewData(2, 0),
        //                                     new PlayerViewData(0, 1),
        //                                     new PlayerViewData(1, 2),
        //                                 });
        // }
        //
        // [Button]
        // public void Move4()
        // {
        //     _playerScoreTable.SortItems(new PlayerViewData[]
        //                                 {
        //                                     new PlayerViewData(2, 1),
        //                                     new PlayerViewData(0, 3),
        //                                     new PlayerViewData(1, 2),
        //                                     new PlayerViewData(3, 0)
        //                                 });
        // }
    }
}
