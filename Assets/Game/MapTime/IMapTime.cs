namespace Game.BeatmapTime
{
	public interface IMapTime
	{
		void Launch();

		void Pause();

		void Resume();

		void Reset();

		void Tick(float deltaTime);

		float GetMapTimeInSeconds();
	}
}