using Beatmaps;

namespace Game.ElementHandle
{
	public class NoneHandleResult : HandleResult
	{
		public NoneHandleResult(MapElement mapElement) : base(mapElement)
		{
		}

		public override ResultCompletionType GetCompletionType()
		{
			return ResultCompletionType.None;
		}
	}
}