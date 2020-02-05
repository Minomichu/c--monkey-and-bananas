//Jag har försökt döpa allt väldigt tydligt istället för att överkommentera,
//hoppas det är okej.

using System;
using System.Collections.Generic;

namespace ApanOchBananer
{
    class Program
    {

        //Skapar behållare för lista
        static List<StuffInList> ScoreBoard = new List<StuffInList>();


        static void WelcomePlayer()
        {

            Console.WriteLine("Hej och välkommen till spelet där du ska ge bananer till en apa!");
            Console.WriteLine("Vad heter du?");

            //Sparar det som spelaren matade in i en variabel
            String name = Console.ReadLine();

            Console.WriteLine("Hej " + name + "! Då kör vi :)\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Ready!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Set!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("GO!\n");
            Console.ResetColor();
        }


        //Spelet startar
        static void MonkeyComingCloser()
        {
            //Lagt +10 från start eftersom den ska börja på 100
            double startPoint = 110;

            //loop för att fortsätta i 10 omgångar
            for (double level = 1; level <= 10; level++)
            {

                //10 meter närmre varje bana
                startPoint -= 10;

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Bana " + level);
                Console.ResetColor();
                Console.WriteLine("Du ska kasta: " + startPoint + " meter");

                /* Anropar uträkningen av närhet till mål som
                 * anropar uträkningen av hur långt man kastat som
                 * anropar allt som behövs för själva uträkningen */
                TargetCalculation();


                static double Speed()
                {
                    Console.WriteLine("Hur många meter per sekund vill du kasta bananen?");
                    var input = Console.ReadLine();
                    Console.WriteLine("Så snabbt! " + input + " meter per sekund!");

                    //Konverterar
                    return Convert.ToDouble(input);
                }


                static double Angle()
                {
                    Console.WriteLine("Vilken vinkel vill du kasta bananen i? Du kan ange minst 0 och max 180.");

                    var input = Console.ReadLine();
                    var degrees = Convert.ToDouble(input);

                    int i = 0;
                    int j = 180;

                    if (degrees >= i && degrees <= j)
                    {
                        var radian = Math.PI * degrees / 180.0;
                        Console.WriteLine("I " + degrees + " graders vinkel, säger du det så!");
                        return radian;

                    }
                    else
                    {
                        Console.WriteLine("Det där var minsann inte en siffra mellan 0-180 :o Försök igen! \n");
                        return Angle();
                    }
                }


                static double Wind()
                {
                    double randomNumber = new Random().Next(-5, 5);
                    Console.WriteLine("Vindhastigheten är: " + randomNumber + " m/s");
                    return randomNumber;
                }


                //Hur långt man kastat
                static double LengthCalculation()
                {

                    //Anropar metoderna och sparar det som returneras i varsin variabel
                    //(tyckte det kändes smart eller borde man gjort på något annat sätt?) 
                    var windy = Wind();
                    var speedy = Speed();
                    var tilt = Angle();

                    double gravity = 9.81;
                    double calculation1 = 2 * speedy * speedy * Math.Cos(tilt) * Math.Sin(tilt) / gravity;
                    double calculation2 = windy * speedy * Math.Sin(tilt) / gravity;
                    double metersThrown = calculation1 - calculation2;

                    return metersThrown;
                }


                //Räknar ut hur många meter från apan bananen landade
                void TargetCalculation()
                {

                    //Anropar kast-metoden och sparar värdet i en variabel
                    double howLong = LengthCalculation();

                    double landingSpot = startPoint - howLong;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Bananen landade " + landingSpot + " meter från apan!");
                    Console.ResetColor();
                    Console.WriteLine(" \n");
                    HitOrMissCalculation(landingSpot);
                }

                //Räknar ut om det var en träff samt lägger in alla resultat i listan
                void HitOrMissCalculation(double landingSpot)
                {

                    //För att en meter ifrån gäller både framför och bakom apan
                    if (landingSpot >= -1 && landingSpot <= 1)
                    {
                        int hits = 1;
                        var thisRoundsScore = new StuffInList(level, startPoint, landingSpot, hits);
                        ScoreBoard.Add(thisRoundsScore);

                    }
                    else
                    {
                        int hits = 0;
                        var thisRoundsScore = new StuffInList(level, startPoint, landingSpot, hits);
                        ScoreBoard.Add(thisRoundsScore);
                    }
                }
            }
        }


        static void GameOver()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Sammanställning:");
            Console.ResetColor();

            //Skriver ut resultaten som finns lagrade i listan
            foreach (var input in ScoreBoard)
            {
                Console.WriteLine("Bana: " + $"{ input.level} | " + "Avstånd: " + $"{ input.startPoint} | " + "Meter ifrån: " + $"{ input.landingSpot} | " + "Poäng: " + $"{ input.hits} \n");
            }


            //Räknar ut hur många träffar det blivit
            FriendOrFood();
            static void FriendOrFood()
            {
                double caughtTheBanana = 0;
                foreach (var input in ScoreBoard)

                {
                if (input.hits == 1)
                    {
                        caughtTheBanana++;
                    }
                }

                if (caughtTheBanana >= 5)

                {
                    Console.WriteLine("Apan lyckades fånga bananen " + caughtTheBanana + " gånger av 10 :)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Yayy du är apans kompis!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Apan lyckades bara fånga bananen " + caughtTheBanana + " gånger av 10 :(");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + "Ojoj nu tänker apan äta upp dig bara för att du var så dålig på att sikta! \n" +
                    "Spriiing!");
                    Console.ResetColor();
                }
            }
        }


        static void StartOver()
        {
            Console.WriteLine("Vill du spela igen?" + "\n" + "Svara ja eller nej :)");
            string choice = Console.ReadLine();

                if(choice == "ja")
            {
                RestartGame();
            }
                else if(choice == "nej")
            {
                Console.WriteLine("Tack för den här gången, hejdå!");
            }
                else
            {
                Console.WriteLine("Apan hörde inte vad du sa, svara lite tydligare.");
                StartOver();
            }
        }


        //Har förstått att man egentligen borde gjort en while-loop i Main med innehållet i StartOver?
        //Lade jag innehllet i Main fick jag den inte att ladda om för den sista else-satsen
        //Lade jag innehållet som det är nu fick jag inte till hur while-loopen i main kunde avbrytas
        //Så gjorde den här lösningen istället:
        static void RestartGame()
        {
            ScoreBoard.Clear();
            MonkeyComingCloser();
            GameOver();
            StartOver();
        }



        //Vad som ska finnas i listan
        public class StuffInList
        {
            public double level { get; set; } //Bana
            public double startPoint { get; set; } //Banans längd
            public double landingSpot { get; set; } //Hur långt från apan kastet landade
            public int hits { get; set; } //Träff eller inte


            /* Enligt manualen: "The this keyword refers to the current instance of the class
             * and is also used as a modifier of the first parameter of an extension method."
             * Helt ärligt så fattar jag inte alls vad det betyder.
             * Känner att det allra mesta i manualen står så krångligt att man inte är tillräckligt duktig för att tyda det än.
             * Men gissar att det här är nånting som gör att man kan lägga in värdena på rätt plats i listan? */
            public StuffInList(double level, double startPoint, double landingSpot, int hits)
            {
                this.level = level;
                this.startPoint = startPoint;
                this.landingSpot = landingSpot;
                this.hits = hits;
            }
        }



        //Anropas från start
        static void Main(string[] args)
        {
            WelcomePlayer();
            MonkeyComingCloser();
            GameOver();
            StartOver();
        }
    }
}