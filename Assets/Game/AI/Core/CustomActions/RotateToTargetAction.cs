using System;
using GameEngine;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable] [GeneratePropertyBag]
[NodeDescription("RotateToTarget", story: "Rotate [Self] to [Target] within [Angle] degrees", category: "Action"
	, id: "1c3c51cf95d2b734a7f6645ae7bf8b3b")]
public partial class RotateToTargetAction : Action
{
	[SerializeReference]
	public BlackboardVariable<Transform> Self;
	[SerializeReference]
	public BlackboardVariable<Transform> Target;
	[SerializeReference]
	public BlackboardVariable<float> Angle;

	protected override Status OnUpdate()
	{
		Vector3 lookDirection = Target.Value.position - Self.Value.position;
		lookDirection.y = 0f;
		var angle = Vector3.Angle(Self.Value.forward, lookDirection);
		Debug.Log("Angle = " + angle);
		if (angle <= Angle.Value)
		{
			return Status.Success;
		}

		var rotateable = Self.Value.GetComponent<IRotateable>();
		Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
		rotateable.RotateTo(targetRotation);
		return Status.Running;
	}
}