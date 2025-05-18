using System;
using GameEngine;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Entity] Attack Target", category: "Action", id: "af9429f905df7f3c26853c82c7ad5396")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;

    protected override Status OnStart()
    {
        var attackComponent = Entity.Value.GetComponent<AttackComponent>();
        attackComponent.Attack();
        return Status.Running;
    }
}

