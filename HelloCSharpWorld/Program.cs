using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCSharpWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Vize Notu:");
            double midterm = Double.Parse(Console.ReadLine());
            Console.Write("Final Notu:");
            double final = Double.Parse(Console.ReadLine());
            double average = midterm * 0.4 + final * 0.6;
            Console.WriteLine(average);
            Console.ReadLine();
        }
    }
}
