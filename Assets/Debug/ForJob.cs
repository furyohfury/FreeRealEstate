using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Debugging
{
    // [BurstCompile]
    public struct ForJob : IJobParallelFor
    {
        public NativeArray<float> ar;

        public ForJob(NativeArray<float> ar)
        {
            this.ar = ar;
        }

        // [BurstCompile]
        public void Execute(int index)
        {
            // Debug.Log(ar[index] * ar[index]);
            ar[index] = math.sqrt(ar[index]);
        }
    }
}
