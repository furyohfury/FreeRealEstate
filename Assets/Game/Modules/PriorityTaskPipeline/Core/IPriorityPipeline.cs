using Cysharp.Threading.Tasks;

namespace PriorityTaskPipeline
{
	public interface IPriorityPipeline
	{
		void Add(UniTask task, int priority);
		UniTask Launch();
	}
}