using UnityEngine;
using UnityEngine.Serialization;

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
                _lane.RemoveItem(item);
                _itemSystem.DestroyItem(item);
            }
        }
    }
}
