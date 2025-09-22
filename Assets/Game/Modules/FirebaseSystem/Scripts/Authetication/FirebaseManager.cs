using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Firebase.Firestore;

namespace FirebaseSystem
{
	public sealed class FirebaseManager
	{
		private readonly FirebaseAuthenticator _firebaseAuthenticator = new();

		private readonly FirestoreDataSender _firestoreDataSender = new();

		private readonly FirestoreDataGetter _firestoreDataGetter = new();

		public bool TryGetCurrentUser(out FirebaseUser user)
		{
			return _firebaseAuthenticator.TryGetCurrentUser(out user);
		}

		public bool TryGetCurrentUserEmail(out string email)
		{
			return _firebaseAuthenticator.TryGetCurrentUserEmail(out email);
		}

		public bool TryGetCurrentUserDisplayName(out string displayName)
		{
			return _firebaseAuthenticator.TryGetCurrentUserDisplayName(out displayName);
		}

		public UniTask<IAuthResult> Register(string mail, string password, string displayName)
		{
			return _firebaseAuthenticator.Register(mail, password, displayName);
		}

		public UniTask<IAuthResult> SignIn(string mail, string password)
		{
			return _firebaseAuthenticator.SignIn(mail, password);
		}

		public UniTask<bool> DeleteCurrentUser()
		{
			return _firebaseAuthenticator.DeleteCurrentUser();
		}

		public UniTask<ISendDataResult> SendData<T>(string collection, string document, T data)
		{
			return _firestoreDataSender.SendData(collection, document, data);
		}

		public UniTask<ISendDataResult> SendNestedData<T>(string collection, string document, string subCollection, string subDocument, T data)
		{
			return _firestoreDataSender.SendNestedData(collection, document, subCollection, subDocument, data);
		}

		public UniTask<IGetDataResult> GetDocumentsByQuery<T>(string collection, params Func<Query, Query>[] queries)
		{
			return _firestoreDataGetter.GetDocumentsByQuery<T>(collection, queries);
		}

		public UniTask<IGetDataResult> GetDocumentsByQuery<T>(string collection, string document, string subCollection, params Func<Query, Query>[] queries)
		{
			return _firestoreDataGetter.GetDocumentsByQuery<T>(collection, document, subCollection, queries);
		}

		public UniTask<IGetDataResult> GetDocumentData<T>(string collection, string document)
		{
			return _firestoreDataGetter.GetDocumentData<T>(collection, document);
		}

		public UniTask<IGetValueResult> GetDataValue<T>(string collection, string document, string path)
		{
			return _firestoreDataGetter.GetDataValue<T>(collection, document, path);
		}
	}
}