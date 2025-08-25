using Beatmaps;

namespace Game.ElementHandle
{
	public sealed class DrumrollHitHandleResult : HandleResult
	{
		public DrumrollHitHandleResult(MapElement mapElement) : base(mapElement)
		{
		}

		public override ResultCompletionType GetCompletionType()
		{
			return ResultCompletionType.Continuous;
		}
	}
}