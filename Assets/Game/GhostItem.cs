using UnityEngine;

namespace Game
{
    public sealed class GhostItem : MonoBehaviour
    {
        public void SetColor(Color ghostItemColor)
        {
            GetComponentInChildren<MeshRenderer>().material.color = ghostItemColor;
        }

        public void Destroy()
        {
            // TODO VFX dissolve mb and destroy
            Destroy(gameObject);
        }
    }
}
