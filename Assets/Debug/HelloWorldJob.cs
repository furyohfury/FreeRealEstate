using Unity.Jobs;
using UnityEngine;

namespace Debugging
{
    public struct HelloWorldJob : IJob
    {
        public void Execute()
        {
            Debug.Log("Hello World!");
        }
    }
}
