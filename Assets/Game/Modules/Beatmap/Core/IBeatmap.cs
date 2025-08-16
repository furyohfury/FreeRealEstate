namespace Beatmaps
{
	public interface IBeatmap
	{
		int GetBpm();
		IDifficulty GetDifficulty();
		MapElement[] GetMapElements();
	}
}