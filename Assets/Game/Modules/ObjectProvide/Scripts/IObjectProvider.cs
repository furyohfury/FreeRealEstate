using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace ObjectProvide
{
	public interface IObjectProvider
	{
		UniTask<T> Get<T>(string id) where T : Object;
	}
}