using UnityEngine;

namespace Game.Services
{
	public sealed class NotesLineBoundsService
	{
		public Vector2 StartPoint => _startPoint.position;
		public Vector2 HitPoint => _hitPoint.position;
		public Transform NotesContainer => _notesContainer;
		private readonly Transform _startPoint;
		private readonly Transform _hitPoint;
		private readonly Transform _notesContainer;

		public NotesLineBoundsService(Transform startPoint, Transform hitPoint, Transform notesContainer)
		{
			_startPoint = startPoint;
			_hitPoint = hitPoint;
			_notesContainer = notesContainer;
		}

		public float GetNoteLineDistance()
		{
			return Vector2.Distance(HitPoint, StartPoint);
		}
	}
}