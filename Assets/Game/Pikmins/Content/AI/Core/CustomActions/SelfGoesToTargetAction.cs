using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable] [GeneratePropertyBag]
[NodeDescription("SelfGoesToTarget", story: "[Self] goes to [Target] with StoppingDistance [StoppingDistance]", category: "Action"
	, id: "c3562c6c4d963d15da18d1a899cb520b")]
public partial class SelfGoesToTargetAction : Action
{
	[SerializeReference]
	public BlackboardVariable<GameObject> Self;
	[SerializeReference]
	public BlackboardVariable<GameObject> Target;
	[SerializeReference]
	public BlackboardVariable<float> StoppingDistance;
	private Vector3 _targetPos;
	private NavMeshAgent _agent;

	protected override Status OnStart()
	{
		var selfPos = Self.Value.transform.position;
		var targetPos = Target.Value.transform.position;
		_targetPos = targetPos;
		targetPos.y = selfPos.y;
		var distance = Vector3.Distance(selfPos, targetPos);
		if (distance <= StoppingDistance)
		{
			return Status.Success;
		}

		_agent = Self.Value.GetComponentInChildren<NavMeshAgent>();
		_agent.stoppingDistance = StoppingDistance;
		_agent.SetDestination(_targetPos);
		return Status.Running;
	}

	protected override Status OnUpdate()
	{
		var targetPos = Target.Value.transform.position;
		if (_targetPos == targetPos)
		{
			return _agent.remainingDistance <= StoppingDistance.Value
				? Status.Success
				: Status.Running;
		}

		_targetPos = targetPos;
		var selfPos = Self.Value.transform.position;
		var distance = Vector3.Distance(selfPos, targetPos);
		if (distance <= StoppingDistance)
		{
			return Status.Success;
		}

		_agent.SetDestination(_targetPos);
		return Status.Running;
	}
}