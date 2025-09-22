using Cysharp.Threading.Tasks;

namespace Game.UI
{
	public interface IWindowSystem
	{
		UniTask<T> Spawn<T>(int priority) where T : IWindow;
		void Close(IWindow window);
		void CloseAll();
	}
}