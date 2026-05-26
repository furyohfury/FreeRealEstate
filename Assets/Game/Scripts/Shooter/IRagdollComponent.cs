using UnityEngine;

namespace Game.Scripts.Shooter
{
    public interface IRagdollComponent
    {
        Collider GetCollider(string boneId);
    }
}
