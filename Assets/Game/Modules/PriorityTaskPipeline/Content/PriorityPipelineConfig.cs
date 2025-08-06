using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PriorityTaskPipeline
{
	[CreateAssetMenu(
		fileName = nameof(PriorityPipelineConfig),
		menuName = nameof(PriorityTaskPipeline) + "/" + nameof(PriorityPipelineConfig)
	)]
	public class PriorityPipelineConfig : ScriptableObject
	{
		[SerializeField] [HorizontalGroup("Nodes")]
		private PriorityPipelineNodeConfig[] _nodes;

		[ShowInInspector] [ReadOnly] [HorizontalGroup("Nodes")]
		private int[] _priorities;
		private readonly PriorityPipeline _pipeline = new();

		public void Add(UniTask task, int priority)
		{
			_pipeline.Add(task, priority);
		}

		public async UniTask Launch()
		{
			if (_pipeline.Count <= 0)
			{
				for (int i = 0, count = _nodes.Length; i < count; i++)
				{
					UniTask[] tasks = _nodes[i].GetTasks();
					for (int j = 0, taskCount = tasks.Length; j < taskCount; j++)
					{
						_pipeline.Add(tasks[i], _nodes[i].Priority);
					}
				}
			}

			await _pipeline.Launch();
		}

		private void OnValidate()
		{
			Array.Sort(_nodes, (a, b) =>
			{
				if (a.Priority == b.Priority)
				{
					return 0;
				}

				if (a.Priority > b.Priority)
				{
					return 1;
				}

				return -1;
			});

			_priorities = _nodes.Select(node => node.Priority).ToArray();
		}
	}
}