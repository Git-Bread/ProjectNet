namespace ProjectNet
{
    public class Menu
    {
        public static void MenuDisplay()
        {
            if(settings.WasInSettings[1])
            {
                settings.oldSpeed = settings.speed;
                settings.speed = 100;
            }

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

            if (settings.WasInSettings[1])
            {
                settings.speed = settings.oldSpeed;
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            ThreadPool.QueueUserWorkItem(state => TextFunctions.ContinueDotter(token));
            Console.Write(" ");

            //SELECT OPTIONS
            while(true) {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    cts.Cancel();
                    HolyScripture.Scripture();
               
                    break;
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    cts.Cancel();
                    DisplaySettings();
                    break;
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    cts.Cancel();
                    break;
                }
            }
        }
        public static void DisplaySettings()
        {
            if (settings.WasInSettings[0])
            {
                settings.oldSpeed = settings.speed;
                settings.speed = 100;
            }

            //SETTINGS
            //FIRST PARAGRAPH
            Console.Clear();
            Console.WriteLine();
            Console.CursorVisible = false;
            settings.wordcounter = 0;
            TextFunctions.SlowPrint(" You offer your hearfelt prayers, and ");
            TextFunctions.SlowPrint("God ", "yellow");
            TextFunctions.SlowPrint("responds with a holy relevation. ");
            TextFunctions.SlowPrint("You may momentarely change the fabric of this world, use it wisely.");

            Console.WriteLine("\n");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint(" Select what faults you want to ammend:");

            Console.WriteLine("\n");
            settings.wordcounter = 0;
            TextFunctions.SlowPrint($" 1. Skip intro sequence = {settings.SkipIntro}");
            Console.WriteLine();
            if (settings.oldSpeed == 0)
            {
                settings.oldSpeed = settings.speed;
            }
            TextFunctions.SlowPrint($" 2. Text Speed = {settings.oldSpeed}");
            settings.wordcounter = 0;
            Console.WriteLine("\n");
            TextFunctions.SlowPrint($" 3. Return");
            Console.WriteLine("\n");

            if (settings.WasInSettings[0])
            {
                settings.speed = settings.oldSpeed;
            }
            Console.Write(" ");
            //SELECT OPTIONS
            bool hold = true;
            while (hold)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch(key.Key)
                {
                    case ConsoleKey.D1:
                        if(settings.SkipIntro)
                        {
                            settings.SkipIntro = false;
                        }
                        else
                        {
                            settings.SkipIntro = true;
                        }
                        settings.WasInSettings[0] = true;
                        DisplaySettings();
                        break;
                    case ConsoleKey.D2:
                        settings.wordcounter = 0;
                        Console.WriteLine();
                        TextFunctions.SlowPrint(" Enter a new value, this modifier is a divider of the total time \n");
                        Console.WriteLine();
                        Console.Write(" ");
                        while (true)
                        {
                            string value = Console.ReadLine() ?? "";
                            int x;
                            if (value == "" || !Int32.TryParse(value, out x))
                            {
                                TextFunctions.SlowPrint("Invalid input, only numbers above 0 are legal \n");
                                continue;
                            }
                            settings.speed = x;
                            settings.WasInSettings[0] = true;
                            DisplaySettings();
                            break;
                        }
                        break;
                    case ConsoleKey.D3:
                        hold = false;
                        break;
                }
            }
            settings.WasInSettings[1] = true;
            MenuDisplay();
        }
    }
}