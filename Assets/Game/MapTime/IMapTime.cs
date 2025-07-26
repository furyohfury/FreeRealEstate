namespace Game.SongMapTime
{
	public interface IMapTime
	{
		float GetMapTimeInSeconds();
		void AddTime(float seconds);
		void Reset();
	}
}