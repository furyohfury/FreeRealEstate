using System;
using GameEngine;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stop carry", story: "[Self] Stop Carry", category: "Action", id: "8916bd39c0c93e03bb0c034b6bf1685d")]
public partial class StopCarryAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    
    protected override Status OnStart()
    {
        if (Self.Value.TryGetComponent(out ICarry iCarry))
        {
            iCarry.StopCarry();
        }
        
        return Status.Success;
    }
}

