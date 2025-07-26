using R3;
using Beatmaps;

namespace Game
{
	public sealed class ActiveMapService
	{
		public ReadOnlyReactiveProperty<ISongMap> ActiveMap => _activeMap;
		private readonly ReactiveProperty<ISongMap> _activeMap = new();

		public void SetMap(ISongMap songMap)
		{
			_activeMap.Value = songMap;
		}
	}
}