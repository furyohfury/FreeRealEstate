using Beatmaps;

namespace Game.ElementHandle
{
	public sealed class SpinnerRunningHandleResult : HandleResult
	{
		public SpinnerRunningHandleResult(MapElement mapElement) : base(mapElement)
		{
		}
		
		public override ResultCompletionType GetCompletionType()
		{
			return ResultCompletionType.Continuous;
		}
	}
}