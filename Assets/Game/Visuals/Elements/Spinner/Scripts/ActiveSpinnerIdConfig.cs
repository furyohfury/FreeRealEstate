using UnityEngine;

namespace Game.Visuals
{
	[CreateAssetMenu(fileName = nameof(ActiveSpinnerIdConfig), menuName = "PrefabIdConfig" + "/" + nameof(ActiveSpinnerIdConfig))]
	public sealed class ActiveSpinnerIdConfig : PrefabIdConfig<ActiveSpinnerView>
	{
	}
}