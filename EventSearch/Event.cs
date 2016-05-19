using System;

namespace EventSearch
{
    public class Event
    {
        public int NumberOfTickets;
        public int LocationX;
        public int LocationY;

        public float MaxPrice = 50f;
        public float MinPrice = 30f;

        public int EventName;
        public Ticket[] Tickets;
        private static readonly Random RandomPrice = new Random();

        public Event(int index, int numberOfTickets, int locationX, int locationY)
        {
            EventName = index;
            NumberOfTickets = numberOfTickets;
            LocationX = locationX;
            LocationY = locationY;
        }

        public void SetTicketPrice()
        {
            Tickets = new Ticket[NumberOfTickets];

            if (NumberOfTickets == 0) return;
            for (var i = 0; i < NumberOfTickets; i++)
            {
                var longPrice = RandomPrice.NextDouble()*(MaxPrice - MinPrice) + MinPrice;
                var ticketPrice = Math.Round(longPrice, 2);
                Tickets[i] = new Ticket(ticketPrice);
            }
        }

        public double FindLowestPrice()
        {
            if (NumberOfTickets == 0) return 0;
            Array.Sort(Tickets,
                (ticket1, ticket2) => ticket1.Price.CompareTo(ticket2.Price));
            return Tickets[0].Price;
        }
    }
}

