using Beatmaps;

namespace Game.ElementHandle
{
	public class NoteHitHandleResult : HandleResult
	{
		public float Offset => _offset;
		public float ClickWindow => _clickWindow;

		private readonly float _offset;
		private readonly float _clickWindow;

		public NoteHitHandleResult(MapElement mapElement, float offset, float clickWindow) : base(mapElement)
		{
			_offset = offset;
			_clickWindow = clickWindow;
		}

		public override JudgementResult JudgeResult(JudgementSettings settings)
		{
			return settings.GetScore(_offset, _clickWindow);
		}
	}
}