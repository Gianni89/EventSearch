using System;

namespace EventSearch
{
    public class Event
    {
        public int numberOfTickets;
        public int locationX;
        public int locationY;

        public float maxPrice = 50f;
        public float minPrice = 30f;

        public int eventName;
        public Ticket[] tickets;
        static Random randomPrice = new Random();

        public Event(int index, int numberOfTickets, int locationX, int locationY)
        {
            this.eventName = index;
            this.numberOfTickets = numberOfTickets;
            this.locationX = locationX;
            this.locationY = locationY;
        }

        public void SetTicketPrice()
        {
            tickets = new Ticket[numberOfTickets];

            if (numberOfTickets != 0)
            {
                for (int i = 0; i < numberOfTickets; i++)
                {
                    Double longPrice = randomPrice.NextDouble() * (maxPrice - minPrice) + minPrice;
                    Double ticketPrice = Math.Round(longPrice, 2);
                    tickets[i] = new Ticket(ticketPrice);
                }
            }
        }

        public Double FindLowestPrice()
        {
            if (numberOfTickets != 0)
            {
                Array.Sort(tickets, delegate (Ticket ticket1, Ticket ticket2)
                {
                    return ticket1.price.CompareTo(ticket2.price);
                });
            return tickets[0].price;
            }
            else return 0;
        }
    }
}

