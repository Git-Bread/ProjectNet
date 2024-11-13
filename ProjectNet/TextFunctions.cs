namespace ProjectNet
{
    class TextFunctions
    {
        public static void ContinueDotter(CancellationToken token)
        {
            int dot = 0;
            while (!token.IsCancellationRequested)
            {
                Console.Write(".");
                dot++;
                Thread.Sleep(2000);
                if(token.IsCancellationRequested)
                {
                    break;
                }
                if (dot == 3)
                {
                    dot = 0;
                    Console.Write("\b \b\b \b\b \b");
                }
            }
        }

        internal static void RowPrint(string text)
        {
            Console.WriteLine(text);
            Thread.Sleep(100);
        }

        public static void SlowPrint(string print, string highlight = "")
        {
            var speed = 60 / Settings.speed;
            var reset = false;

            switch (highlight)
            {
                default: break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    reset = true;
                    break;
                case "purple":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    reset = true;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    reset = true;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    reset = true;
                    break;
                case "brown":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    reset = true;
                    break;
                case "white":
                    Console.ForegroundColor = ConsoleColor.White;
                    reset = true;
                    break;
            }

            for (int i = 0; i < print.Length; i++)
            {
                Settings.wordCounter++;
                if (Settings.wordCounter >= Settings.lineSize && print[i] == ' ')
                {
                    Console.WriteLine();
                    Settings.wordCounter = 0;
                }
                Console.Write(print[i]);
                Thread.Sleep(speed);
                if (print[i] == ',' || print[i] == '.')
                {
                    Thread.Sleep(600 / Settings.speed);
                }
            }
            if (reset)
            {
                Console.ResetColor();
            }
        }
    }
}    