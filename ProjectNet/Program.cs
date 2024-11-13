namespace ProjectNet
{
    public class TextGame
    {
        public static void Main(string[] args)
        {
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
                TextFunctions.SlowPrint("unless the program asks for it.");
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

                Intro.PlayIntro();
            }
            Menu.MenuDisplay();
        }
    }
}