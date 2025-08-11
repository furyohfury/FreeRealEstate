using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PriorityTaskPipeline
{
	public abstract class PriorityPipelineNodeConfig : ScriptableObject
	{
		public int Priority;

		public abstract UniTask GetTasks();
	}
}