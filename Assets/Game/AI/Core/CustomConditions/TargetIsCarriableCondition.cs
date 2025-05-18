using GameEngine;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TargetIsCarriable", story: "[Target] is carriable", category: "Conditions", id: "91e6b681367ae58765857c75694035d2")]
public partial class TargetIsCarriableCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Entity> Target;

    public override bool IsTrue()
    {
        return Target.Value.HasComponent<CarriableComponent>();
    }
}
