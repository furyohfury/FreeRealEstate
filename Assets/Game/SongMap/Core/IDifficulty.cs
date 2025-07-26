namespace Beatmaps
{
	public interface IDifficulty
	{
		string GetName();
		IDifficultyParams[] GetDifficultyParams();
	}
}