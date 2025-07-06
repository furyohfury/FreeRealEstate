using System;
using GameEngine;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable] [GeneratePropertyBag]
[NodeDescription("Debug Entity", story: "Debug [Entity] is null", category: "Action", id: "fc6c54536b8c2ff6c0d151f48677d604")]
public partial class DebugEntityAction : Action
{
	[SerializeReference]
	public BlackboardVariable<GameObject> Entity;

	protected override Status OnStart()
	{
		if (Entity.Value == null)
		{
			Debug.Log("Entity is null");
		}

		return Status.Success;
	}
}