using System;
using System.Collections.Generic;
using Game.UI.Leaderboards;
using Game.UI.Loading;
using Game.Visuals;

namespace Game.UI
{
	public static class WindowPrefabsLibrary
	{
		private static readonly Dictionary<Type, string> _prefabIDs = new()
		                                                              {
			                                                              { typeof(ILeaderboardWindowView), PrefabsStaticNames.LEADERBOARD_WINDOW }
			                                                              , { typeof(ILeaderboardRecordView), PrefabsStaticNames.LEADERBOARD_RECORD }
			                                                              , { typeof(IDimLoadingScreen), PrefabsStaticNames.DIM_LOADING_SCREEN }
			                                                              , { typeof(IMessageWindow), PrefabsStaticNames.MESSAGE_POPUP }
		                                                              };

		public static bool TryGetPrefabId<T>(out string prefabId)
		{
			var type = typeof(T);
			return _prefabIDs.TryGetValue(type, out prefabId);
		}
		
		public static bool TryGetPrefabId(Type type, out string prefabId)
		{
			return _prefabIDs.TryGetValue(type, out prefabId);
		}
	}
}