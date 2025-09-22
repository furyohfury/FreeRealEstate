using System.Threading.Tasks;
using FirebaseSystem;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class FirebaseTests
{
	private FirebaseAuthenticator _firebaseAuthenticator;
	private FirebaseInitializer _firebaseInitializer;
	private readonly FirestoreTests _firestoreTests = new FirestoreTests();
	private const string TEST_MAIL = "test1234@gmail.com";
	private const string TEST_PASSWORD = "Ara123456";
	private const string TEST_DISPLAY_NAME = nameof(TEST_DISPLAY_NAME);

	[SetUp] [TearDown]
	public void Setup()
	{
		_firebaseInitializer = new FirebaseInitializer();
		_firebaseAuthenticator = new FirebaseAuthenticator();
	}

	[TearDown]
	public void TearDown()
	{
	}

	[Test]
	public void WhenDebug_And_ThenWriteInConsole()
	{
		// Arrange
		// Act
		Debug.Log("Test");
		// Assert
		Assert.IsTrue(true);
	}

	[Test]
	public async Task WhenInitialize_AndInInitializer_ThenAvailable()
	{
		// Arrange
		// Act
		var result = await _firebaseInitializer.Init();
		// Assert
		Assert.IsTrue(result);
	}

	[Test]
	public async Task WhenRegister_AndAccountDoesntExist_ThenCreateAccount()
	{
		// Arrange
		// Act
		var isRegistered = await _firebaseAuthenticator.Register(TEST_MAIL, TEST_PASSWORD, TEST_DISPLAY_NAME);
		// Assert
		Assert.IsTrue(isRegistered is AuthSuccess);
		await _firebaseAuthenticator.DeleteCurrentUser();
	}

	[Test]
	public async Task WhenRegister_AndAccountDoesntExist_ThenAccountHasRightData()
	{
		// Arrange
		// Act
		await _firebaseAuthenticator.Register(TEST_MAIL, TEST_PASSWORD, TEST_DISPLAY_NAME);
		// Assert
		Assert.IsTrue(_firebaseAuthenticator.TryGetCurrentUserEmail(out var email));
		Assert.IsTrue(_firebaseAuthenticator.TryGetCurrentUserDisplayName(out var displayName));
		Assert.IsTrue(email == TEST_MAIL);
		Assert.IsTrue(displayName == TEST_DISPLAY_NAME);
		await _firebaseAuthenticator.DeleteCurrentUser();
	}

	[Test]
	public async Task WhenRegister_AndAccountExists_ThenError()
	{
		// Arrange
		// Act
		var firstRegister = await _firebaseAuthenticator.Register(TEST_MAIL, TEST_PASSWORD, TEST_DISPLAY_NAME);
		var secondRegister = await _firebaseAuthenticator.Register(TEST_MAIL, TEST_PASSWORD, TEST_DISPLAY_NAME);
		// Assert
		Assert.IsTrue(firstRegister is AuthSuccess);
		Assert.IsTrue(secondRegister is AuthFailure);
		await _firebaseAuthenticator.DeleteCurrentUser();
	}

	[Test]
	public async Task WhenSignInWithRightPassword_AndAccountExists_ThenLogged()
	{
		// Arrange
		await _firebaseAuthenticator.Register(TEST_MAIL, TEST_PASSWORD, TEST_DISPLAY_NAME);
		// Act
		var signing = await _firebaseAuthenticator.SignIn(TEST_MAIL, TEST_PASSWORD);
		// Assert
		Assert.IsTrue(signing is AuthSuccess);
		await _firebaseAuthenticator.DeleteCurrentUser();
	}

	[Test]
	public async Task WhenSignIn_AndAccountDoesntExist_ThenError()
	{
		// Arrange
		// Act
		var signing = await _firebaseAuthenticator.SignIn("NonExistingMail123123@gmail.com", TEST_PASSWORD);
		// Assert
		Assert.IsTrue(signing is AuthFailure);
	}

	[Test]
	public async Task WhenSignIn_AndWrongPassword_ThenError()
	{
		// Arrange
		await _firebaseAuthenticator.Register(TEST_MAIL, TEST_PASSWORD, TEST_DISPLAY_NAME);
		// Act
		var signing = await _firebaseAuthenticator.SignIn(TEST_MAIL, "wrongpassword");
		// Assert
		Assert.IsTrue(signing is AuthFailure);
	}
}