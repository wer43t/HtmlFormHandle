using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Core;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Car car = new Car();

            Console.WriteLine("Content-Type: text/html \n\n");
            var queryStr = Environment.GetEnvironmentVariable("QUERY_STRING");

            //var queryStr = "FishName=Semga&MinTemp=-3&MaxTemp=5&MinTime=20&MaxTime=60&TimeStart=12.06.2021+12%3A23&Temps=1+2+3+3+4+3+5+4+2+1+1+1+1+0+-1+-2+-2+-3+-4+-4+-5+-5+-4+-4+-4+-3";
            queryStr = WebUtility.UrlDecode(queryStr);
            string[] queryArr = queryStr.Split('&');

            //foreach (string q in queryArr)
            //{
            //    Console.WriteLine(q);
            //}


            //int fishCount = int.Parse(Console.ReadLine());                      //  Количество рыб

            //for (int i = 0; i < fishCount; i++)
            //{
            car.fishes.Add(new Fish(queryArr[0].Split('=')[1]));                   //  Название рыбы
            car.fishes[0].fishType.minTemp = int.Parse(queryArr[1].Split('=')[1]); //  Минимальная температура      
            car.fishes[0].fishType.maxTemp = int.Parse(queryArr[2].Split('=')[1]); //  Максимальная температура
            car.fishes[0].fishType.minTime = int.Parse(queryArr[3].Split('=')[1]); //  Минимальное время
            car.fishes[0].fishType.maxTime = int.Parse(queryArr[4].Split('=')[1]); //  Максимальное время
                                                                                   //}

            Console.WriteLine($"<pre>{car.Delivery(queryArr[5].Split('=')[1], queryArr[6].Split('=')[1])}</pre>"); // Время старта, Температура
        }
    }
}
