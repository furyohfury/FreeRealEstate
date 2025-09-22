using System.Collections.Generic;

namespace FirebaseSystem
{
	public sealed class SuccessGetDataResult<T> : IGetDataResult
	{
		public readonly List<T> Data;

		public SuccessGetDataResult(List<T> data)
		{
			Data = data;
		}
	}
}