using System;
using Game;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetTarget", story: "Get [Target] from [Self]", category: "Action", id: "761167f0987812ee0157a011f15d690f")]
public partial class GetTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        var targetValue = Self.Value.GetComponent<IPikminTarget>().Target;
        Target.Value = targetValue;
        return Status.Success;
    }
}

