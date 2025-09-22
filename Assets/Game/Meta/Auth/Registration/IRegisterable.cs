using Cysharp.Threading.Tasks;

namespace Game.Meta.Authentication
{
	public interface IRegisterable
	{
		UniTask<IAuthResult> Register(string email, string password, string nickname);
	}
}