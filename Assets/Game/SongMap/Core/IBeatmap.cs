namespace Beatmaps
{
	public interface IBeatmap
	{
		IDifficulty GetDifficulty();
		MapElement[] GetMapElements();
	}
}