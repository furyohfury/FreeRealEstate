using GameEngine;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TargetIsAlive", story: "[Target] is alive", category: "Conditions", id: "7b95d07f6a85c275e8dd2e012cb5661b")]
public partial class TargetIsAliveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Entity> Target;

    public override bool IsTrue()
    {
        if (Target.Value.TryGetComponent(out LifeComponent lifeComponent) == false)
        {
            return false;
        }

        var isAlive = !lifeComponent.IsDead;
        return isAlive;
    }
}
