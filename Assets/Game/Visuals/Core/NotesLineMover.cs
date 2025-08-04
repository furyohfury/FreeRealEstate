using System.Collections.Generic;
using Game.Services;
using UnityEngine;
using VContainer.Unity;

namespace Game.Visuals
{
	public sealed class NotesLineMover : ITickable, IStartable
	{
		private readonly NotesLineBoundsService _notesLineBoundsService;
		private readonly List<ElementView> _elements = new(); // TODO check destroy
		private float _speed;

		public NotesLineMover(NotesLineBoundsService notesLineBoundsService)
		{
			_notesLineBoundsService = notesLineBoundsService;
		}

		public void Start()
		{
			var distance = Vector2.Distance(_notesLineBoundsService.HitPoint, _notesLineBoundsService.StartPoint);
			_speed = distance / NotesVisualData.SCROLL_TIME;
		}

		public void AddElement(ElementView view)
		{
			_elements.Add(view);
		}

		public void Tick()
		{
			for (int i = 0, count = _elements.Count; i < count; i++)
			{
				_elements[i].Move(_elements[i].GetPosition() - new Vector3(_speed * Time.deltaTime, 0, 0));
			}
		}
	}
}