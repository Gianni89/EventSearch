using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSearch
{
    public class EventDistances
    {
        public static Dictionary<Event, int> EventAndDistanceToEvent { get; set; } =
            new Dictionary<Event, int>();

        public static Dictionary<Event, int> FindNearestEvents()
        {
            //Convert user input into usable location
            const char delimiter = ',';
            var locationInputs = EventSearch.UserInput.Split(delimiter);
            var currentLocationX = int.Parse(locationInputs[0]);
            var currentLocationY = int.Parse(locationInputs[1]);

            //Calculate the distance to each event and store the event and distance to the dictionary eventAndDistanceToEvent
            for (var i = 0; i < GenerateEvents.NumberOfEvents; i++)
            {
                var distanceToEvent = CalculateDistance(currentLocationX, currentLocationY,
                    GenerateEvents.EventArray[i].LocationX,
                    GenerateEvents.EventArray[i].LocationY);
                EventAndDistanceToEvent.Add(GenerateEvents.EventArray[i], distanceToEvent);
            }

            //Order the dictionary by the distance to the nearest event
            EventAndDistanceToEvent = EventAndDistanceToEvent.OrderBy(pair => pair.Value)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            return EventAndDistanceToEvent;
        }

        public static int CalculateDistance(int currentX, int currentY, int destinationX, int destinationY)
        {
            var distanceToEvent = Math.Abs(currentX - destinationX) + Math.Abs(currentY - destinationY);
            return distanceToEvent;
        }
    }
}
