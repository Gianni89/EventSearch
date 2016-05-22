using System;
using System.Collections.Generic;

namespace EventSearch
{
    internal class EventSearch
    {
        private static bool _awaitingCurrentLocation = true;
        public static string UserInput { get; set; }

        //Only want to list the nearest 5 events
        private const int NumberOfEventsToList = 5;

        private static void Main()
        {
            GenerateEvents.GenerateSeedEvents();

            Console.WriteLine("Please Input Coordinates: a,b");

            while (_awaitingCurrentLocation)
            {
                UserInput = Console.ReadLine();

                if (UserInput == "exit")
                {
                    _awaitingCurrentLocation = false; //Will terminate the program
                }

                if (RegEx.CheckCurrentLocationFormat(UserInput))
                    //Check format of user input is valid and in world range
                {
                    PrintResult(EventDistances.FindNearestEvents()); //Prints the desired output
                    EventDistances.EventAndDistanceToEvent.Clear();
                    //Clears the eventAndDistanceToEvent dictionary to allow the user to input another location
                }
                else if (UserInput != "exit")
                {
                    Console.WriteLine(
                        "Please Input Coordinates with the following format: a,b within the range -10 to 10");
                }
            }
        }

        private static void PrintResult(Dictionary<Event, int> eventAndDistanceToEvent)
        {
            Console.WriteLine($"Closest Events to ({UserInput})");
            var counter = 0;

            foreach (var pair in eventAndDistanceToEvent)
            {
                if (counter < NumberOfEventsToList)
                {
                    Console.WriteLine(
                        $"Event {pair.Key.EventName} - {CheapestOrNoTickets(pair.Key)}, Distance {pair.Value} ");
                    counter++;
                }
            }

            Console.WriteLine("Enter another location or \"exit\" to terminate");
        }

        private static string CheapestOrNoTickets(Event _event)
        {
            if (_event.NumberOfTickets == 0)
            {
                return "There are no tickets for this event";
            }

            return $"The cheapest ticket is ${_event.FindLowestPrice()}";
        }
    }
}
