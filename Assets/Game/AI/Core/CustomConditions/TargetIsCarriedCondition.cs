using GameEngine;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TargetIsCarried", story: "[Target] isCarried", category: "Conditions", id: "bc0973dc56a6ac6a1259ced7ccfcb041")]
public partial class TargetIsCarriedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public override bool IsTrue()
    {
        return Target.Value.GetComponent<CarriableComponent>().IsCarried;
    }
}
