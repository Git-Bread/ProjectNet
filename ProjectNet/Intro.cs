namespace ProjectNet
{
    public class intro
    {
        public static void playIntro()
        {
            //SETUP
            Console.WriteLine();
            Console.Write(" ");
            Console.CursorVisible = false;

            //FIRST PARAGRAPH
            TextFunctions.slowPrint("Baron ");
            TextFunctions.slowPrint(Environment.UserName + "...", "green");
            TextFunctions.slowPrint(" im afraid it has come to this, you are hereby found ");
            TextFunctions.slowPrint("Guilty, ", "red");
            TextFunctions.slowPrint("on 3 counts of ");
            TextFunctions.slowPrint("Adultery, ", "red");
            TextFunctions.slowPrint("and two counts of, ");
            TextFunctions.slowPrint("Murder ", "red");
            TextFunctions.slowPrint("aswell as several unverified cases of ");
            TextFunctions.slowPrint("Unsanctioned ", "red");
            TextFunctions.slowPrint("duels. ");

            //SECOND PARAGRAPH
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.slowPrint("Due to these ");
            TextFunctions.slowPrint("Serius Offences, ", "red");
            TextFunctions.slowPrint("you should be sentenced to ");
            TextFunctions.slowPrint("Death... ", "red");
            TextFunctions.slowPrint("But in the light of your past acts of ");
            TextFunctions.slowPrint("Heroism and Service ", "green");
            TextFunctions.slowPrint("you shall be granted leniency by this court.");

            //THIRD PARAGRAPH
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.slowPrint("By the power vested in me by his holy Emperor \n ");
            settings.wordcounter = 0;
            TextFunctions.slowPrint("Iskander XXII... ", "yellow");
            TextFunctions.slowPrint("You are hereby sentenced to a trial by combat, with ");
            TextFunctions.slowPrint("God ", "yellow");
            TextFunctions.slowPrint("as your only witness. ");
            TextFunctions.slowPrint("May you repent for your ");
            TextFunctions.slowPrint("Crimes ", "crimes");
            TextFunctions.slowPrint("and prove your innocence before the divine. ");

            //LAST PARAGRAPH
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.slowPrint("You will leave at dawn for the ");
            TextFunctions.slowPrint("Dark Tower, ", "purple");
            TextFunctions.slowPrint("and if you survive its tormented halls, may you ");
            TextFunctions.slowPrint("return a free man. ", "green");
            TextFunctions.slowPrint("It pains me to do this, ");
            TextFunctions.slowPrint("Old Friend. ", "green");
            TextFunctions.slowPrint("As a last favor to you, for all you have done for me, i shall make sure your ");
            TextFunctions.slowPrint("Daughter ", "yellow");
            TextFunctions.slowPrint("gets taken care of.");

            //END OF INTRO
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.slowPrint("Good Luck, Old Friend.", "green");
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            Console.Write("Press Enter to Continue");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            ThreadPool.QueueUserWorkItem(state => TextFunctions.continueDotter(token));

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    cts.Cancel();
                    break;
                }
            }
            Console.CursorVisible = true;
        }
    }
}