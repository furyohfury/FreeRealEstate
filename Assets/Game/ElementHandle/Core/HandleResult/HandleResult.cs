using Beatmaps;

namespace Game.ElementHandle
{
	public abstract class HandleResult
	{
		public MapElement Element => _mapElement;
		private readonly MapElement _mapElement;

		protected HandleResult(MapElement mapElement)
		{
			_mapElement = mapElement;
		}

		public virtual JudgementResult JudgeResult(JudgementSettings settings)
		{
			return settings.GetDefaultScore();
		}

		public virtual ResultCompletionType GetCompletionType()
		{
			return ResultCompletionType.Final;
		}

		public enum ResultCompletionType
		{
			None
			, Final
			, Continuous
		}
	}
}