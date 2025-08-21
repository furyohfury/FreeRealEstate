using Beatmaps;

namespace Game.ElementHandle
{
	public class MissHandleResult : HandleResult
	{
		public MissHandleResult(MapElement mapElement) : base(mapElement)
		{
		}

		public override JudgementResult JudgeResult(JudgementSettings settings)
		{
			return new JudgementResult(JudgementType.Miss);
		}
	}
}