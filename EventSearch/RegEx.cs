using System.Text.RegularExpressions;

namespace EventSearch
{
    public class RegEx
    {
        private static readonly Regex LocationRegex = new Regex(@"(\A-?\b(\d|1[0])\b,-?\b(\d|1[0])\b\Z)");

        public static bool CheckCurrentLocationFormat(string userInput)
        {
            //Takes a user input and checks if the format corresponds to an optional '-' sign, followed by a digit, followed by an optional '0'. 
            //It then accepts a ',' and performs the same check
            return LocationRegex.IsMatch(userInput);
        }
    }
}
