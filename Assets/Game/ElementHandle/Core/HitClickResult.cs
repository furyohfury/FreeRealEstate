namespace Game.ElementHandle
{
	public class HitClickResult : ClickResult
	{
		public float Offset;

		public HitClickResult(float offset)
		{
			Offset = offset;
		}
	}
}