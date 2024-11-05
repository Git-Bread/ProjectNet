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
                Thread.Sleep(1500);
                if (dot == 3)
                {
                    dot = 0;
                    Console.Write("\b \b\b \b\b \b");
                }
            }
        }

        public static void SlowPrint(string print, string highlight = "")
        {
            var speed = 60 / settings.speed;
            var reset = false;
            if (highlight == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                reset = true;
            }
            else if (highlight == "purple")
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                reset = true;
            }
            else if (highlight == "green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                reset = true;
            }
            else if (highlight == "yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                reset = true;
            }
            for (int i = 0; i < print.Length; i++)
            {
                settings.wordcounter++;
                if (settings.wordcounter >= settings.lineSize && print[i] == ' ')
                {
                    Console.WriteLine();
                    settings.wordcounter = 0;
                }
                Console.Write(print[i]);
                Thread.Sleep(speed);
                if (print[i] == ',' || print[i] == '.')
                {
                    Thread.Sleep(speed * 3);
                }
            }
            if (reset)
            {
                Console.ResetColor();
            }
        }
    }
}    