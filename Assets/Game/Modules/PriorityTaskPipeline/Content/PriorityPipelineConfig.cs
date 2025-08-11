using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace PriorityTaskPipeline
{
	[CreateAssetMenu(
		fileName = nameof(PriorityPipelineConfig),
		menuName = nameof(PriorityTaskPipeline) + "/" + nameof(PriorityPipelineConfig)
	)]
	public class PriorityPipelineConfig : ScriptableObject, IPriorityPipeline
	{
		[SerializeField]
		[HorizontalGroup("Nodes")]
		[OnCollectionChanged(nameof(NodesChanged))]
		[ListDrawerSettings(DraggableItems = false)]
		private PriorityPipelineNodeConfig[] _nodes;

		[SerializeField]
		[HorizontalGroup("Nodes")]
		[ListDrawerSettings(DraggableItems = false
			, DefaultExpandedState = true
			, HideAddButton = true
			, HideRemoveButton = true)]
		private List<int> _priorities;
		private readonly PriorityPipeline _pipeline = new();

		public void Add(UniTask task, int priority)
		{
			_pipeline.Add(task, priority);
		}

		public async UniTask Launch()
		{
			if (IsPipelineInitialized() == false)
			{
				InitializePipeline();
			}

			await _pipeline.Launch();
		}

		private bool IsPipelineInitialized()
		{
			return _pipeline.Count > 0;
		}

		private void InitializePipeline()
		{
			for (int i = 0, count = _nodes.Length; i < count; i++)
			{
				UniTask task = _nodes[i].GetTasks();
				_pipeline.Add(task, _nodes[i].Priority);
			}
		}

		private void NodesChanged(CollectionChangeInfo info)
		{
			SortNodes();
			UpdatePriorities();
		}

		[Button]
		private void UpdateNodesAccordingToPriorities()
		{
			for (int i = 0, count = _priorities.Count; i < count; i++)
			{
				_nodes[i].Priority = _priorities[i];
			}

			SortNodes();
			UpdatePriorities();
		}

		private void UpdatePriorities()
		{
			_priorities = _nodes.Select(node => node.Priority).ToList();
		}

		private void SortNodes()
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
		}
	}
}