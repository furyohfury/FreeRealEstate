using System.Diagnostics;
using TriInspector;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Debugging
{
    public class DOTSTests : MonoBehaviour
    {
        private void Start()
        {
            var helloWorldJob = new HelloWorldJob();
            JobHandle jobHandle = helloWorldJob.Schedule();
            jobHandle.Complete();
        }

        [Button]
        private void LaunchForJobs()
        {
            float[] floats = new float[10000000];
            for (int i = 0, count = floats.Length; i < count; i++)
            {
                floats[i] = i;
            }
            var nativeArray = new NativeArray<float>(floats, Allocator.TempJob);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            JobHandle jobHandle = new ForJob(nativeArray).Schedule(nativeArray.Length, 1000);
            jobHandle.Complete();
            stopwatch.Stop();
            Debug.Log(stopwatch.ElapsedTicks);
            nativeArray.Dispose();
        }
    }
}
