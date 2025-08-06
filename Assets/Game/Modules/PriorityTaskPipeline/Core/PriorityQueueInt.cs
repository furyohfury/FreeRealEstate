using System;
using System.Collections.Generic;
using System.Linq;

namespace PriorityTaskPipeline
{
	internal class PriorityQueueInt<T>
	{
		public int Count => _heap.Count;

		public List<PriorityQueueNode<T>> Nodes => _heap;
		public T[] Elements => _heap.Select(node => node.Element).ToArray();

		private readonly List<PriorityQueueNode<T>> _heap;

		public PriorityQueueInt()
		{
			_heap = new List<PriorityQueueNode<T>>();
		}

		public void Add(T element, int priority)
		{
			_heap.Add(new PriorityQueueNode<T>(element, priority));

			int index = Count - 1;
			int parent = (index - 1) / 2;

			while (index > 0 && _heap[parent].Priority < _heap[index].Priority)
			{
				SwapElements(index, parent);

				index = parent;
				parent = (index - 1) / 2;
			}
		}

		public T GetElement()
		{
			if (Count == 0)
			{
				throw new InvalidOperationException("Queue is empty.");
			}

			T result = _heap[0].Element;
			_heap[0] = _heap[^1];
			_heap.RemoveAt(Count - 1);
			Heapify(0);
			return result;
		}

		public PriorityQueueNode<T> GetNode()
		{
			if (Count == 0)
			{
				throw new InvalidOperationException("Queue is empty.");
			}

			PriorityQueueNode<T> result = _heap[0];
			_heap[0] = _heap[^1];
			_heap.RemoveAt(Count - 1);
			Heapify(0);
			return result;
		}

		public T PeekElement()
		{
			if (Count == 0)
			{
				throw new InvalidOperationException("Queue is empty.");
			}

			return _heap[0].Element;
		}

		public PriorityQueueNode<T> PeekNode()
		{
			if (Count == 0)
			{
				throw new InvalidOperationException("Queue is empty.");
			}

			return _heap[0];
		}

		public void Clear()
		{
			_heap.Clear();
		}

		private void Heapify(int index)
		{
			int leftChild;
			int rightChild;
			int largestChild;

			for (;;)
			{
				leftChild = 2 * index + 1;
				rightChild = 2 * index + 2;
				largestChild = index;

				if (leftChild < Count && _heap[leftChild].Priority > _heap[largestChild].Priority)
				{
					largestChild = leftChild;
				}

				if (rightChild < Count && _heap[rightChild].Priority > _heap[largestChild].Priority)
				{
					largestChild = rightChild;
				}

				if (largestChild == index)
				{
					break;
				}

				SwapElements(index, largestChild);
				index = largestChild;
			}
		}

		private void SwapElements(int firstIndex, int secondIndex)
		{
			(_heap[firstIndex], _heap[secondIndex]) = (_heap[secondIndex], _heap[firstIndex]);
		}
	}
}