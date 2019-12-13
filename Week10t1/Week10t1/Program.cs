using System;
using System.Linq;

namespace Week10t1
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<string> genericList = new GenericList<string>();
            genericList.Add("1");
            genericList.Add("2");
            genericList.Add("45");
            genericList.Add("3");
            genericList.Add("8");
            genericList.Add("55");

            //            foreach (var item in genericList)
            //            {
            //                Console.WriteLine(item);
            //            }


            Console.WriteLine(genericList.Max());

        }
    }
}
