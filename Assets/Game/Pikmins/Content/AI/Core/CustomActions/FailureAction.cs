using System;
using Unity.Behavior;
using Unity.Properties;
using Action = Unity.Behavior.Action;

[Serializable] [GeneratePropertyBag]
[NodeDescription("Failure", story: "Return failure", category: "Action", id: "2a8d871919fa47bf8a4039fe36a49122")]
public partial class FailureAction : Action
{
	protected override Status OnStart()
	{
		return Status.Failure;
	}
}