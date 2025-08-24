using ObjectProvide;

namespace Game.Visuals
{
	public sealed class ActiveSpinnerViewFactory : PrefabFactory<ActiveSpinnerView>
	{
		public ActiveSpinnerViewFactory(
			IObjectProvider objectProvider,
			PrefabIdConfig<ActiveSpinnerView> prefabIdConfig
		) : base(objectProvider, prefabIdConfig)
		{
		}
	}
}