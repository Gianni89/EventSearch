using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EventSearch
{
    class Program
    {
        static String userInput;
        static int numberOfEvents = 15;
        static Random nextInt = new Random();
        static Event[] eventArray = new Event[numberOfEvents];
        static Event[] NearestFiveEvents = new Event[5];
        static List<string> eventList = new List<string>();

        static IOrderedEnumerable<KeyValuePair<int, int>> orderedPairs;

        static Dictionary<int, int> eventAndDistance = new Dictionary<int, int>();

        static bool awaitingCurrentLocation = true;

        private static readonly Regex locationRegex = new Regex(@"-?\b(\d|1[0])\b,-?\b(\d|1[0])\b"); 
                                                                           
        static void Main(string[] args)
        {
            GenerateEvents();
            Console.WriteLine("Please Input Coordinates: a,b");

            while (awaitingCurrentLocation)
            {
                userInput = Console.ReadLine();

                if (userInput == "exit")
                {
                    awaitingCurrentLocation = false;
                }

                if (CheckCurrentLocationFormat(userInput))
                {
                    FindNearestFiveEvents();
                    PrintResult(orderedPairs);
                    eventAndDistance.Clear();
                }
                else if (userInput != "exit")
                {
                    Console.WriteLine("Please Input Coordinates with the following format: a,b within the range -10 to 10");
                }
            }
        }

        static void GenerateEvents()
        {
            int counter = 0;

            while (counter < numberOfEvents)
            {
                int randLocationX = nextInt.Next(-10, 10);
                int randLocationY = nextInt.Next(-10, 10);

                if (CheckIfEventExists(randLocationX, randLocationY))
                {
                    randLocationX = nextInt.Next(-10, 10);
                    randLocationY = nextInt.Next(-10, 10);
                }
                else
                {
                    int numberOfTickets = nextInt.Next(0, 5);
                    eventArray[counter] = new Event(counter, numberOfTickets, randLocationX, randLocationY);
                    eventArray[counter].SetTicketPrice();
                    counter++;
                }
            }
        }

        static bool CheckIfEventExists(int locationX, int locationY)
        {
            string locationOfEvent = String.Format("{0}, {1}", locationX.ToString(), locationY.ToString());

            if (eventList.Exists(locationToTry => locationToTry == locationOfEvent))
            {
                return true;
            }
            else
            {
                eventList.Add(locationOfEvent);
                return false;
            }
        }

        static bool CheckCurrentLocationFormat(string userInput)
        {
            return locationRegex.IsMatch(userInput);
        }

        static void FindNearestFiveEvents()
        {
            Char deliminator = ',';
            String[] locationInputs = userInput.Split(deliminator);
            int currentLocationX = int.Parse(locationInputs[0]);
            int currentLocationY = int.Parse(locationInputs[1]);

            for (int i = 0; i < numberOfEvents; i++)
            {
                CalculateDistance(currentLocationX, currentLocationY, eventArray[i].locationX, eventArray[i].locationY, eventArray[i].eventName);
            }

            orderedPairs = SortByDistance();
        }

        static void CalculateDistance(int currentX, int currentY, int destinationX, int destinationY, int eventName)
        {
            int distance = Math.Abs(currentX - destinationX) + Math.Abs(currentY - destinationY);
            eventAndDistance.Add(eventName, distance);
        }

        static IOrderedEnumerable<KeyValuePair<int, int>> SortByDistance()
        {
            var orderedPairs =  from pair in eventAndDistance
                                orderby pair.Value ascending
                                select pair;
            return orderedPairs;
        }

        static void PrintResult(IOrderedEnumerable<KeyValuePair<int, int>> orderedPairs)
        {
            Console.WriteLine("Closest Events to ({0})", userInput);
            int counter = 0;

            foreach (KeyValuePair<int, int> pair in orderedPairs)
            {
                if (counter < 5)
                {
                    Console.WriteLine("Event {0} - {1}, Distance {2} ", pair.Key, CheapestOrNoTickets(pair), pair.Value);
                    counter++;
                }
            }
            Console.WriteLine("Enter another location or \"exit\" to terminate");
        }

        static string CheapestOrNoTickets(KeyValuePair<int, int> pair)
        {
            if (eventArray[pair.Key].numberOfTickets == 0)
            {
                return "There are no tickets for this event";
            }
            else
            {
                return String.Format("The cheapest ticket is ${0}", eventArray[pair.Key].FindLowestPrice());
            }
        }
    }
}
