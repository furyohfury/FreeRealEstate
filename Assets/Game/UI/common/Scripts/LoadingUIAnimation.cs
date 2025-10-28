using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public sealed class LoadingUIAnimation : MonoBehaviour
	{
		[SerializeField]
		private Image _arrowsImage;
		[SerializeField]
		private float _speed = 150f;
		
		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		private void Update()
		{
			_arrowsImage.transform.Rotate(new Vector3(0, 0, _speed * Time.deltaTime));
		}
	}
}