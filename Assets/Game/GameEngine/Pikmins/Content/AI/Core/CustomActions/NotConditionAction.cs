using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable] [GeneratePropertyBag]
[NodeDescription("NotCondition", story: "Not condition", category: "Action", id: "bb0a618d884a6363528962e8ac3d0658")]
public partial class NotConditionAction : Action
{
	[SerializeReference]
	protected Condition m_Condition;

	protected override Status OnStart()
	{
		m_Condition.OnStart();

		return m_Condition.IsTrue()
			? Status.Failure
			: Status.Success;
	}

	protected override void OnEnd()
	{
		base.OnEnd();

		m_Condition.OnEnd();
	}
}