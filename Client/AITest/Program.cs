using Apache.Ignite.Core;
using Apache.Ignite.Core.Binary;
using Apache.Ignite.Core.Compute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            var cfg = new IgniteConfiguration { BinaryConfiguration = new BinaryConfiguration(typeof(CountFunc)), ClientMode = true };
            cfg.IgniteHome = @"C:\Program Files\Java\apache-ignite-fabric-1.8.0-bin";
            using (var ignite = Ignition.Start(cfg))
            {                
                int n = 10;
                int the_smallest = Int32.MaxValue;

                int[][] f = new int[n][];

                for(int i = 0; i < n; i++)
                {
                    f[i] = new int[n];

                    for(int j = 0; j < n; j++)
                    {
                        f[i][j] = r.Next(1, 10);

                        Console.Write(f[i][j] + " ");
                    }
                    Console.WriteLine();
                }

                var res = ignite.GetCompute().Apply(new CountFunc(), f);

                foreach (var ul in res)
                {
                    if (ul < the_smallest) {
                        the_smallest = ul;
                    }                    
                }

                Console.WriteLine("Самый маленький элемент: " + the_smallest);
                Console.Read();
            }            
        }
    }

    internal class CountFunc : IComputeFunc<int[], int>
    {
        public int Invoke(int[] arg)
        {            
            return 0;
        }
    }
}
