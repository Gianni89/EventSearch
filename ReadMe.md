# Event Search

## Write Up

For this challenge I was tasked with creating a program that would generate seed data of a set of events located in a grid world. Each point in the grid could hold a maximum of one event, and each event could have 0 or more tickets, with each ticket having a price. The user should be able to input their location into the program, with the program returning a list of the nearest five events (calculated by their Manhattan distance), the name of the event and the price of the cheapest ticket.

The program is written in c# and the project can be built and run in Visual Studio 2015 by [**downloading**](https://github.com/Gianni89/EventSearch/archive/master.zip) the repo and launching the solution file (*EventSearch.sln*). The built program EventSearch.exe can also be found and run [**here**](EventSearch/bin/Release/EventSearch.exe).

## Assumptions

The assumptions made while coding this challenge were influenced by the [requirements](#requirements) and [scenario](#scenario). 

The first constraint is that the program should randomly generate seed data. As such, I made a Generate Events [class](EventSearch/GenerateEvents.cs) which handles event generation. I have assumed that the program will have a fixed number of events, rather than the number of events being dynamic either over time or influenced by user input. With this assumption in place I made the choice to have the number of events hard coded as a property in the class, with 50 events chosen. This avoids worrying about whether the number of events is greater than the number of world points, though the program can handle (0 *to* number of world points) events.

The second constraint is that the world ranges from -10 to 10 in X and Y. I wanted the code to be as expandable as possible so only placed artificial constraints in the code to implement the world size. Events are generated at random locations within the range, with the Max and Min coordinate expressed as a field in the class. For a larger world, these can be expanded or removed. I assumed that the user would also be constrained by this world size and should only be able to input a user location within this grid. As such I made a Regular Expression [class](EventSearch/RegEx.cs) to pattern match the user input. This ensures only user inputs that are located within the world size are validated.

The [instructions](#instructions) state that the program should return the five closest events. I have assumed this means that the list should have five entries and as such it is possible that the fifth listed event is as close as an event not listed. There are other ways this could have been handled, for example in the case of events being the same distance away they could be further ordered by their ticket price. However the number of events that are listed have only been limited by the instruction constraint, which I have made as a field NumberOfEventsToList in the Event Search [class](EventSearch/EventSearch.cs). One can imagine a method that handles NumberOfEventsToList in a dynamic way, allowing the user to user to see more than the five nearest events if the wish.

To ensure that only one event is placed at any location the Generate Events [class](EventSearch/GenerateEvents.cs) has a CheckEventExists method that prevents placement of an event at a location if an event already exists there. Multiple events at a location can be supported by removing this method.

A much larger world size can be accommodated by expanding or removing the Max and Min coordinates fields in the class and accepting the same range as a valid user input. In a world where the are a much larger number of events the method used to find the nearest events would become inefficient. Currently the distance to each event is calculated and ordered, with five nearest events listed. A better approach might be to take the user's location and look for events 0 units away, 1 unit away, 2 units away etc. and stop when the desired number of events have been found. This would be much faster for a large number of events.

## Specs

###Requirements
- Code in any language you like but please provide clear instructions on how we should build & run your code
- Please use any source control system you like, and send us a link (or if you prefer just a zip of your project)
- The first requirement is your code meets the requirements
- Secondary requirements are whether your code is idiomatic for the language being coded in, easy to read, and clearly laid out

###Scenario
- Your program should randomly generate seed data
- Your program should operate in a world that ranges from -10 to +10 (Y axis), and -10 to +10 (X axis)
- Your program should assume that each co-ordinate can hold a maximum of one event
- Each event has a unique numeric identifier (e.g. 1, 2, 3)
- Each event has zero or more tickets
- Each ticket has a non-zero price, expressed in US Dollars
- The distance between two points should be computed as the Manhattan distance

###Instructions

- You are required to write a program which accepts a user location as a pair of co-ordinates, and returns a list of the five closest events, along with the cheapest ticket price for each event
- Please detail any assumptions you have made
- How might you change your program if you needed to support multiple events at the same location?
- How would you change your program if you were working with a much larger world size?

###Example Program Run
```
Please Input Coordinates:
> 4,2

Closest Events to (4,2):

Event 003 - $30.29, Distance 3

Event 001 - $35.20, Distance 5

Event 006 - $01
```


