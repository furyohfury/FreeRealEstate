using GameEngine;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Entity is null", story: "[Entity] is null", category: "Conditions", id: "56c36fb28c94659bab8104f37f13a7bc")]
public partial class EntityIsNullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;

    public override bool IsTrue()
    {
        return Entity.Value == null;
    }
}
