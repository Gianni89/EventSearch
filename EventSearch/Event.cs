using System;

namespace EventSearch
{
    public class Event
    {
        public int NumberOfTickets { get; }
        public int LocationX { get; }
        public int LocationY { get; }

        public int EventName { get; }
        public Ticket[] Tickets { get; set; }

        //Give ticket prices an arbitrary max value
        private const int MaxPrice = 500;

        private static readonly Random RandomPrice = new Random();

        public Event(int index, int numberOfTickets, int locationX, int locationY)
        {
            EventName = index;
            NumberOfTickets = numberOfTickets;
            LocationX = locationX;
            LocationY = locationY;
            RandomlyInitialiseTicketPrices();
        }

        public void RandomlyInitialiseTicketPrices()
        {
            Tickets = new Ticket[NumberOfTickets];

            if (NumberOfTickets == 0) return;
            for (var i = 0; i < NumberOfTickets; i++)
            {
                var ticketPrice = PickRandomPrice();
                Tickets[i] =  new Ticket(ticketPrice);
            }

            //Sort tickets by price
            Array.Sort(Tickets,
                (ticket1, ticket2) => ticket1.Price.CompareTo(ticket2.Price));
        }

        private static double PickRandomPrice()
        {
            var longPrice = RandomPrice.NextDouble() * RandomPrice.Next(MaxPrice);
            var ticketPrice = Math.Round(longPrice, 2);
            return ticketPrice;
        }

        public double FindLowestPrice()
        {
            return Tickets[0].Price;
        }
    }
}

