using System;
using GameEngine;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

[Serializable] [GeneratePropertyBag]
[Condition("EntityIsDead", story: "[Target] is dead", category: "Conditions", id: "01e48c5f3496e798c887925061dbd111")]
public partial class EntityIsDeadCondition : Condition
{
	[SerializeReference]
	public BlackboardVariable<GameObject> Target;

	public override bool IsTrue()
	{
		if (Target.Value.TryGetComponent(out IHitPoints hitPoints) == false)
		{
			return false;
		}

		return hitPoints.HitPoints <= 0;
	}
}