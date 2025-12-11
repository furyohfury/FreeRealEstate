using UnityEngine;

namespace Gameplay
{
	[RequireComponent(typeof(BoxCollider))]
	public sealed class PlayzoneCollider : MonoBehaviour
	{
		public BoxCollider BoxCollider;
		public Player Player;
	}
}
