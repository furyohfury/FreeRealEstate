using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace ObjectProvide.Tests
{
	[TestFixture]
	public class AddressablesTest
	{
		private const string TESTPREFAB = "testprefab";
		private const string TESTICON = "testicon";

		[Test]
		public void ConsoleTest()
		{
			Debug.Log("Test");
		}

		[Test]
		public async Task WhenLoad_AndNoDublicates_ThenLoadedAsset()
		{
			// Arrange
			var loader = new AddressablesObjectProvider();
			// Act
			Sprite icon = await loader.Get<Sprite>(TESTICON);
			// Assert
			Assert.IsTrue(icon != null);
		}

		[Test]
		public async Task WhenLoad_AndLoadAgain_ThenLoadedOnce()
		{
			// Arrange
			var loader = new AddressablesObjectProvider();
			// Act
			Sprite icon = await loader.Get<Sprite>(TESTICON);
			Sprite secondIcon = await loader.Get<Sprite>(TESTICON);
			// Assert
			Assert.IsTrue(icon != null);
			Assert.IsTrue(secondIcon != null);
			Assert.IsTrue(secondIcon == icon);
		}

		[Test]
		public async Task WhenLoad_AndLoadAtSameTime_ThenLoadedOnce()
		{
			// Arrange
			var loader = new AddressablesObjectProvider();
			// Act
			var iconTask = loader.Get<Sprite>(TESTICON);
			var secondIconTask = loader.Get<Sprite>(TESTICON);
			var compositeTask = await UniTask.WhenAll(iconTask, secondIconTask);
			var icon = compositeTask.Item1;
			var secondIcon = compositeTask.Item2;
			// Assert
			Assert.IsTrue(icon != null);
			Assert.IsTrue(secondIcon != null);
			Assert.IsTrue(secondIcon == icon);
		}
	}
}