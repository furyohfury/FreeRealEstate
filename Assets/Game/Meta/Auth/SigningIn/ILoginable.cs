using Cysharp.Threading.Tasks;

namespace Game.Meta.Authentication
{
	public interface ILoginable
	{
		UniTask<IAuthResult> Login(string email, string password);
	}
}