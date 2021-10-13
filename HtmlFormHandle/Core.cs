using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class FishType
    {
        public string typeName { get; set; }
        public int minTemp { get; set; }
        public int maxTemp { get; set; }
        public int minTime { get; set; }
        public int maxTime { get; set; }

        public FishType(string newTypeName, int newMinTemp, int newMaxTemp, int newMinTime, int newMaxTime)
        {
            typeName = newTypeName;
            minTemp = newMinTemp;
            maxTemp = newMaxTemp;
            minTime = newMinTime;
            maxTime = newMaxTime;
        }
        public FishType(string newTypeName) : this(newTypeName, 0, 0, 0, 0)
        { }

    }
    public class Fish
    {
        public FishType fishType { get; set; }

        public Fish(FishType newFishType, List<DateTime> newTime, List<int> newTemp)
        {
            fishType = newFishType;
        }
        public Fish(string newFishType)
        {
            fishType = new FishType(newFishType);
        }

    }
    public class Car
    {
        public string carName { get; set; }
        public List<Fish> fishes { get; set; }
        public DateTime date { get; set; }
        public List<DateTime> time { get; set; }
        public List<int> temp { get; set; }

        public Car()
        {
            carName = "Car";
            fishes = new List<Fish>();
            date = new DateTime();
        }
        public string Delivery(string startTime, string deliveryTemp)
        {
            string result = "<table border=1>";
            foreach (Fish f in fishes)
            {
                int minMistake = 0;
                int maxMistake = 0;
                string mistakes = "";
                string status = "";
                FishType fish = f.fishType;

                DateTime start = DateTime.Parse(startTime);
                foreach (string t in deliveryTemp.Split(' '))
                {
                    if (int.Parse(t) > fish.maxTemp)
                    {
                        if (mistakes == "")
                        {
                            mistakes += "<tr><th>Время</th><th>Факт</th><th>Норма</th><th>Отклонение</th></tr>";
                        }
                        maxMistake += 10;
                        mistakes += $"<tr><th>{start}</th> <th>{t}</th> <th>{fish.maxTemp}</th> <th>{fish.maxTemp - int.Parse(t)}</th></tr>";
                    }
                    else if (int.Parse(t) < fish.minTemp)
                    {
                        if (mistakes == "")
                        {
                            mistakes += "<tr><th>Время</th><th>Факт</th><th>Норма</th><th>Отклонение</th></tr>";
                        }
                        minMistake += 10;
                        mistakes += $"<tr><th>{start}</th> <th>{t}</th> <th>{fish.minTemp}</th> <th>{fish.minTemp - int.Parse(t)}</th></tr>";
                    }
                    start = start.AddMinutes(10);
                }

                if (minMistake > fish.minTime )
                {
                    status = $"<tr><td>Вид рыб</td><td>{fish.typeName}</td><td></td><td></td></tr><tr><td></td><td>Температура</td><td>Время</td><td></td></tr><tr><td>Max</td><td>{fish.maxTemp}</td><td>{fish.maxTime}</td><td></td></tr><tr><td>Min</td><td>{fish.minTemp}</td><td>{ fish.minTime}</td><td></td></tr><tr><td>Дата</td><td>{startTime}</td><td></td><td></td></tr><tr><td>Температура</td><td colspan='3'>{deliveryTemp}</td></tr><tr><td>Отчет</td><td></td><td></td><td></td></tr><tr><th colspan='4'>Порог минимально допустимой температуры превышен на { minMistake}минут.</th></tr>";
                }
                else if (maxMistake > fish.maxTime)
                {
                    status = $"<tr><td>Вид рыб</td><td>{fish.typeName}</td><td></td><td></td></tr><tr><td></td><td>Температура</td><td>Время</td><td></td></tr><tr><td>Max</td><td>{fish.maxTemp}</td><td>{fish.maxTime}</td><td></td></tr><tr><td>Min</td><td>{fish.minTemp}</td><td>{ fish.minTime}</td><td></td></tr><tr><td>Дата</td><td>{startTime}</td><td></td><td></td></tr><tr><td>Температура</td><td colspan='3'>{deliveryTemp}</td></tr><tr><td>Отчет</td><td></td><td></td><td></td></tr><tr><th colspan='4'>Порог минимально допустимой температуры превышен на { maxMistake}минут.</th></tr>";
                }
                else
                {
                    status = $"<tr><td>Вид рыб</td><td>{fish.typeName}</td><td></td><td></td></tr><tr><td></td><td>Температура</td><td>Время</td><td></td></tr><tr><td>Max</td><td>{fish.maxTemp}</td><td>{fish.maxTime}</td><td></td></tr><tr><td>Min</td><td>{fish.minTemp}</td><td>{ fish.minTime}</td><td></td></tr><tr><td>Дата</td><td>{startTime}</td><td></td><td></td></tr><tr><td>Температура</td><td colspan='3'>{deliveryTemp}</td></tr><tr><td>Отчет</td><td></td><td></td><td></td></tr><tr><th colspan='4'>Доставка выполнена успешно</th></tr>";
                }

                result += status + mistakes + "</table>";
            }
            return result;
        }
    }
}