using UnityEngine;

namespace Game.Visuals
{
	[CreateAssetMenu(fileName = "SpinnerPrefabConfig", menuName = "Visuals/PrefabConfig/SpinnerPrefabConfig")]
	public sealed class SpinnerPrefabConfig : ScriptableObject
	{
		public string SpinnerViewId => _spinnerViewId;
		[SerializeField]
		private string _spinnerViewId;
	}
}