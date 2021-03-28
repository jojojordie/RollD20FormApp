using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsFormsApp2;

namespace RollD20WindowsForm
{
    partial class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine("Hello! This is the RollD20 console app developed by Jordan Neumann written in C#.");
            Console.WriteLine("This DnD inspired app rolls die with user input a given number of times.");
            bool rollDice = true;
            while (rollDice)
            {
                AskQuestionsAndRollDice();
                rollDice = RollDiceAgain();
            }
        }

        public static void AskQuestionsAndRollDice()
        {
            int ReturnedDiceType = DiceTypeCollector();
            int ReturnedNumberOfDice = NumberOfDiceCollector();
            Roll(ReturnedDiceType, ReturnedNumberOfDice);
        }

        public static int DiceTypeCollector()
        {
            while (true)
            {
                Console.WriteLine("What kind of dice would you like to roll?");
                Console.WriteLine("Examples include d4, d6, d8, d10, d12, d20, or d100");
                string DiceType = Console.ReadLine();
                DiceType = DiceType.ToLower();
                if (DiceType.Substring(0, 1) == "d")
                {
                    try
                    {
                        string TrimmedDiceType = DiceType.TrimStart('d');
                        int DiceTypeInt = Int32.Parse(TrimmedDiceType);
                        return DiceTypeInt;
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Please format your dice type in the format d + number of sides, ex.d4, d6, d10,...");
                        //Opens DummyFile.txt and append lines. If file is not exists then create and open.
                        File.AppendAllText(@"C:\RollD20Exceptions.txt", ex.Message + "\r\n");
                    }
                }

                else
                {
                    Console.WriteLine("Please format your dice type in the format d + number of sides, ex.d4, d6, d10,...");
                    File.AppendAllText(@"C:\RollD20Exceptions.txt", "The DiceType entered was not a number. It was: " + DiceType + "\r\n");
                }
            }
        }


        public static int NumberOfDiceCollector()
        {
            while (true)
            {
                Console.WriteLine("How many dice would you like to roll?");
                string DiceNumber = Console.ReadLine();
                try
                {
                    int DiceNumberInt = Int32.Parse(DiceNumber);
                    return DiceNumberInt;
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Please enter an integer. Ex. 1, 2, 3, ...");
                    Console.WriteLine("The User Entered " + DiceNumber + " instead of an integer");
                    File.AppendAllText(@"C:\RollD20Exceptions.txt", ex.Message + "\r\n");
                }
            }
        }

        public static void Roll(int ReturnedDiceType, int ReturnedNumberOfDice)
        {
            Console.WriteLine("Rolling dice...");
            List<Dice> dieRolls = new List<Dice>();
            int i;
            for (i = 0; i < ReturnedNumberOfDice; i++)
            {
                Dice die = new Dice();
                die.NumberOfSides = ReturnedDiceType;
                die.DiceRoller();
                dieRolls.Add(die);
                Console.Write(die.Result + " ");
            }

            Console.WriteLine("");

            var queryNatHighestNumber = from die in dieRolls
                                        where die.Result == ReturnedDiceType
                                        select die;

            var NumberOfHighestRolls = 0;

            foreach (Dice die in queryNatHighestNumber)
            {
                NumberOfHighestRolls++;
            }

            string Ending = "";

            if (NumberOfHighestRolls > 0)
            {
                Ending = "Congrats!";
            }

            else
            {
                Ending = "Oops!";
            }

            Console.WriteLine("You rolled the highest number for a dice with " + ReturnedDiceType + " sides " + NumberOfHighestRolls + " times! " + Ending);
            Form1 DiceRollForm= new Form1();
            DiceRollForm.RollResult = dieRolls;
            DiceRollForm.DiceRollsFormFiller();
            Application.Run(DiceRollForm);
        }

        static bool RollDiceAgain()
        {
            while (true)
            {
                Console.WriteLine("Would you like to roll again? Type 'y' or 'yes' for yes, and 'n' or 'no' for no");
                string rollDiceAgain = Console.ReadLine();
                rollDiceAgain = rollDiceAgain.ToLower();
                if (rollDiceAgain == "y" || rollDiceAgain == "yes")
                {
                    return true;
                }
                else if (rollDiceAgain == "n" || rollDiceAgain == "no")
                {
                    Console.WriteLine("Thanks for using my app!");
                    return false;
                }
            }
        }
    }
}
