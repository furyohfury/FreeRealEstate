using TMPro;
using UnityEngine;

namespace Game.UI
{
	public sealed class ScrollTransitionText : MonoBehaviour, ITextView
	{
		[SerializeField]
		private TMP_Text _text;
		
		public void SetText(string text)
		{
			_text.SetText(text); // TODO make number scroll from below
		}
	}
}