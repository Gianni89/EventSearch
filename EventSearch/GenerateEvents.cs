using System;

namespace EventSearch
{
    public class GenerateEvents
    {
        public static Event[] EventArray { get; set; }
        public static Random Random { get; } = new Random();

        public static bool EventExists { get; set; }

        //The world should be populated with a number of events, 50 is chosen arbitrarily
        public static int NumberOfEvents { get; } = 50;

        //The world should only extend from -10 to 10
        private const int MinCoord = -10;
        private const int MaxCoord = 10;

        //There should be between 0 and some number of tickets per event, 5 is chosen arbitrarily
        private const int MinTicketsPerEvent = 0;
        private const int MaxTicketsPerEvent = 5;

        public static void GenerateSeedEvents()
        {
            var counter = 0;

            EventArray = new Event[NumberOfEvents];

            while (counter < NumberOfEvents)
            {
                var randLocationX = Random.Next(MinCoord, MaxCoord + 1); //+1 as upperbound is exclusive
                var randLocationY = Random.Next(MinCoord, MaxCoord + 1);

                var eventExists = CheckEventExists(randLocationX, randLocationY);
                //Checks to see if an event already exists at location

                if (!eventExists)
                {
                    //If no event already exists at location, create an event there with a random number of tickets
                    //Increase the counter for generated events
                    var numberOfTickets = Random.Next(MinTicketsPerEvent, MaxTicketsPerEvent + 1);
                    EventArray[counter] = new Event(counter, numberOfTickets, randLocationX, randLocationY);
                    counter++;
                }
            }
        }

        public static bool CheckEventExists(int locationX, int locationY)
        {
            //We only want one event per location so we check here to see if an event already exists.
            //Takes a location to try placing an event at, and checks the locations of already existing events; 
            //Returns true iff an event already exists at the location to try, else false
            foreach (var _event in EventArray)
            {
                if (_event != null)
                {
                    EventExists = _event.LocationX == locationX & _event.LocationY == locationY;
                    if (EventExists)
                    {
                        return EventExists;
                    }
                }
            }

            EventExists = false;
            return EventExists;
        }
    }
}
