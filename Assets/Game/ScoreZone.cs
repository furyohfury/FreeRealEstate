using UnityEngine;

namespace Game
{
    public sealed class ScoreZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Item _))
            {
                Debug.Log("Scored");
                Destroy(other.gameObject);
            }
        }
    }
}
