using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PseudoEnumerable;
using PseudoLINQ;

namespace ConsoleForLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //var source = new string[] { "que", "qweqwe", "frefer" };

            //var result = source.Where(s => s.Length > 4);
            //foreach (var item in result)
            //{
            //    Console.Write($"{item} ");
            //}
            //Console.WriteLine();
            //var result1 = source.Where(s => s.Length > 4);
            //foreach (var item in result1)
            //{
            //    Console.Write($"{item} ");
            //}
            var source1 = new string[] { "que55", "qweqwe", "frefer" };
            var result = source1.ForAll<string>(s => s.Length > 4);

            Console.WriteLine(result);

        }
    }
}
