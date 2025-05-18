using GameEngine;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TryCarryTarget", story: "[Entity] try carry [Target]", category: "Action", id: "96c183095df3d31809d63b3bb93b61fc")]
public partial class TryCarryTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;
    [SerializeReference] public BlackboardVariable<Entity> Target;

    protected override Status OnStart()
    {
        var carryComponent = Entity.Value.GetComponent<CarryComponent>();
        if (carryComponent.TryCarry(Target.Value))
        {
            return Status.Success;
        }
        
        return Status.Failure;
    }
}

