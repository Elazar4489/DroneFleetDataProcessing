using DroneSystem.src.Pipeline;
using System;
namespace DroneSystem.program
{
    class Program
    {
        static void Main(string[] args)
        {
            DataPipeline pipeline = new DataPipeline();
            pipeline.Run();
        }
    }
}
