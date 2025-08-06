using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PriorityTaskPipeline
{
	[CreateAssetMenu(fileName = "DebugNodeConfig", menuName = "PriorityTaskPipeline/DebugNodeConfig")]
	public sealed class DebugNodeConfig : PriorityPipelineNodeConfig
	{
		public override UniTask[] GetTasks()
		{
			return Array.Empty<UniTask>();
		}
	}
}