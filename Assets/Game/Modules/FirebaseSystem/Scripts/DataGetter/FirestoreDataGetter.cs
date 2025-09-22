using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

namespace FirebaseSystem
{
	public sealed class FirestoreDataGetter
	{
		private readonly FirebaseFirestore _database = FirebaseFirestore.DefaultInstance;

		public async UniTask<IGetDataResult> GetDocumentsByQuery<T>(string collection, params Func<Query, Query>[] queries)
		{
			CollectionReference collectionReference = _database.Collection(collection);
			Query query = collectionReference;
			for (int i = 0, count = queries.Length; i < count; i++)
			{
				query = queries[i](query);
			}

			try
			{
				QuerySnapshot snapshot = await query.GetSnapshotAsync();
				if (snapshot.Documents.Any() == false)
				{
					return new NullFirestoreResult();
				}

				IEnumerable<DocumentSnapshot> documentsSnapshots = snapshot.Documents;
				List<T> documentsData = new();
				foreach (var documentSnapshot in documentsSnapshots)
				{
					documentsData.Add(documentSnapshot.ConvertTo<T>());
				}

				return new SuccessGetDataResult<T>(documentsData);
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

		public async UniTask<IGetDataResult> GetDocumentsByQuery<T>(
			string collection,
			string document,
			string subCollection,
			params Func<Query, Query>[] queries
			)
		{
			CollectionReference collectionRef = _database.Collection(collection);
			var documentRef = collectionRef.Document(document);
			var subCollectionRef = documentRef.Collection(subCollection);
			Query query = subCollectionRef;
			for (int i = 0, count = queries.Length; i < count; i++)
			{
				query = queries[i](query);
			}

			try
			{
				QuerySnapshot snapshot = await query.GetSnapshotAsync();
				if (snapshot.Documents.Any() == false)
				{
					return new NullFirestoreResult();
				}

				IEnumerable<DocumentSnapshot> documentsSnapshots = snapshot.Documents;
				List<T> documentsData = new();
				foreach (var documentSnapshot in documentsSnapshots)
				{
					documentsData.Add(documentSnapshot.ConvertTo<T>());
				}

				return new SuccessGetDataResult<T>(documentsData);
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

		public async UniTask<IGetDataResult> GetDocumentData<T>(string collection, string document)
		{
			var documentReference = _database.Collection(collection).Document(document);
			try
			{
				DocumentSnapshot snapshot = await documentReference.GetSnapshotAsync();
				T data = snapshot.ConvertTo<T>();

				return new SuccessGetDataResult<T>(new List<T>() { data });
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

		public async UniTask<IGetValueResult> GetDataValue<T>(string collection, string document, string path)
		{
			var documentReference = _database.Collection(collection).Document(document);
			try
			{
				var snapshot = await documentReference.GetSnapshotAsync();
				Dictionary<string, object> dictionary = snapshot.ToDictionary();
				if (dictionary.TryGetValue(path, out object value))
				{
					return new SuccessGetValueResult<T>((T)value);
				}
				else
				{
					return new FailedFirestoreResult(FirestoreError.InvalidArgument,
						FirestoreUtils.GetUserFriendlyMessage(FirestoreError.InvalidArgument)
						);
				}
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