using System;
using GameEngine;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable] [GeneratePropertyBag]
[NodeDescription("GetCarryAnchorPoint", story: "Get [CarryAnchorPoint] from [Target]", category: "Action", id: "0b28cbb1cd93884df398aaa989c3c972")]
public partial class GetCarryAnchorPointAction : Action
{
	[SerializeReference]
	public BlackboardVariable<GameObject> Target;
	[SerializeReference]
	public BlackboardVariable<Transform> CarryAnchorPoint;

	protected override Status OnStart()
	{
		CarryAnchorPoint.Value = Target.Value.GetComponent<IGetAnchorPoint>().GetAnchorPoint();
		return Status.Success;
	}
}