using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetToNull", story: "Set [variable] to null", category: "Action", id: "250708188dabf601dff4838ea39fa8c1")]
public partial class SetToNullAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Variable;

    protected override Status OnStart()
    {
        Variable.Value = null;
        return Status.Success;
    }
}

