using System;
using Game;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AssignTargetToBlackboard", story: "Assign [Target] from [Self] to blackboard", category: "Action", id: "761167f0987812ee0157a011f15d690f")]
public partial class AssignTargetToBlackboardAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    protected override Status OnStart()
    {
        var targetValue = Self.Value.GetComponent<IPikminTarget>().Target;
        Target.Value = targetValue;
        Debug.Log("Assigned target");
        return Status.Success;
    }
}

