using System.Collections.Generic;
using Beatmaps;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Visuals
{
	[CreateAssetMenu(fileName = "SingleNotePrefabConfig", menuName = "Visuals/PrefabConfig/SingleNotePrefabConfig")]
	public sealed class SingleNotePrefabConfig : SerializedScriptableObject
	{
		public IReadOnlyDictionary<Notes, string> ViewIds => _viewIds;
		[SerializeField]
		private Dictionary<Notes, string> _viewIds;
	}
}