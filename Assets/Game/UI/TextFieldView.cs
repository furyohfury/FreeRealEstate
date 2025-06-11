using TMPro;
using UnityEngine;

namespace Game
{
	public sealed class TextFieldView : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _tmpText;

		public void SetText(string text)
		{
			_tmpText.SetText(text);
		}
	}
}