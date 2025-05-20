using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Debug
{
	public sealed class ShipPointsDebug : MonoBehaviour
	{
		[ShowInInspector] [ReadOnly]
		private int Points => _shipPoints.Points.Value;
		
		private ShipPoints _shipPoints;

		[Inject]
		public void Construct(ShipPoints shipPoints)
		{
			_shipPoints = shipPoints;
		}

		[Button]
		private void Print()
		{
			UnityEngine.Debug.Log($"points = {_shipPoints.Points.Value}");
		}
	}
}