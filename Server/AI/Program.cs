using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Compute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new IgniteConfiguration { BinaryConfiguration = new BinaryConfiguration(typeof(CountFunc)) };
            cfg.IgniteHome = @"C:\Program Files\Java\apache-ignite-fabric-1.8.0-bin";
            using (var ignite = Ignition.Start(cfg))
            {
                Console.Read();
            }            
        }
    }

    internal class CountFunc : IComputeFunc<int[], int>
    {
        public int Invoke(int[] arg)
        {
            int smallest = Int32.MaxValue;
            string arr = "";

            for (int i = 0; i < arg.Length; i++)
            {
                if (arg[i] < smallest) {
                    smallest = arg[i];
                }
                arr += arg[i] + " ";
            }
            Console.WriteLine(arr + " Самый маленький в строке: " + smallest);

            return smallest;
        }
    }
}
