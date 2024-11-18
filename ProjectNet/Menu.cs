namespace ProjectNet
{
    public class Menu
    {
        public static void MenuDisplay()
        {
            if (Settings.wasInSettings[1])
            {
                Settings.speed = 100;
            }

            #region First Paragraph
            Console.Clear();
            Console.WriteLine();
            Console.CursorVisible = false;
            TextFunctions.SlowPrint(" You stand before ");
            TextFunctions.SlowPrint("God... ", "yellow");
            if (CharacterSheet.floor.level != -1)
            {
                TextFunctions.SlowPrint("you are back here ");
                TextFunctions.SlowPrint("again, ", "yellow");
                TextFunctions.SlowPrint("stuck in thought, pondering about the ");
                TextFunctions.SlowPrint("Inevitable. ", "red");
            }
            else
            {
                TextFunctions.SlowPrint("and he offers you a short respite, ");
                TextFunctions.SlowPrint("a moment to think about the ");
                TextFunctions.SlowPrint("Inevitable. ", "red");
            }
            TextFunctions.SlowPrint("At this crossroad of faith you may ");
            TextFunctions.SlowPrint("ATONE (Start/Continue), ", "green");
            TextFunctions.SlowPrint("WORSHIP (Change Settings)...", "yellow");
            #endregion
            #region Second Paragraph
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" Or if despair has taken root in your heart, you may, ");
            TextFunctions.SlowPrint("Escape This Mortal Coil (Reset Save). ", "red");
            Console.WriteLine("\n");
            #endregion
            #region Options
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint($" What will you do {Settings.rank}?");
            Settings.wordCounter = 0;
            Console.WriteLine("\n");
            TextFunctions.SlowPrint(" 1. ATONE ", "green");
            Console.WriteLine();
            TextFunctions.SlowPrint(" 2. WORSHIP ", "yellow");
            Console.WriteLine();
            TextFunctions.SlowPrint(" 3. END IT ", "red");
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" The ");
            TextFunctions.SlowPrint("Lord, ", "yellow");
            TextFunctions.SlowPrint("awaits your choice");
            #endregion

            if (Settings.wasInSettings[1])
            {
                Saver.LoadSettings();
            }

            CancellationTokenSource cts = new();
            CancellationToken token = cts.Token;
            ThreadPool.QueueUserWorkItem(state => TextFunctions.ContinueDotter(token));

            //SELECT OPTIONS
            while(true) {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    cts.Cancel();
                    RandomText.Scripture();
                    GameStart.Introduction();
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
                    Saver.RemoveSave();
                    System.Environment.Exit(0);
                    break;
                }
            }
        }
        public static void DisplaySettings()
        {
            if (Settings.wasInSettings[0])
            {
                Saver.SaveSettings();
                Settings.speed = 100;
            }

            #region Settings
            Console.Clear();
            Console.WriteLine();
            Console.CursorVisible = false;
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" You offer your heartfelt prayers, and ");
            TextFunctions.SlowPrint("God ", "yellow");
            TextFunctions.SlowPrint("responds with a holy relevation. ");
            TextFunctions.SlowPrint("You may momentarely change the fabric of this world, use it wisely.");

            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" Select what faults you want to ammend:");

            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            #endregion

            if (Settings.wasInSettings[0])
            {
                Saver.LoadSettings();
            }
            TextFunctions.SlowPrint($" 1. Skip intro sequence = {Settings.skipIntro}");
            Console.WriteLine();

            TextFunctions.SlowPrint($" 2. Text Speed = {Settings.speed}");
            Settings.wordCounter = 0;
            Console.WriteLine("\n");
            TextFunctions.SlowPrint($" 3. Return", "green");
            Console.WriteLine("\n");

            Console.Write(" ");
            //SELECT OPTIONS
            bool hold = true;
            while (hold)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch(key.Key)
                {
                    case ConsoleKey.D1:
                        if(Settings.skipIntro)
                        {
                            Settings.skipIntro = false;
                        }
                        else
                        {
                            Settings.skipIntro = true;
                        }
                        Settings.wasInSettings[0] = true;
                        DisplaySettings();
                        break;
                    case ConsoleKey.D2:
                        Settings.wordCounter = 0;
                        Console.WriteLine();
                        TextFunctions.SlowPrint(" Enter a new value, this modifier is a divider of the total time \n");
                        Console.WriteLine();
                        Console.Write(" ");
                        while (true)
                        {
                            string value = Console.ReadLine() ?? "";
                            if (value == "" || !Int32.TryParse(value, out int x))
                            {
                                TextFunctions.SlowPrint(" Invalid input, only numbers above 0 are legal \n");
                                Console.Write(" ");
                                continue;
                            }
                            Settings.speed = x;
                            Settings.wasInSettings[0] = true;
                            DisplaySettings();
                            break;
                        }
                        break;
                    case ConsoleKey.D3:
                        hold = false;
                        break;
                }
            }
            Settings.wasInSettings[1] = true;
            MenuDisplay();
        }
    }
}