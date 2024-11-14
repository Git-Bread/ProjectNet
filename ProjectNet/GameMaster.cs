namespace ProjectNet
{
    public class GameMaster
    {
        public static void RunGame()
        {
            Console.Clear();
            CharacterSheet.floor = GameGen.GenerateFloor();
            #region tutorial
            if (CharacterSheet.floor.level == 0)
            {
                Console.WriteLine();
                TextFunctions.RowPrint(" Welcome to the actual game, you will interact with the diffrent levels of the tower ");
                TextFunctions.RowPrint(" and perform actions based on your input. The actions you can do are as follows, with example imputs:");
                Console.WriteLine();
                TextFunctions.RowPrint(" {ascend, go up, climb the stairs} Ascend the stairs to the next floor, need to beat the enemy first however.");
                TextFunctions.RowPrint(" {proceed, march, press forward} Go forward and engage the opponent");
                TextFunctions.RowPrint(" {survey, look around, observe} Look around, see if something catches your eyes");
                TextFunctions.RowPrint(" {rest, sit down, take a break} Rest, helps if theres something to rest on.");
                TextFunctions.RowPrint(" {open, unlock, remove the lid} Opens a chest or container");
                Console.WriteLine("\n");
                Settings.wordCounter = 0;
                TextFunctions.RowPrint(" When combat starts you may either perform a Slashing/Swiping or a Piercing/Thrusting attack.");
                TextFunctions.RowPrint(" The effectivness of your attack depends on luck, your stats aswell as the weapon/move you use,");
                TextFunctions.RowPrint(" as an example, slashing attacks will do much less against a golem than piercing");
                Console.WriteLine("\n");
                TextFunctions.SlowPrint(" Press Enter to Continue");
                Settings.wordCounter = 0;

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
            #endregion

            Console.Clear();
            while(true)
            {
                Floor floor = CharacterSheet.floor;
                bool monsterDefeated = false;
                Random rand = new Random();
                #region Standard Description
                Settings.wordCounter = 0;
                Console.WriteLine();
                TextFunctions.SlowPrint(" You are in an square room with a narrow hallway leading forward... ");
                TextFunctions.SlowPrint("At the end of the passage you see another room and a ");
                TextFunctions.SlowPrint($"{floor.opponent.Name}, ", "red");
                TextFunctions.SlowPrint("looking menacingly at you. ");
                Settings.wordCounter = 0;
                Console.WriteLine("\n");
                #endregion
                bool playing = true;
                while(playing)
                {
                    Console.WriteLine();
                    Console.Write(" ");
                    float action = Predicter.Determine(Console.ReadLine() ?? "null");
                    Console.Write(" ");
                    Console.WriteLine(action);
                    Console.WriteLine("\n");
                    string restFactor = "none";
                    int heatFactor = 1;
                    int chest = 3;
                    bool lookedAround = false;

                    switch (action)
                    {
                        case 1:
                            if (monsterDefeated)
                            {
                                playing = false;
                            }
                            else {
                                TextFunctions.SlowPrint(" You cannot leave while the ");
                                TextFunctions.SlowPrint($"{floor.opponent.Name} ", "red");
                                TextFunctions.SlowPrint("still lurks these halls.");
                                Settings.wordCounter = 0;
                                Console.WriteLine("\n");
                            }
                            break;
                        case 2:
                            if(!monsterDefeated)
                            {
                                RunCombat();
                            }
                            else {
                                TextFunctions.SlowPrint(" The opponent has been vanquished, ");
                                TextFunctions.SlowPrint($"{floor.opponent.Name} ", "red");
                                TextFunctions.SlowPrint("is no more, you may go to the next level.");
                                Settings.wordCounter = 0;
                                Console.WriteLine("\n");
                            }
                            break;
                        case 3:
                            if (floor.objects.Count > 0)
                            {
                                if(lookedAround)
                                {
                                    TextFunctions.SlowPrint(" You have already looked around.");
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    break;
                                }
                                TextFunctions.SlowPrint(" You look around the room...");
                                Settings.wordCounter = 0;
                                Console.WriteLine("\n");
                                for (int i = 0; i < floor.objects.Count; i++)
                                {
                                    switch (floor.objects[i])
                                    {
                                        case "Table":
                                            TextFunctions.SlowPrint(" A rudimentary wooden table dominates the room. ");
                                            TextFunctions.SlowPrint("The table is relatively featureless and has a quaint ");
                                            TextFunctions.SlowPrint("Wooden Chair ", "brown");
                                            TextFunctions.SlowPrint("that accomppanys it.");
                                            Settings.wordCounter = 0;
                                            restFactor = "chair";
                                            Console.WriteLine("\n");
                                            break;
                                        case "Chair":
                                            TextFunctions.SlowPrint(" A large armchair catches your eyes, even in this damp and dark place it somehow ");
                                            TextFunctions.SlowPrint("seems to be in good condition. ");
                                            TextFunctions.SlowPrint("You could need the ");
                                            TextFunctions.SlowPrint("Rest ", "green");
                                            TextFunctions.SlowPrint("that the armchairs presence silently proposes. ");
                                            Settings.wordCounter = 0;
                                            restFactor = "comfyChair";
                                            Console.WriteLine("\n");
                                            break;
                                        case "Bed":
                                            TextFunctions.SlowPrint(" In the corner of the room is a normal bed, not unlike those found in normal taverns and barracks, it even passes the flea-check. ");
                                            TextFunctions.SlowPrint("Sleeping for a bit ", "green");
                                            TextFunctions.SlowPrint("might be nice.");
                                            Settings.wordCounter = 0;
                                            restFactor = "bed";
                                            Console.WriteLine("\n");
                                            break;
                                        case "Fireplace":
                                            TextFunctions.SlowPrint(" You spot a gray stone fireplace in the corner of the room, it even has wood and matches already prepared, ");
                                            TextFunctions.SlowPrint("Convinent! ", "yellow");
                                            TextFunctions.SlowPrint("You light the fireplace without much resistance, its logs practicaly asking to be burned. ");
                                            TextFunctions.SlowPrint("You feel its ");
                                            TextFunctions.SlowPrint("Warmth ", "red");
                                            TextFunctions.SlowPrint("spreading across your body. ");
                                            Settings.wordCounter = 0;
                                            heatFactor++;
                                            Console.WriteLine("\n");
                                            break;
                                        case "Chest":
                                            TextFunctions.SlowPrint(" A suspicius iron chest stands right next to the entrance, practicly begging to be opened. ");
                                            TextFunctions.SlowPrint("But who knows what it contains? ", "white");
                                            Settings.wordCounter = 0;
                                            Console.WriteLine("\n");
                                            chest = rand.Next(2);
                                            break;
                                    }
                                }
                                lookedAround = true;
                            }
                            else
                            {
                                TextFunctions.SlowPrint(" Theres nothing of intrest nearby");
                                Settings.wordCounter = 0;
                                Console.WriteLine("\n");
                            }
                            break;
                        case 4:
                            switch(restFactor)
                            {
                                case "none":
                                    TextFunctions.SlowPrint(" You lay down on the stone tiles, it might not be ");
                                    TextFunctions.SlowPrint("Comfortable. ", "white");
                                    TextFunctions.SlowPrint("But it will have to do.");
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    if (CharacterSheet.life < 5 + CharacterSheet.strength)
                                    {
                                        int recover = rand.Next(2) * heatFactor;
                                        if (CharacterSheet.life + recover > 5 + CharacterSheet.strength)
                                        {
                                            CharacterSheet.life = 5 + CharacterSheet.strength;
                                        }
                                        else
                                        {
                                            CharacterSheet.life += recover;
                                        }
                                        TextFunctions.SlowPrint($" You recover {recover} HP, ", "green");
                                        TextFunctions.SlowPrint("however the ");
                                        TextFunctions.SlowPrint("back pain ", "red");
                                        TextFunctions.SlowPrint("will take a while to go away.");
                                        CharacterSheet.pain += 1;
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                    }
                                    break;
                                case "chair":
                                    TextFunctions.SlowPrint(" You sit down on the chair, ");
                                    TextFunctions.SlowPrint("its alright. ", "white");
                                    TextFunctions.SlowPrint("Not the worst resting spot you have ever had.");
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    if (CharacterSheet.life < 5 + CharacterSheet.strength)
                                    {
                                        int recover = rand.Next(3) * heatFactor;
                                        if (CharacterSheet.life + recover > 5 + CharacterSheet.strength)
                                        {
                                            CharacterSheet.life = 5 + CharacterSheet.strength;
                                        }
                                        else
                                        {
                                            CharacterSheet.life += recover;
                                        }
                                        if(CharacterSheet.pain > 0)
                                        {
                                            CharacterSheet.pain -= 1;
                                            TextFunctions.SlowPrint(" The rest made some pain go away.", "green");
                                            Settings.wordCounter = 0;
                                            Console.WriteLine("\n");
                                        }
                                        TextFunctions.SlowPrint($" You recover {recover} HP, ", "green");
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                    }
                                    break;
                                case "comfyChair":
                                    TextFunctions.SlowPrint(" You sit down in the armchair and prepare for some shut-eye, ahh truly the king of all chairs. ");
                                    TextFunctions.SlowPrint("Words dont do it justice. ", "green");
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    if (CharacterSheet.life < 5 + CharacterSheet.strength)
                                    {
                                        int recover = rand.Next(4) * heatFactor;
                                        if (CharacterSheet.life + recover > 5 + CharacterSheet.strength)
                                        {
                                            CharacterSheet.life = 5 + CharacterSheet.strength;
                                        }
                                        else
                                        {
                                            CharacterSheet.life += recover;
                                        }
                                        if (CharacterSheet.pain > 0)
                                        {
                                            CharacterSheet.pain -= 1;
                                            TextFunctions.SlowPrint(" The rest made some pain go away.", "green");
                                            Settings.wordCounter = 0;
                                            Console.WriteLine("\n");
                                        }
                                        TextFunctions.SlowPrint($" You recover {recover} HP, ", "green");
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                    }
                                    break;
                                case "bed":
                                    TextFunctions.SlowPrint(" You lay down on the suprisingly clean sheets, ");
                                    TextFunctions.SlowPrint("why is a clean bed here? ", "white");
                                    TextFunctions.SlowPrint("Thats a matter for another day.");
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    if (CharacterSheet.life < 5 + CharacterSheet.strength)
                                    {
                                        int recover = rand.Next(5) * heatFactor;
                                        if (CharacterSheet.life + recover > 5 + CharacterSheet.strength)
                                        {
                                            CharacterSheet.life = 5 + CharacterSheet.strength;
                                        }
                                        else
                                        {
                                            CharacterSheet.life += recover;
                                        }
                                        if (CharacterSheet.pain > 0)
                                        {
                                            CharacterSheet.pain -= 1;
                                            TextFunctions.SlowPrint(" The rest made some pain go away.", "green");
                                            Settings.wordCounter = 0;
                                            Console.WriteLine("\n");
                                        }
                                        TextFunctions.SlowPrint($" You recover {recover} HP, ", "green");
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                    }
                                    break;

                            }
                            break;
                        case 5:
                            switch(chest)
                            {
                                case 0:
                                    TextFunctions.SlowPrint(" You apporach the closed chest and open it with anticipation...");
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    RandReward();
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    break;
                                case 1:
                                    if(CharacterSheet.key)
                                    {
                                        TextFunctions.SlowPrint(" You apporach the closed chest and unlock it with your key, whereafter you open it with anticipation...");
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                        RandReward();
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                    }
                                    else
                                    {
                                        TextFunctions.SlowPrint(" Chest is locked, ");
                                        TextFunctions.SlowPrint("Unlucky.", "red");
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                    }
                                    break;
                                case 3:
                                    TextFunctions.SlowPrint(" Theres no chest to interact with?");
                                    Settings.wordCounter = 0;
                                    Console.WriteLine("\n");
                                    break;
                            }
                            break;
                    }
                }

                if (floor.level == 3)
                {
                    
                }
                else
                {
                    CharacterSheet.floor = GameGen.GenerateFloor();
                }
            }
        }

        public static void RunCombat()
        {
            
        }

        public static void RandReward()
        {
            Random rand = new();
            int reward = rand.Next(5);
            switch (reward)
            {
                case 0:
                    TextFunctions.SlowPrint(" It contains a prime ");
                    TextFunctions.SlowPrint(" Lump Of Coal! ", "purple");
                    break;
                case 1:
                    TextFunctions.SlowPrint(" It contains a cool ");
                    TextFunctions.SlowPrint(" Stick! ", "yellow");
                    break;
                case 2:
                    TextFunctions.SlowPrint(" It contains a heroic ");
                    TextFunctions.SlowPrint(" Stone! ", "brown");
                    break;
                case 3:
                    TextFunctions.SlowPrint(" It contains a Majestic ");
                    TextFunctions.SlowPrint(" NOTHING! ", "red");
                    break;
                case 4:
                    TextFunctions.SlowPrint(" It contains a OP");
                    TextFunctions.SlowPrint(" Gigga Blade! ", "green");
                    CharacterSheet.weapon = new Weapon("Very Big Super Blade", 10, true);
                    TextFunctions.SlowPrint(" Time to steamroll some mobs! Hurray!");
                    break;
            }
        }
    }
}