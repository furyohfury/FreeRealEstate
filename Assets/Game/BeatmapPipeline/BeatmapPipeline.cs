using Beatmaps;
using R3;

namespace Game.BeatmapControl
{
	public sealed class BeatmapPipeline
	{
		public bool IsEnded => _activeIndex >= _activeMapElements?.Length;
		public Observable<MapElement> Element => _element;
		public ReadOnlyReactiveProperty<IBeatmap> Map => _map;

		private readonly ReactiveProperty<MapElement> _element = new();
		private readonly ReactiveProperty<IBeatmap> _map = new();
		private MapElement[] _activeMapElements;
		private int _activeIndex = 0;

		public void SwitchToNextElement()
		{
			_activeIndex++;
			UpdateElement();
		}

		public void SetMap(IBeatmap beatmap)
		{
			_map.Value = beatmap;
			_activeMapElements = beatmap.GetMapElements();
			_activeIndex = 0;
			UpdateElement();
		}

		public void RestartMap()
		{
			_activeIndex = 0;
			UpdateElement();
		}

		private void UpdateElement()
		{
			if (_activeIndex < _activeMapElements.Length)
			{
				_element.Value = _activeMapElements[_activeIndex];
			}
		}
	}
}