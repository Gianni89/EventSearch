using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EventSearch
{
    internal class Program
    {
        private static string _userInput;
        private const int NumberOfEvents = 15;
        private static readonly Random NextInt = new Random();
        private static readonly Event[] EventArray = new Event[NumberOfEvents];
        private static readonly List<string> EventList = new List<string>();

        static IOrderedEnumerable<KeyValuePair<int, int>> _orderedPairs;

        private static readonly Dictionary<int, int> EventAndDistance = new Dictionary<int, int>();

        static bool _awaitingCurrentLocation = true;

        private static readonly Regex LocationRegex = new Regex(@"-?\b(\d|1[0])\b,-?\b(\d|1[0])\b");

        private static void Main()
        {
            GenerateEvents();
            Console.WriteLine("Please Input Coordinates: a,b");

            while (_awaitingCurrentLocation)
            {
                _userInput = Console.ReadLine();

                if (_userInput == "exit")
                {
                    _awaitingCurrentLocation = false;
                }

                if (CheckCurrentLocationFormat(_userInput))
                {
                    FindNearestFiveEvents();
                    PrintResult(_orderedPairs);
                    EventAndDistance.Clear();
                }
                else if (_userInput != "exit")
                {
                    Console.WriteLine(
                        "Please Input Coordinates with the following format: a,b within the range -10 to 10");
                }
            }
        }

        private static void GenerateEvents()
        {
            var counter = 0;

            while (counter < NumberOfEvents)
            {
                var randLocationX = NextInt.Next(-10, 10);
                var randLocationY = NextInt.Next(-10, 10);

                if (CheckNoEventExists(randLocationX, randLocationY))
                {
                    var numberOfTickets = NextInt.Next(0, 5);
                    EventArray[counter] = new Event(counter, numberOfTickets, randLocationX, randLocationY);
                    EventArray[counter].SetTicketPrice();
                    counter++;
                }
            }
        }

        private static bool CheckNoEventExists(int locationX, int locationY)
        {
            string locationOfEvent = $"{locationX}, {locationY}";

            if (EventList.Exists(locationToTry => locationToTry == locationOfEvent))
            {
                return false;
            }
            EventList.Add(locationOfEvent);
            return true;
        }

        private static bool CheckCurrentLocationFormat(string userInput)
        {
            return LocationRegex.IsMatch(userInput);
        }

        private static void FindNearestFiveEvents()
        {
            const char deliminator = ',';
            var locationInputs = _userInput.Split(deliminator);
            var currentLocationX = int.Parse(locationInputs[0]);
            var currentLocationY = int.Parse(locationInputs[1]);

            for (var i = 0; i < NumberOfEvents; i++)
            {
                CalculateDistance(currentLocationX, currentLocationY, EventArray[i].LocationX, EventArray[i].LocationY,
                    EventArray[i].EventName);
            }

            _orderedPairs = SortByDistance();
        }

        private static void CalculateDistance(int currentX, int currentY, int destinationX, int destinationY,
            int eventName)
        {
            var distance = Math.Abs(currentX - destinationX) + Math.Abs(currentY - destinationY);
            EventAndDistance.Add(eventName, distance);
        }

        private static IOrderedEnumerable<KeyValuePair<int, int>> SortByDistance()
        {
            var orderedPairs = from pair in EventAndDistance
                orderby pair.Value ascending
                select pair;
            return orderedPairs;
        }

        private static void PrintResult(IEnumerable<KeyValuePair<int, int>> orderedPairs)
        {
            Console.WriteLine($"Closest Events to ({_userInput})");
            var counter = 0;

            foreach (var pair in orderedPairs)
            {
                if (counter < 5)
                {
                    Console.WriteLine($"Event {pair.Key} - {CheapestOrNoTickets(pair)}, Distance {pair.Value} ");
                    counter++;
                }
            }
            Console.WriteLine("Enter another location or \"exit\" to terminate");
        }

        private static string CheapestOrNoTickets(KeyValuePair<int, int> pair)
        {
            if (EventArray[pair.Key].NumberOfTickets == 0)
            {
                return "There are no tickets for this event";
            }
            return $"The cheapest ticket is ${EventArray[pair.Key].FindLowestPrice()}";
        }
    }
}
