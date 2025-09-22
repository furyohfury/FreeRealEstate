using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using FirebaseSystem;
using NUnit.Framework;

public class FirestoreTests
{
	private FirestoreDataSender _firestoreDataSender;
	private FirestoreDataGetter _firestoreDataGetter;

	private const string TEST_MAIL = "test@gmail.com";
	private const string TESTCOLLECTION = "TestCollection";
	private const string TEST_DOCUMENT = "UserInfo";

	[SetUp]
	public void Setup()
	{
		_firestoreDataSender = new FirestoreDataSender();
		_firestoreDataGetter = new FirestoreDataGetter();
	}

	[Test]
	public async Task WhenUpdateData_AndHasSameFields_ThenDataUpdated()
	{
		// Arrange
		var data = new Dictionary<string, object>()
		           {
			           { "Id", "TestUser" }, { "email", TEST_MAIL }
		           };
		// Act
		await _firestoreDataSender.SendData(TESTCOLLECTION, TEST_DOCUMENT, data);
		IGetValueResult idResult = await _firestoreDataGetter.GetDataValue<string>(TESTCOLLECTION, TEST_DOCUMENT, "Id");
		IGetValueResult emailResult = await _firestoreDataGetter.GetDataValue<string>(TESTCOLLECTION, TEST_DOCUMENT, "email");
		// Assert
		Assert.IsTrue(idResult is SuccessGetValueResult<string> idValueResult
		              && "TestUser" == idValueResult.Value);
		Assert.IsTrue(emailResult is SuccessGetValueResult<string> emailValueResult
		              && TEST_MAIL == emailValueResult.Value);
	}

	// [Test]
	// public async Task WhenUpdateData_AndDocumentDoesntExist_ThenThrows()
	// {
	// 	// Arrange
	// 	var db = FirebaseFirestore.DefaultInstance;
	// 	var collection = db.Collection(TESTCOLLECTION);
	// 	var document = collection.Document("NewDocument");
	// 	var data = new Dictionary<string, object>()
	// 	           {
	// 		           { "Id", "TestUser" }
	// 	           };
	// 	Exception ex = null;
	// 	// Act
	// 	try
	// 	{
	// 		await document.UpdateAsync(data);
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		ex = e;
	// 		Debug.Log(e);
	// 	}
	//
	// 	// Assert
	// 	Assert.IsFalse(ex == null);
	// 	Assert.IsFalse(ex is OperationCanceledException);
	// 	Assert.IsTrue(ex is FirestoreException);
	// }

	[Test]
	public async Task WhenAddTwoDocumentsWithSameField_AndQueryThisField_ThenGetTwoDocuments()
	{
		// Arrange
		var firstDocumentName = "NewDocument1";
		var secondDocumentName = "NewDocument2";
		var thirdDocumentName = "NewDocument3";
		var sameField = "Field";
		var sameValue = "sameValue";
		var anotherValue = "anotherValue";
		var sameData = new Dictionary<string, object>()
		               {
			               { sameField, sameValue }
		               };

		var anotherData = new Dictionary<string, object>()
		                  {
			                  { sameField, anotherValue }
		                  };
		// Act
		await _firestoreDataSender.SendData(TESTCOLLECTION, firstDocumentName, sameData);
		await _firestoreDataSender.SendData(TESTCOLLECTION, secondDocumentName, sameData);
		await _firestoreDataSender.SendData(TESTCOLLECTION, thirdDocumentName, anotherData);
		var result = await _firestoreDataGetter.GetDocumentsByQuery<Dictionary<string, object>>(
			TESTCOLLECTION,
			query => query.WhereEqualTo(sameField, sameValue)
			);

		// Assert
		Assert.IsTrue(result is SuccessGetDataResult<List<Dictionary<string, object>>> success
		              && success.Data.Count == 2);
	}

	[Test]
	public async Task WhenAddTwoDocumentsWithSameTwoFields_AndQueryThoseFields_ThenGetTwoDocuments()
	{
		// Arrange
		var firstDocumentName = "NewDocument1";
		var secondDocumentName = "NewDocument2";
		var thirdDocumentName = "NewDocument3";
		var firstfieldName = "FirstField";
		var secondfieldName = "SecondField";
		var sameFieldValue = "SameFieldValue";
		var sameData = new Dictionary<string, object>()
		               {
			               { firstfieldName, sameFieldValue }, { secondfieldName, sameFieldValue }
		               };
		var anotherData = new Dictionary<string, object>()
		                  {
			                  { firstfieldName, sameFieldValue }, { secondfieldName, "AnotherSecondFieldValue" }
		                  };
		// Act
		await _firestoreDataSender.SendData(TESTCOLLECTION, firstDocumentName, sameData);
		await _firestoreDataSender.SendData(TESTCOLLECTION, secondDocumentName, sameData);
		await _firestoreDataSender.SendData(TESTCOLLECTION, thirdDocumentName, anotherData);
		var result = await _firestoreDataGetter.GetDocumentsByQuery<Dictionary<string, object>>(
			TESTCOLLECTION,
			query => query.WhereEqualTo(firstfieldName, sameFieldValue),
			query => query.WhereEqualTo(secondfieldName, sameFieldValue)
			);

		// Assert
		Assert.IsTrue(result is SuccessGetDataResult<List<Dictionary<string, object>>> success
			&& success.Data.Count == 2);
	}

	[Test]
	public async Task WhenAddCustomType_AndGetItAfer_ThenGetValidData()
	{
		// Arrange
		int testNumber = 1;
		var testId = "TestUserId";
		var testStruct = new TestStruct { Number = testNumber, Id = testId };
		// Act
		var sendDataResult = await _firestoreDataSender.SendData(TESTCOLLECTION, TEST_DOCUMENT, testStruct);
		var getDataResult = await _firestoreDataGetter.GetDocumentData<TestStruct>(TESTCOLLECTION, TEST_DOCUMENT);
		// Assert
		Assert.IsTrue(sendDataResult is SuccessSendDataResult);
		Assert.IsTrue(getDataResult is SuccessGetDataResult<TestStruct> successGetDataResult
		              && successGetDataResult.Data.Equals(testStruct));
	}

	[FirestoreData]
	public struct TestStruct
	{
		[FirestoreProperty]
		public int Number { get; set; }

		[FirestoreProperty]
		public string Id { get; set; }
	}
}