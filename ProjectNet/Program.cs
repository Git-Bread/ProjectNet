namespace ProjectNet
{
    public class TextGame
    {
        public static void Main(string[] args)
        {
            Saver.LoadSettings();
            Saver.LoadGame();
            //error handling
            if (!File.Exists("config/monsters.json"))
            {
                Console.WriteLine("NO MONSTERS FILE, PLEASE CHECK INSTALL");
                System.Environment.Exit(0);
            };
            //error handling
            if (!File.Exists("config/character.json"))
            {
                Console.WriteLine("NO CHARACTER FILE, DO YOU WANT TO CREATE ONE? (WILL EXIT UPON COMPLETION)");
                Console.WriteLine();
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                bool run = true;
                while (run)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.D1)
                    {
                        File.Create("config/character.json");
                        System.Environment.Exit(0);
                        break;
                    }
                    else if (key.Key == ConsoleKey.D2)
                    {
                        Console.WriteLine("PLEASE CHECK INSTALLATION");
                        System.Environment.Exit(0);
                        break;
                    }
                }
            };

            if (!File.Exists("config/settings.json"))
            {
                Console.WriteLine("NO SETTINGS FILE, DO YOU WANT TO CREATE ONE? (WILL EXIT UPON COMPLETION)");
                Console.WriteLine();
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                bool run = true;
                while (run)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.D1)
                    {
                        File.Create("config/settings.json");
                        System.Environment.Exit(0);
                        break;
                    }
                    else if (key.Key == ConsoleKey.D2)
                    {
                        Console.WriteLine("PLEASE CHECK INSTALLATION");
                        System.Environment.Exit(0);
                        break;
                    }
                }
            };


            //console windows only for resize, linux be damned
            Console.Title = "The Dark Tower";


            if (!Settings.skipIntro)
            {
                #region Intro
                Console.WriteLine();
                TextFunctions.RowPrint(@"  _________  ___  ___  _______           ________  ________  ________  ___  __       ");
                TextFunctions.RowPrint(@" |\___   ___\\  \|\  \|\  ___ \         |\   ___ \|\   __  \|\   __  \|\  \|\  \   ");
                TextFunctions.RowPrint(@" \|___ \  \_\ \  \\\  \ \   __/|        \ \  \_|\ \ \  \|\  \ \  \|\  \ \  \/  /|_   ");
                TextFunctions.RowPrint(@"      \ \  \ \ \   __  \ \  \_|/__       \ \  \ \\ \ \   __  \ \   _  _\ \   ___  \  ");
                TextFunctions.RowPrint(@"       \ \  \ \ \  \ \  \ \  \_|\ \       \ \  \_\\ \ \  \ \  \ \  \\  \\ \  \\ \  \ ");
                TextFunctions.RowPrint(@"        \ \__\ \ \__\ \__\ \_______\       \ \_______\ \__\ \__\ \__\\ _\\ \__\\ \__\");
                TextFunctions.RowPrint(@"         \|__|  \|__|\|__|\|_______|        \|_______|\|__|\|__|\|__|\|__|\|__| \|__|");
                TextFunctions.RowPrint(@"  _________  ________  ___       __   _______   ________  ");
                TextFunctions.RowPrint(@" |\___   ___\\   __  \|\  \     |\  \|\  ___ \ |\   __  \  ");
                TextFunctions.RowPrint(@" \|___ \  \_\ \  \|\  \ \  \    \ \  \ \   __/|\ \  \|\  \   ");
                TextFunctions.RowPrint(@"      \ \  \ \ \  \\\  \ \  \  __\ \  \ \  \_|/_\ \   _  _\  ");
                TextFunctions.RowPrint(@"       \ \  \ \ \  \\\  \ \  \|\__\_\  \ \  \_|\ \ \  \\  \|   ");
                TextFunctions.RowPrint(@"        \ \__\ \ \_______\ \____________\ \_______\ \__\\ _\    ");
                TextFunctions.RowPrint(@"         \|__|  \|_______|\|____________|\|_______|\|__|\|__|        ");
                Console.WriteLine();
                Thread.Sleep(2000);
                TextFunctions.SlowPrint(@" - A Demo by maga2101");
                Settings.wordCounter = 0;
                Thread.Sleep(2000);

                Console.Clear();
                Console.WriteLine();
                TextFunctions.SlowPrint(" In this demo it is recomended to not interact with the ");
                TextFunctions.SlowPrint("Console ", "yellow");
                TextFunctions.SlowPrint("while text is being written, when no text has been written for a second its safe to assume that it needs input. The game saves its progress at every mayor progress milestone and loads automatically on launch.");
                Settings.wordCounter = 0;
                Console.WriteLine();
                Console.WriteLine();
                Console.Write(" Press Enter to Continue");
                #endregion

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
                if(CharacterSheet.floor.level != -1)
                {
                    Intro.PlayIntro();
                }
            }
            Menu.MenuDisplay();
        }
    }
}