using System;
using System.Net;
using Core;
using Parser;
using MySql.Data.MySqlClient;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            ParserQuery parser = new ParserQuery();
            Car car = new Car();

            Console.WriteLine("Content-Type: text/html \n\n");
            var queryStr = Environment.GetEnvironmentVariable("QUERY_STRING");

            //var queryStr = "fish=Semga&MinTemp=-3&MaxTemp=5&MinTime=20&MaxTime=60&TimeStart=12.06.2021+12%3A23&Temps=1+2+3+3+4+3+5+4+2+1+1+1+1+0+-1+-2+-2+-3+-4+-4+-5+-5+-4+-4+-4+-3";
            queryStr = WebUtility.UrlDecode(queryStr);


            parser.GetFields(queryStr);

            car.fishes.Add(new Fish(parser.fields["fish"]));                   //  Название рыбы

            string connStr = "server=192.168.146.128;user=wer43t;database=Fishes;port=3306;password=1";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();

                string sql = $"SELECT * FROM Fishes.Fish Where Name = '{parser.fields["fish"]}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    car.fishes[0].fishType.minTemp = Convert.ToInt32(res[2]);
                    car.fishes[0].fishType.maxTemp = Convert.ToInt32(res[3]);
                    car.fishes[0].fishType.minTime = Convert.ToInt32(res[4]);
                    car.fishes[0].fishType.maxTime = Convert.ToInt32(res[5]);
                }                                  
                res.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            conn.Close();

            Console.WriteLine($"<pre>{car.Delivery(parser.fields["TimeStart"], parser.fields["Temps"])}</pre>"); // Время старта, Температура
        }

    }
}
