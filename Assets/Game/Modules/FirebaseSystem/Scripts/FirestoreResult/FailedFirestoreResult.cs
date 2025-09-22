using Firebase.Firestore;

namespace FirebaseSystem
{
	public sealed class FailedFirestoreResult : ISendDataResult, IGetDataResult, IGetValueResult
	{
		public FirestoreError Error;
		public string Message;

		public FailedFirestoreResult(FirestoreError error, string message)
		{
			Error = error;
			Message = message;
		}
	}
}