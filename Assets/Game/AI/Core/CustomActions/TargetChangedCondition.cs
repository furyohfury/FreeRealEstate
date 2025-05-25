using System;
using Game;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TargetChanged", story: "[Target] in [Self] has changed", category: "Conditions", id: "5dcf13eb9ea46f13ae114514ed706baa")]
public partial class TargetChangedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    public override bool IsTrue()
    {
        var targetComponent = Self.Value.GetComponent<IPikminTarget>();
        GameObject pikminTarget = targetComponent.Target;
        
        return pikminTarget != Target.Value;
    }
}
