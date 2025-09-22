namespace Game.Meta.Authentication
{
	public class ErrorAuthResult : IAuthResult
	{
		public string Error { get; private set; }

		public ErrorAuthResult(string error)
		{
			Error = error;
		}
	}
}