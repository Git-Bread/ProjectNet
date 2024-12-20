﻿namespace ProjectNet
{
    public class Intro
    {
        //plays the intro before the menu
        public static void PlayIntro()
        {
            //SETUP
            Console.Clear();
            Console.WriteLine();
            Console.Write(" ");
            Console.CursorVisible = false;

            //text block
            #region First Paragraph
            TextFunctions.SlowPrint($"{Settings.rank} ");
            TextFunctions.SlowPrint(Environment.UserName + "...", "green");
            TextFunctions.SlowPrint(" im afraid it has come to this, you are hereby found ");
            TextFunctions.SlowPrint("Guilty, ", "red");
            TextFunctions.SlowPrint("on 3 counts of ");
            TextFunctions.SlowPrint("Adultery, ", "red");
            TextFunctions.SlowPrint("and two counts of ");
            TextFunctions.SlowPrint("Murder ", "red");
            TextFunctions.SlowPrint("aswell as several unverified cases of ");
            TextFunctions.SlowPrint("Unsanctioned ", "red");
            TextFunctions.SlowPrint("duels. ");
            #endregion
            #region Second Paragraph
            Console.WriteLine("\n ");
            Console.Write(" ");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint("Due to these ");
            TextFunctions.SlowPrint("Serius Offences, ", "red");
            TextFunctions.SlowPrint("you should be sentenced to ");
            TextFunctions.SlowPrint("Death... ", "red");
            TextFunctions.SlowPrint("But in the light of your past acts of ");
            TextFunctions.SlowPrint("Heroism and Service ", "green");
            TextFunctions.SlowPrint("you shall be granted leniency by this court.");
            #endregion
            #region Third Paragraph
            Console.WriteLine("\n ");
            Console.Write(" ");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint("By the power vested in me by his holy Emperor \n ");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint("Iskander XXII... ", "yellow");
            TextFunctions.SlowPrint("You are hereby sentenced to a trial by combat, with ");
            TextFunctions.SlowPrint("God ", "yellow");
            TextFunctions.SlowPrint("as your only witness. ");
            TextFunctions.SlowPrint("May you repent for your ");
            TextFunctions.SlowPrint("Crimes ", "crimes");
            TextFunctions.SlowPrint("and prove your innocence before the divine. ");
            #endregion
            #region Fourth Paragraph
            Console.WriteLine("\n ");
            Console.Write(" ");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint("You will leave at dawn for the ");
            TextFunctions.SlowPrint("Dark Tower, ", "purple");
            TextFunctions.SlowPrint("and if you survive its tormented halls, may you ");
            TextFunctions.SlowPrint("return a free man. ", "green");
            TextFunctions.SlowPrint("It pains me to do this, ");
            TextFunctions.SlowPrint("Old Friend. ", "green");
            TextFunctions.SlowPrint("As a last favor to you, for all you have done for me, i shall make sure your ");
            TextFunctions.SlowPrint("Daughter ", "yellow");
            TextFunctions.SlowPrint("gets taken care of.");
            #endregion
            
            Console.WriteLine("\n ");
            Console.Write(" ");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint("Good Luck, Old Friend.", "green");
            Console.WriteLine("\n ");
            Console.Write(" ");
            Settings.wordCounter = 0;
            Console.Write("Press Enter to Continue");

            //running a paralell thread to get a "..." effect at awaiting input, very complicated for the actual effect, but console imposes alot of limitations.
            CancellationTokenSource cts = new();
            CancellationToken token = cts.Token;
            ThreadPool.QueueUserWorkItem(state => TextFunctions.ContinueDotter(token));

            //awaits enter
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    cts.Cancel();
                    break;
                }
            }
        }
    }
}
