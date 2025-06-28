using System;
using GameEngine;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable] [GeneratePropertyBag]
[NodeDescription("GetCarryAnchorPoint", story: "[Self] get [CarryAnchorPoint]", category: "Action", id: "0b28cbb1cd93884df398aaa989c3c972")]
public partial class GetCarryAnchorPointAction : Action
{
	[SerializeReference]
	public BlackboardVariable<GameObject> Self;
	[SerializeReference]
	public BlackboardVariable<Transform> CarryAnchorPoint;

	protected override Status OnStart()
	{
		CarryAnchorPoint.Value = Self.Value.GetComponent<ICarry>().GetAnchorPoint();
		return Status.Success;
	}
}