using System;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	[RequireComponent(typeof(Button))]
	public sealed class ButtonView : MonoBehaviour, IButtonView
	{
		public Observable<Unit> OnButtonPressed { get; private set; }

		[SerializeField][Required]
		private Button _button;

		private void Awake()
		{
			OnButtonPressed = _button.OnClickAsObservable();
		}
	}
}