namespace ProjectNet
{
    public class Menu
    {
        public static void MenuDisplay()
        {
            //FIRST PARAGRAPH
            Console.Clear();
            Console.WriteLine();
            Console.CursorVisible = false;
            TextFunctions.SlowPrint(" You stand before ");
            TextFunctions.SlowPrint("God... ", "yellow");
            TextFunctions.SlowPrint("and he offers you a short respite, ");
            TextFunctions.SlowPrint("a moment to think about the ");
            TextFunctions.SlowPrint("Inevitable. ", "red");
            TextFunctions.SlowPrint("At this crossroad of faith you may ");
            TextFunctions.SlowPrint("ATONE (Start/Continue), ", "green");
            TextFunctions.SlowPrint("WORSHIP (Change Settings)...", "yellow");

            //SECOND PARAGRAPH
            Console.WriteLine("\n");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint(" Or if despair has taken root in your heart, you may, ");
            TextFunctions.SlowPrint("OFFER YOUR UNWORTHY SOUL (Reset Save). ", "red");
            Console.WriteLine("\n");

            //OPTIONS
            settings.wordcounter = 0;
            TextFunctions.SlowPrint($" What will you do {settings.rank}?");
            settings.wordcounter = 0;
            Console.WriteLine("\n");
            TextFunctions.SlowPrint(" 1. ATONE ", "green");
            Console.WriteLine();
            TextFunctions.SlowPrint(" 2. WORSHIP ", "yellow");
            Console.WriteLine();
            TextFunctions.SlowPrint(" 3. END IT ", "red");
            Console.WriteLine("\n");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint(" The ");
            TextFunctions.SlowPrint("Lord, ", "yellow");
            TextFunctions.SlowPrint("awaits your choice");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            ThreadPool.QueueUserWorkItem(state => TextFunctions.ContinueDotter(token));

            //SELECT OPTIONS
            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.D1)
                {
                    cts.Cancel();
                    break;
                }
                else if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.D2)
                {
                    cts.Cancel();
                    break;
                }
                else if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.D3)
                {
                    cts.Cancel();
                    break;
                }
            }
        }
        public static void DisplaySettings()
        {
            HolyScripture.ReadScripture();

        }
    }
}