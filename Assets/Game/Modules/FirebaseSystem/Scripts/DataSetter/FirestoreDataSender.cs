using System;
using Cysharp.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

namespace FirebaseSystem
{
	public sealed class FirestoreDataSender
	{
		private readonly FirebaseFirestore _database = FirebaseFirestore.DefaultInstance;

		public async UniTask<ISendDataResult> SendData<T>(string collection, string document, T data)
		{
			var collectionRef = _database.Collection(collection);
			var documentRef = collectionRef.Document(document);
			try
			{
				await documentRef.SetAsync(data, SetOptions.MergeAll);
				return new SuccessSendDataResult();
			}
			catch (FirestoreException ex)
			{
				FirestoreError errorCode = ex.ErrorCode;
				return new FailedFirestoreResult(errorCode, FirestoreUtils.GetUserFriendlyMessage(errorCode));
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return new NullFirestoreResult();
			}
		}

		public async UniTask<ISendDataResult> SendNestedData<T>(
			string collection,
			string document,
			string subCollection,
			string subDocument,
			T data)
		{
			var collectionRef = _database.Collection(collection);
			var documentRef = collectionRef.Document(document);
			var subCollectionRef = documentRef.Collection(subCollection);
			var subDocumentRef = subCollectionRef.Document(subDocument);
			try
			{
				await subDocumentRef.SetAsync(data, SetOptions.MergeAll);
				return new SuccessSendDataResult();
			}
			catch (FirestoreException ex)
			{
				FirestoreError errorCode = ex.ErrorCode;
				return new FailedFirestoreResult(errorCode, FirestoreUtils.GetUserFriendlyMessage(errorCode));
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return new NullFirestoreResult();
			}
		}
	}
}