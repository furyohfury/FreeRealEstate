using UnityEngine;

namespace Game.Visuals
{
	public sealed class AddressablesReleaser : MonoBehaviour
	{
		private AddressablesPrefabFactory _factory;
		private string _id;

		public void Init(AddressablesPrefabFactory provider, string id)
		{
			_factory = provider;
			_id = id;
		}

		private void OnDestroy()
		{
			_factory.Return(_id);
		}
	}
}