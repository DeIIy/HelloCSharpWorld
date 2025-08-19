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
        static void Ebob(ref int ebobXY, int x, int y)
        {
            int asalcarpan = 2;
            while(x!=1 && y!=1)
            {
                if(x % asalcarpan == 0 && y % asalcarpan == 0) 
                {
                    x = x / asalcarpan; //sayıyı asal çarpana böldüm
                    y = y / asalcarpan; //sayıyı asal çarpana böldüm
                    ebobXY = ebobXY * asalcarpan; // Her iki sayıyı da böldüğü çarpıma dahil ettim
                }
                else if(x % asalcarpan != 0 && y % asalcarpan == 0)
                {
                    y = y / asalcarpan; // Sadece bu sayı bölünüyorsa böldüm
                }
                else if (x % asalcarpan == 0 && y % asalcarpan != 0)
                {
                    x = x / asalcarpan; // Sadece bu sayı bölünüyorsa böldüm
                }
                else // Her iki sayı da tam bölünmüyorsa bir sonraki asal çarpana geçilecek
                {
                    bool asalSayi = false; // Durum olumsuz!
                    do
                    {
                        asalcarpan++; // Asal çarpanı bir artırdım yeni değer asal olmayabilir.
                        for(int i = 2; i < asalcarpan; i++) // Döngü başlattım
                        {
                            if (asalcarpan % i == 0) break; // 1 veya kendisi haricindeki bir sayıya tam bölünmemeli
                            else asalSayi = true; // Durum olumlu
                        }
                        if (asalSayi) break; // Döngüden çıktım.
                    } while(true);
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Title = "Ebob";
            int ebob = 1;
            Console.Write("Birinci Sayı:");
            int bS = Int32.Parse(Console.ReadLine());
            Console.Write("İkinci Sayı:");
            int iS = Int32.Parse(Console.ReadLine());
            Ebob(ref ebob, bS, iS);
            Console.WriteLine("Girilen {0} ve {1} Sayılarının Ebobu: {2}",bS,iS,ebob);
            Console.ReadLine();
        }
            
    }
}
