using R3;

namespace Game
{
	public sealed class ShipPoints
	{
		public readonly ReactiveProperty<int> Points = new(0);
	}
}