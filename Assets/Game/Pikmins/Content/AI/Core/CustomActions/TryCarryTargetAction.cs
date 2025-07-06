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
    [SerializeReference] public BlackboardVariable<GameObject> Entity;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        if (Entity.Value.TryGetComponent(out ICarry iCarry) == false)
        {
            return Status.Failure;
        }
        
        return iCarry.TryCarry(Target.Value)
            ? Status.Success
            : Status.Failure;
    }
}

