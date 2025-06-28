using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

[Serializable] [GeneratePropertyBag]
[Condition("CheckHorizontalDistance", story: "Is horizontal distance between [Self] and [Target] [Operator] than [distance]", category: "Conditions"
	, id: "6e382ebc429bb090e09134943222596d")]
public partial class CheckHorizontalDistanceCondition : Condition
{
	[SerializeReference]
	public BlackboardVariable<Transform> Self;
	[SerializeReference]
	public BlackboardVariable<Transform> Target;
	[Comparison(ComparisonType.All)]
	[SerializeReference]
	public BlackboardVariable<ConditionOperator> Operator;
	[SerializeReference]
	public BlackboardVariable<float> Distance;

	public override bool IsTrue()
	{
		if (Self.Value == null || Target.Value == null)
		{
			return false;
		}

		var selfPos = Self.Value.position;
		selfPos.y = 0;
		var targetPos = Target.Value.position;
		targetPos.y = 0;
		float distance = Vector3.Distance(selfPos, targetPos);
		Debug.Log("distance = " + distance.ToString());
		return ConditionUtils.Evaluate(distance, Operator, Distance.Value);
	}
}