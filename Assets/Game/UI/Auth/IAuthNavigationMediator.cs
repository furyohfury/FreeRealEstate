using Cysharp.Threading.Tasks;

namespace Game.UI
{
	public interface IAuthNavigationMediator
	{
		UniTask ShowOptions();
		UniTask ShowLoginWindow();
		UniTask ShowRegisterWindow();
	}
}