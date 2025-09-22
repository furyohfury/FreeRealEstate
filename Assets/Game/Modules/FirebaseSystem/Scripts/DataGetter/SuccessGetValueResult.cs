namespace FirebaseSystem
{
	public sealed class SuccessGetValueResult<T> : IGetValueResult
	{
		public T Value;

		public SuccessGetValueResult(T value)
		{
			Value = value;
		}
	}
}