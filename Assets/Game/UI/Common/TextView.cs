using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.UI
{
	public sealed class TextView : MonoBehaviour
	{
		[SerializeField] [Required]
		private TMP_Text _textField;

		public void SetText(string text)
		{
			_textField.SetText(text);
		}
	}
}