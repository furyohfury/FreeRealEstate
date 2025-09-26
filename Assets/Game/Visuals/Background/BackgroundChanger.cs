using UnityEngine;

namespace Game.Visuals
{
	public sealed class BackgroundChanger : IBackgroundChanger
	{
		private readonly ConstantSizePicture _constantSizePicture;

		public BackgroundChanger(ConstantSizePicture constantSizePicture)
		{
			_constantSizePicture = constantSizePicture;
		}

		public void Change(Sprite sprite)
		{
			if (sprite != null)
			{
				_constantSizePicture.SetImage(sprite);
			}
		}
	}
}