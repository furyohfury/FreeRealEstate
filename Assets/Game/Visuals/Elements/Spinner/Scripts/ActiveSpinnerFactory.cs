using ObjectProvide;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerFactory : PrefabFactory<ActiveSpinnerView>
	{
		public ActiveSpinnerFactory(IObjectProvider objectProvider, PrefabIdConfig<ActiveSpinnerView> prefabIdConfig) : base(objectProvider
			, prefabIdConfig)
		{
		}
	}
}