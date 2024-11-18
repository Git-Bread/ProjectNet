namespace ProjectNet
{
    //welcome to the hall of stylistic inefficency
    class TextFunctions
    {
        //writes dots with a cancellation token
        public static void ContinueDotter(CancellationToken token)
        {
            int dot = 0;
            //if not cancelled
            while (!token.IsCancellationRequested)
            {
                Console.Write(".");
                dot++;
                Thread.Sleep(2000);
                if(token.IsCancellationRequested)
                {
                    break;
                }
                //it has printed three dots, remove the last three symbols
                if (dot == 3)
                {
                    dot = 0;
                    Console.Write("\b \b\b \b\b \b");
                }
            }
        }

        //prints a whole row with a small delay
        internal static void RowPrint(string text)
        {
            Console.WriteLine(text);
            Thread.Sleep(100);
        }

        //here it lay, the abomination of printing in-efficiency, converts all nice print lines into a veritable barrage of print statements for every symbol in the string. ITS extremely inefficient, but its pretty
        //takest a string asweell as a optional highlight to color said string
        public static void SlowPrint(string print, string highlight = "")
        {
            //base print speed divided by user speed
            var speed = 60 / Settings.speed;
            //if colored it needs to remove the color
            var reset = false;

            //text color switch
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

            //the most abused for loop int the program, uses the string as a char array, ends line if wordcounter is hit and a space is found,
            //if a "," or "." is found it adds a small delay, all to create the illusion of printing letter for letter.
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