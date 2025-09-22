namespace Game.UI
{
	public interface IMessageWindow : IWindow, IWindowClosable
	{
		void SetMessageText(string text);
	}
}