using UnityEngine;

namespace Game
{
    public sealed class ScoreZone : MonoBehaviour
    {
        [SerializeField]
        private ItemSystem _itemSystem;
        [SerializeField]
        private Lane _lane;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Item item))
            {
                Debug.Log("Scored");
                _lane.RemoveItem(item); // TODO if removes in another lane
                _itemSystem.DestroyItem(item);
            }
        }
    }
}
