using UnityEngine;

namespace Game.Visuals
{
	public sealed class DrumrollNoteView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;

		public Vector3 GetPosition()
		{
			return transform.position;
		}

		public void Move(Vector3 pos)
		{
			transform.position = pos;
		}

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

		public void DestroyView()
		{
			Destroy(gameObject);
		}
	}
}