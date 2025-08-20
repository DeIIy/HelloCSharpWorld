using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloCSharpWorld
{
    public static class InputHandler
    {
        public static int IntInputHandler(string userNotice)
        {
            int value;
            while (true)
            {
                Console.Write(userNotice);
                try
                {
                    value = Int32.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Lütfen sadece sayısal bir değer giriniz.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Girdiğiniz sayı çok büyük veya küçük. Lütfen geçerli bir aralıkta sayı giriniz.");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Herhangi bir giriş yapılmadı. Lütfen tekrar deneyiniz.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Beklenmeyen bir hata oluştu. Lütfen tekrar deneyiniz.");
                }
            }
            return value;
        }
    }

    internal class Program
    {
        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            int sqrt = (int)Math.Sqrt(number);
            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        static int GetNextPrime(int currentPrime)
        {
            do
            {
                currentPrime++;
            } while (!IsPrime(currentPrime));
            return currentPrime;
        }

        static (int gcdResult, int firstValue, int secondValue) DivideIfDivisible(
            int gcdResult, int factorCandidate, int firstValue, int secondValue)
        {
            if (firstValue % factorCandidate == 0 && secondValue % factorCandidate == 0)
            {
                gcdResult *= factorCandidate;

                firstValue /= factorCandidate;
                secondValue /= factorCandidate;
            }
            else if (firstValue % factorCandidate == 0)
            {
                firstValue /= factorCandidate;
            }
            else if (secondValue % factorCandidate == 0)
            {
                secondValue /= factorCandidate;
            }

            return (gcdResult, firstValue, secondValue);
        }

        static int CalculateGCD(int gcdResult, int firstValue, int secondValue)
        {
            int factorCandidate = 2;
            while (firstValue != 1 && secondValue != 1)
            {
                if (firstValue % factorCandidate == 0 || secondValue % factorCandidate == 0)
                {
                    var result = DivideIfDivisible(gcdResult, factorCandidate, firstValue, secondValue);
                    gcdResult = result.gcdResult;
                    firstValue = result.firstValue;
                    secondValue = result.secondValue;
                }
                else
                {
                    factorCandidate = GetNextPrime(factorCandidate);
                }
            }
            return gcdResult;
        }

        static (int firstValue, int secondValue) GetNumberForGCD()
        {
            int firstValue = InputHandler.IntInputHandler("EBOB hesaplaması yapılacak ilk sayıyı giriniz: ");
            int secondValue = InputHandler.IntInputHandler("EBOB hesaplaması yapılacak ikinci sayıyı giriniz: ");
            return (firstValue, secondValue);
        }

        static void Main(string[] args)
        {;
            var (firstValue, secondValue) = GetNumberForGCD();
            int gcdResult = CalculateGCD(1, firstValue, secondValue);
            Console.WriteLine($"EBOB({firstValue}, {secondValue}) = {gcdResult}");
        }
    }
}
