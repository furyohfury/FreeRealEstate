namespace PriorityTaskPipeline
{
	internal struct PriorityQueueNode<T>
	{
		public readonly T Element;
		public readonly int Priority;

		public PriorityQueueNode(T element, int priority)
		{
			Element = element;
			Priority = priority;
		}
	}
}