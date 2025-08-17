using UnityEngine;

namespace Game.Services
{
	public sealed class ScreenBeatmapBoundsService
	{
		public Vector2 StartPoint => _startPoint.position;
		public Vector2 HitPoint => _hitPoint.position;
		private readonly Transform _startPoint;
		private readonly Transform _hitPoint;

		public ScreenBeatmapBoundsService(Transform startPoint, Transform hitPoint)
		{
			_startPoint = startPoint;
			_hitPoint = hitPoint;
		}

		public float GetNoteLineDistance()
		{
			return Vector2.Distance(HitPoint, StartPoint);
		}
	}
}