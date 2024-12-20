﻿namespace ProjectNet
{
    internal class RandomText
    {
       //creates arbitary "made up" religious looking quotes, that works as loading screens. Because all good games have loading screens so mine will have one aswell, woe upon the user
        public static void Scripture()
        {
            //lots of text for flavour, probaly a waste of time
            #region Scriptures
            string[] Scriptures = {
            " The Lord giveth, and the lord taketh, ask not what the lord can do for you, ask what you can do for the Lord. - Salem 7:2",
            " Through holy penance and exhorting purity among his flock, a lost soul may once again enter his warm embrace. - Salem 7:8",
            " This world is an illusion exile, have you ever seen the true face of God? - Archprelate Dominus 'The Hand Of God'",
            " Guide your flock with the fervor of the Fanatic, execute his will with the fury of the Flaggelant, and only then may Heaven grace this lowly plane. - Hierodommus 12:2",
            " The Earth is the lowest plane, the center of the celestial bodies, all rotate around earth and all will eventually fall to the Earth. Thats why the Earth is polluted with Sin, while the heavens remains pure. - Hierodommus 17:7",
            " EXALT YOUR FLESH FOR THE LORD, SURRENDER YOUR EXISTANCE TO HIS LOVING EMBRACE. - Unknown Fanatic",
            " Treat your neighbor like your family, and strangers like your neighbor. Eventualy his righteous shall ascend, but untill then we must evoke his image here on Earth - Hierodommus 4:7",
            " It is the job of the Peasant to tend the fields, to offer the tithes, while its the job of the clergy to ensure the laypeople do not stray from his divine design - Calix 20:3",
            " I crave for the certanty and strenght of steel, I aspire for the purity of the blessed machine, when that body you call a temple decays and fails you. You will come to me for salvation, but I am already saved... Even in death I serve the Omnissiah - Unknown Heathen",
            " Let not the righteous falter, weakness leads to sin, and sin leads to depravity. Stand tall and remember, you are his agents on this world, his holy Justice. Remain pure in spirit, devout in labour and fair in rule. Only then, the Lord will smile upon you. - High Proctor Isobel",
            };
            #endregion

            //randomizer for what text to pick
            Random random = new();
            Console.Clear();
            Console.WriteLine("");
            Settings.wordCounter = 0;
            Settings.lineSize *= 2; //doubles the linesize for this part
            int randomNumber = random.Next(0, Scriptures.Length);
            TextFunctions.SlowPrint(Scriptures[randomNumber]);
            Settings.lineSize /= 2; //halves the linesize

            //awaits enter
            Console.WriteLine("\n");
            Settings.wordCounter = 0;
            TextFunctions.SlowPrint(" Press Enter to Continue");

            CancellationTokenSource cts = new();
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