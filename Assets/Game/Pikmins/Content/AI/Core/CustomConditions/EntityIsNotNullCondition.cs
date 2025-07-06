using GameEngine;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Entity is not null", story: "[Entity] is not null", category: "Conditions", id: "8586cf5a16ddac34ea9a2c5b9ee93a03")]
public partial class EntityIsNotNullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Entity;

    public override bool IsTrue()
    {
        return Entity.Value != null;
    }
}
