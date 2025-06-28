using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsRotatedToTarget", story: "[Self] is rotated to [Target] within [Threshold]", category: "Conditions", id: "36fb6c52bad2a6595bbbc05157d1d59e")]
public partial class IsRotatedToTargetCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Threshold;

    public override bool IsTrue()
    {
        var angle = Vector3.Angle(Self.Value.forward, Target.Value.forward);
        return angle <= Threshold.Value;
    }
}
