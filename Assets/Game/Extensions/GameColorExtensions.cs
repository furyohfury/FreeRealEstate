using System;
using UnityEngine;

namespace Game.Extensions
{
    public static class GameColorExtensions
    {
        public static Color ToColor(this GameColor color)
        {
            return color switch
            {
                GameColor.Red => Color.red,
                GameColor.Green => Color.green,
                GameColor.Purple => Color.purple,
                GameColor.Blue => Color.blue,
                GameColor.Yellow => Color.yellow,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }
    }
}
