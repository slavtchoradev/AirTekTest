using AirTek.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AirTek
{
    class Program
    {
        static void Main(string[] args)
        {
            var fligthList = new List<FlightSchedule> {
                new FlightSchedule { FlightNumber = 1 , Departure = "YUL", Arrival = "YYZ", Day = 1, OrdersList = new List<string>()},
                new FlightSchedule { FlightNumber = 2 , Departure = "YUL", Arrival = "YYC", Day = 1, OrdersList = new List<string>()},
                new FlightSchedule { FlightNumber = 3 , Departure = "YUL", Arrival = "YVR", Day = 1, OrdersList = new List<string>()},
                new FlightSchedule { FlightNumber = 4 , Departure = "YUL", Arrival = "YYZ", Day = 2, OrdersList = new List<string>()},
                new FlightSchedule { FlightNumber = 5 , Departure = "YUL", Arrival = "YYC", Day = 2, OrdersList = new List<string>()},
                new FlightSchedule { FlightNumber = 6 , Departure = "YUL", Arrival = "YVR", Day = 2, OrdersList = new List<string>()},
            };

            Console.WriteLine($"Flights schedule:");

            foreach (var fly in fligthList)
            {
                Console.WriteLine($"Flight: {fly.FlightNumber}, departure: {fly.Departure}, arrival: {fly.Arrival}, day: {fly.Day}");
            }


            List<Order> orders = GetOrders();

            Console.WriteLine();
            Console.WriteLine($"Flight itineraries:");

            foreach (var order in orders)
            {
                var fly = fligthList.Where(x => x.Arrival == order.Destination && x.OrdersList.Count < 20).FirstOrDefault();
                if (fly != null)
                {
                    fly.OrdersList.Add(order.Name);
                    Console.WriteLine($"order: {order.Name}, flightNumber: {fly.FlightNumber}, departure: {fly.Departure}, arrival: {fly.Arrival}, day: {fly.Day}");
                }
                else
                {
                    Console.WriteLine($"order: {order.Name}, flightNumber: not schedule");
                }
            }
        }

        private static List<Order> GetOrders()
        {
            List<Order> orders = null;
            var filePath = $"{Directory.GetCurrentDirectory()}/Data/orders.json";
            if (File.Exists(filePath))
            {
                string jsonStr = null;
                using (StreamReader r = new StreamReader(filePath))
                {
                    jsonStr = r.ReadToEnd();
                }

                if (jsonStr != null)
                {
                    Dictionary<string, Order> dictOrd = JsonConvert.DeserializeObject<Dictionary<string, Order>>(jsonStr);

                    orders = new List<Order>();
                    foreach (var item in dictOrd)
                    {
                        var ord = item.Value;
                        ord.Name = item.Key;
                        orders.Add(ord);
                    }
                }
            }

            return orders;
        }

    }


}
