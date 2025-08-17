using UnityEngine;

namespace Game.Visuals
{
	public sealed class SingleNoteView : ElementView
	{
		public float Alpha
		{
			get => _spriteRenderer.color.a;
			set
			{
				var color = _spriteRenderer.color;
				color.a = value;
				_spriteRenderer.color = color;
			}
		}
		[SerializeField]
		private SpriteRenderer _spriteRenderer;
	}
}