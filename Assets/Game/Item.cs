using UnityEngine;

namespace Game
{
    public sealed class Item : MonoBehaviour
    {
        public void Move(Vector3 v)
        {
            transform.position += v;
        }
    }
}
