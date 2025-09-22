using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Loading
{
	public sealed class DimLoadingScreen : MonoBehaviour, IDimLoadingScreen
	{
		[SerializeField]
		private Image _arrowsImage;
		[SerializeField]
		private float _speed;

		public void Close()
		{
			Destroy(gameObject);
		}

		private void Update()
		{
			_arrowsImage.transform.Rotate(new Vector3(0, 0, _speed * Time.deltaTime));
		}
	}
}