using Beatmaps;

namespace Game.ElementHandle
{
	public class NoteHitHandleResult : HandleResult
	{
		public float Offset;

		public NoteHitHandleResult(MapElement mapElement, float offset) : base(mapElement)
		{
			Offset = offset;
		}
	}
}