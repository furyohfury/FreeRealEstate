using System.Collections.Generic;
using Beatmaps;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Visuals
{
	[CreateAssetMenu(fileName = "SingleNotePrefabConfig", menuName = "Visuals/SingleNotePrefabConfig")]
	public sealed class SingleNotePrefabConfig : SerializedScriptableObject
	{
		public IReadOnlyDictionary<Notes, SingleNoteView> Views => _views;
		[SerializeField]
		private Dictionary<Notes, SingleNoteView> _views;
	}
}