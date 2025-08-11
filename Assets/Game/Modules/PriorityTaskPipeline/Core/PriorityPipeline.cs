using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace PriorityTaskPipeline
{
	public class PriorityPipeline : IPriorityPipeline
	{
		public int Count => _priorityQueue.Count;

		private readonly PriorityQueueInt<UniTask> _priorityQueue = new();

		public void Add(UniTask task, int priority)
		{
			_priorityQueue.Add(task, priority);
		}

		public void Clear()
		{
			_priorityQueue.Clear();
		}

		public async UniTask Launch()
		{
			while (_priorityQueue.Count > 0)
			{
				var node = _priorityQueue.GetNode();
				UniTask task = node.Element;
				var priority = node.Priority;
				if (_priorityQueue.Count > 0 && _priorityQueue.PeekNode().Priority == priority)
				{
					var taskList = new List<UniTask>() { task, _priorityQueue.GetElement() };
					while (_priorityQueue.Count > 0 && _priorityQueue.PeekNode().Priority == priority)
					{
						taskList.Add(_priorityQueue.GetElement());
					}

					await UniTask.WhenAll(taskList);
				}
				else
				{
					await task;
				}
			}
		}
	}

	// public class PriorityPipeline<T>
	// {
	// 	private readonly PriorityQueueInt<UniTask<T>> _priorityQueue = new();
	//
	// 	public void Add(UniTask<T> task, int priority)
	// 	{
	// 		_priorityQueue.Add(task, priority);
	// 	}
	//
	// 	public async UniTask<T> Launch()
	// 	{
	// 		while (_priorityQueue.Count > 0)
	// 		{
	// 			PriorityQueueInt<UniTask<T>>.Node node = _priorityQueue.GetNode();
	// 			UniTask<T> task = node.Element;
	// 			var priority = node.Priority;
	// 			if (_priorityQueue.Count > 0 && _priorityQueue.PeekNode().Priority == priority)
	// 			{
	// 				var taskList = new List<UniTask<T>>() { task, _priorityQueue.GetElement() };
	// 				while (_priorityQueue.Count > 0 && _priorityQueue.PeekNode().Priority == priority)
	// 				{
	// 					taskList.Add(_priorityQueue.GetElement());
	// 				}
	//
	// 				await UniTask.WhenAll(taskList);
	// 			}
	// 			else
	// 			{
	// 				await task;
	// 			}
	// 		}
	// 	}
	// }
}