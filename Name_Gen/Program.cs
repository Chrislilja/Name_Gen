using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Name_Gen
{
    class Program
    {
        static void Main(string[] args)
        {

            //Set WebURL Strings + create a .NET webclient
            string FemaleWebSource = "http://deron.meranda.us/data/popular-female-first.txt";
            string MaleWebSource = "http://deron.meranda.us/data/popular-male-first.txt";
            string LastWebSource = "http://deron.meranda.us/data/popular-last.txt";
            WebClient client = new WebClient();


            //Download HTML Response from web URLS
            string FemaleReply = client.DownloadString(FemaleWebSource);
            string MaleReply = client.DownloadString(MaleWebSource);
            string LastReply = client.DownloadString(LastWebSource);

            //Call regex function with params to return a string array
            string[] females = regexget(@"([A-Z])\w+", FemaleReply);
            string[] males =  regexget(@"([A-Z])\w+", MaleReply);
            string[] lasts = regexget(@"([A-Z])\w+", LastReply);

            //GET USER INPUT//

            Console.WriteLine(females.Length + " : females");
            Console.WriteLine(males.Length + " : males");
            Console.WriteLine(lasts.Length + " : lasts");

            Console.WriteLine();
            Console.WriteLine("Male, or Female?");
            Console.WriteLine("Male(1");
            Console.WriteLine("Female(2)");

            int gender = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many?");
            int togenerate = Convert.ToInt32(Console.ReadLine());

            //SETUP array for currently selected gender, to be copied into. Then copies based on if user selected male/female.
            string[] actingfirsts;
            switch (gender)
            {
                case 1:
                    actingfirsts = males;
                    break;
                case 2:
                    actingfirsts = females;
                    break;
                default:
                    Console.WriteLine("Eh?");
                    actingfirsts = null;
                    break;
            }

            //Creates new psyduo random number
            Random rnd = new Random();

            int counter = 0;

            
            while (counter < togenerate)
            {

                //Grabs a new int from the random number, randomized according to the size of the array's being used.
                int ChosenNameNum = rnd.Next(0, actingfirsts.Length);
                int ChosenLastNum = rnd.Next(0, lasts.Length);

                //Uses that random int to select a position in the name array's. One random number for each. Printing them
                Console.WriteLine(actingfirsts[ChosenNameNum] + ", " + lasts[ChosenLastNum]);

                //Next name
                counter++;
            }

            Console.ReadLine();



        }

        public static string[] regexget(string pattern, string input)
        {


            List<string> matchlist = new List<string>();



            foreach (Match m in Regex.Matches(input, pattern))
                matchlist.Add(m.Value);

            return matchlist.ToArray();
        }
    }
}
