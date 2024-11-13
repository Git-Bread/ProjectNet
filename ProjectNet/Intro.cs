namespace ProjectNet
{
    public class Intro
    {
        public static void PlayIntro()
        {
            //SETUP
            Console.WriteLine();
            Console.Write(" ");
            Console.CursorVisible = false;

            //FIRST PARAGRAPH
            TextFunctions.SlowPrint($"{settings.rank} ");
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

            //SECOND PARAGRAPH
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint("Due to these ");
            TextFunctions.SlowPrint("Serius Offences, ", "red");
            TextFunctions.SlowPrint("you should be sentenced to ");
            TextFunctions.SlowPrint("Death... ", "red");
            TextFunctions.SlowPrint("But in the light of your past acts of ");
            TextFunctions.SlowPrint("Heroism and Service ", "green");
            TextFunctions.SlowPrint("you shall be granted leniency by this court.");

            //THIRD PARAGRAPH
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint("By the power vested in me by his holy Emperor \n ");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint("Iskander XXII... ", "yellow");
            TextFunctions.SlowPrint("You are hereby sentenced to a trial by combat, with ");
            TextFunctions.SlowPrint("God ", "yellow");
            TextFunctions.SlowPrint("as your only witness. ");
            TextFunctions.SlowPrint("May you repent for your ");
            TextFunctions.SlowPrint("Crimes ", "crimes");
            TextFunctions.SlowPrint("and prove your innocence before the divine. ");

            //LAST PARAGRAPH
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint("You will leave at dawn for the ");
            TextFunctions.SlowPrint("Dark Tower, ", "purple");
            TextFunctions.SlowPrint("and if you survive its tormented halls, may you ");
            TextFunctions.SlowPrint("return a free man. ", "green");
            TextFunctions.SlowPrint("It pains me to do this, ");
            TextFunctions.SlowPrint("Old Friend. ", "green");
            TextFunctions.SlowPrint("As a last favor to you, for all you have done for me, i shall make sure your ");
            TextFunctions.SlowPrint("Daughter ", "yellow");
            TextFunctions.SlowPrint("gets taken care of.");

            //END OF INTRO
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint("Good Luck, Old Friend.", "green");
            Console.WriteLine("\n ");
            Console.Write(" ");
            settings.wordcounter = 0;
            Console.Write("Press Enter to Continue");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            ThreadPool.QueueUserWorkItem(state => TextFunctions.ContinueDotter(token));

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    cts.Cancel();
                    break;
                }
            }
        }
    }
}