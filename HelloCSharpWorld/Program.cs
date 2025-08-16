using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloCSharpWorld
{
    internal class Program
    {
        static void LogExecution(string funcName)
        {
            Console.WriteLine($"{funcName} fonksiyonu başladı.");
        }

        static void ShowTheBigger(byte theBigOne, byte theSmallOne)
        {
            LogExecution("ShowTheBigger");
            Console.WriteLine("{0} sayısı {1} sayısından büyüktür", theBigOne, theSmallOne);
        }
        static void WhoIsBigger(byte alphaValue, byte omegaValue)
        {
            LogExecution("WhoIsBigger");
            if(alphaValue > omegaValue)
            {
                ShowTheBigger(alphaValue, omegaValue);
            }
            else if(omegaValue > alphaValue)
            {
                ShowTheBigger(omegaValue, alphaValue);
            }
            else
            {
                Console.WriteLine("İki sayı eşit!");
            }
        }
        static void CheckUserInput(byte alphaValue, byte omegaValue)
        {
            LogExecution("CheckUserInput");
            Console.WriteLine("Kullanıcının girdiği 1. sayı -> {0} ve 2. sayı -> {1}",alphaValue,omegaValue);
        }
        static (byte alphaValue, byte omegaValue)? GetNumbersFromUser()
        {
            LogExecution("GetNumbersFromUser");
            Console.Write("Karşılaştırma yapmak için 1. sayıyı giriniz...");
            //byte alphaValue = byte.Parse(Console.ReadLine());
            bool isValidAlpha = byte.TryParse(Console.ReadLine(), out byte alphaValue);
            if (!isValidAlpha)
            {
                Console.WriteLine("Geçerli bir sayı girmediniz!");
                return null;
            }
            Console.Write("Karşılaştırma yapmak için 2. sayıyı giriniz...");
            //byte omegaValue = byte.Parse(Console.ReadLine());
            bool isValidOmega = byte.TryParse(Console.ReadLine(), out byte omegaValue);
            if (!isValidOmega)
            {
                Console.WriteLine("Geçerli bir sayı girmediniz!");
                return null;
            }
            return (alphaValue, omegaValue);
        }
        static void Main(string[] args)
        {
            //var (alphaValue, omegaValue) = GetNumbersFromUser();
            var numbers = GetNumbersFromUser();
            if (numbers == null) return; 
            var (alphaValue, omegaValue) = numbers.Value;
            CheckUserInput(alphaValue, omegaValue);
            WhoIsBigger (alphaValue, omegaValue);
            Console.ReadLine();
        }
    }
}
