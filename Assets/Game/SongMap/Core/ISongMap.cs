namespace Beatmaps
{
	public interface ISongMap
	{
		IDifficulty GetDifficulty();
		MapElement[] GetMapElements();
	}
}