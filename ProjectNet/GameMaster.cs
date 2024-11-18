namespace ProjectNet
{
    public class GameMaster
    {
        public static void RunGame()
        {
            Console.Clear();
            if(CharacterSheet.floor.level == -1)
            {
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
                #endregion

                Console.Clear();
                Saver.SaveGame();
            }

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
                string restFactor = "none";
                int heatFactor = 1;
                int chest = 3;
                bool lookedAround = false;

                while (playing)
                {
                    Console.Write(" ");
                    float action = Predicter.Determine(Console.ReadLine() ?? "look around");
                    Console.WriteLine();

                    switch (action)
                    {
                        case 1:
                            #region go up the stairs
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
                            #endregion
                        case 2:
                            #region advance
                            if (!monsterDefeated)
                            {
                                monsterDefeated = RunCombat();
                            }
                            else {
                                TextFunctions.SlowPrint(" The opponent has been vanquished, ");
                                TextFunctions.SlowPrint($"{floor.opponent.Name} ", "red");
                                TextFunctions.SlowPrint("is no more, you may go to the next level.");
                                Settings.wordCounter = 0;
                                Console.WriteLine("\n");
                            }
                            break;
                            #endregion
                        case 3:
                            #region look around
                            if (lookedAround)
                            {
                                TextFunctions.SlowPrint(" You have already looked around.");
                                Settings.wordCounter = 0;
                                Console.WriteLine("\n");
                                break;
                            }
                            if (floor.objects.Count > 0 && !lookedAround)
                            {
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
                            }
                            else
                            {
                                TextFunctions.SlowPrint(" Theres nothing of intrest nearby");
                                Settings.wordCounter = 0;
                                Console.WriteLine("\n");
                            }
                            lookedAround = true;
                            break;
                        #endregion
                        case 4:
                            #region rest
                            switch (restFactor)
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
                                        TextFunctions.SlowPrint($" You recover {recover} HP, your HP is {CharacterSheet.life} and maximum is {5 + CharacterSheet.strength}. ", "green");
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
                                        TextFunctions.SlowPrint($" You recover {recover} HP, your HP is {CharacterSheet.life} and maximum is {5 + CharacterSheet.strength}. ", "green");
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
                                        TextFunctions.SlowPrint($" You recover {recover} HP, your HP is {CharacterSheet.life} and maximum is {5 + CharacterSheet.strength}. ", "green");
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
                                        TextFunctions.SlowPrint($" You recover {recover} HP, your HP is {CharacterSheet.life} and maximum is {5 + CharacterSheet.strength}. ", "green");
                                        Settings.wordCounter = 0;
                                        Console.WriteLine("\n");
                                    }
                                    break;

                            }
                            break;
                        #endregion
                        case 5:
                            #region open chest
                            switch (chest)
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
                            #endregion
                    }
                }

                if (floor.level == 3)
                {
                    Console.Clear();
                    Console.WriteLine();
                    TextFunctions.SlowPrint(" Congratulations! You won the game, hurray!");
                    Settings.rank = "Duke";
                    Saver.SaveSettings();
                    Saver.RemoveSave();
                    Thread.Sleep(5000);
                    System.Environment.Exit(0);
                }
                else
                {
                    CharacterSheet.floor = GameGen.GenerateFloor();
                    Saver.SaveGame();
                }
            }
        }

        public static bool RunCombat()
        {
            Monster monster = CharacterSheet.floor.opponent;
            Random random = new Random();
            TextFunctions.SlowPrint(" You engage the ");
            TextFunctions.SlowPrint($"{monster.Name}! ", "red");
            TextFunctions.SlowPrint("What do you do? ");
            Settings.wordCounter = 0;
            Console.WriteLine("\n");
            bool combat = true;

            while (combat)
            {
                Console.Write(" ");
                float action = Predicter.CombatDetermine(Console.ReadLine() ?? "null");
                Weapon weapon = CharacterSheet.weapon;
                Console.Write(" ");
                double combatModifier = 1;

                if (action == 1)
                {
                    TextFunctions.RowPrint($"You perform a slashing action with { CharacterSheet.weapon.name}");
                }
                else
                {
                    TextFunctions.RowPrint($"You perform a thrusting action with { CharacterSheet.weapon.name}");
                }

                Settings.wordCounter = 0;
                Console.WriteLine();

                if (monster.DefenseType != weapon.type && monster.DefenseType != action - 1)
                {
                    combatModifier = 2;
                }
                else if (monster.DefenseType != action - 1)
                {
                    combatModifier = 1.5;
                }
                float randomRollDMG = random.NextSingle();
                if(randomRollDMG < 0.5F)
                {
                    randomRollDMG = 0.5F;
                }
                if (randomRollDMG > 0.9F)
                {
                    int damageNum = (int)Math.Round(weapon.damage * 2 * combatModifier);
                    TextFunctions.SlowPrint(" CRITICAL HIT! ");
                    TextFunctions.RowPrint($"You did {damageNum} damage!");
                    monster.HP -= damageNum;
                }
                else
                {
                    int damageNum = (int)Math.Round((weapon.damage * randomRollDMG * combatModifier));
                    bool doDamage = true;
                    if(CharacterSheet.pain > 0 && random.Next(10) > 6)
                    {
                        TextFunctions.RowPrint(" OUCH, your back is acting up, you miss this attack, pity");
                        doDamage = false;
                    }
                    if(doDamage)
                    {
                        TextFunctions.RowPrint($" You hit the monster, for {damageNum} damage!");
                        monster.HP -= damageNum;
                    }
                }

                Settings.wordCounter = 0;
                Console.WriteLine();

                if (monster.HP <= 0)
                {
                    TextFunctions.SlowPrint($" Congratulations! You have slain {monster.Name}!");
                    if(random.Next(2) == 1)
                    {
                        Settings.wordCounter = 0;
                        Console.WriteLine();
                        TextFunctions.SlowPrint(" And he also dropped a key? Neat!");
                        CharacterSheet.key = true;
                    }
                    CharacterSheet.strength += 2;
                    CharacterSheet.agility += 1;
                    Settings.wordCounter = 0;
                    Console.WriteLine("\n");
                    combat = false;
                    return true;
                }
                else
                {
                    randomRollDMG = random.NextSingle();
                    TextFunctions.RowPrint($" The {monster.Name} attacks!");
                    int damageNum = (int)Math.Round(monster.Damage * randomRollDMG);

                    if(damageNum > 3)
                    {
                        damageNum -= CharacterSheet.agility;
                    }
                    if(damageNum <= 0)
                    {
                        TextFunctions.RowPrint(" The monsters attack missed! Good dodge!");
                    }
                    else
                    {
                        TextFunctions.RowPrint($" The monsters attack hits!, you suffer {damageNum} damage, ouch.");
                        CharacterSheet.life -= damageNum;
                        TextFunctions.RowPrint($" Current life is: {CharacterSheet.life}");
                    }

                    if (CharacterSheet.life <= 0)
                    {
                        Settings.wordCounter = 0;
                        Console.Clear();
                        Console.WriteLine();
                        Thread.Sleep(400);
                        TextFunctions.SlowPrint(" You have failed... Thus the tower has become your final resting place, farewell.", "red");
                        Saver.RemoveSave();
                        Thread.Sleep(4000);
                        System.Environment.Exit(0);
                    }

                }
                Settings.wordCounter = 0;
                Console.WriteLine();
            }
            return false;
        }

        public static void RandReward()
        {
            Random rand = new();
            int reward = rand.Next(5);
            switch (reward)
            {
                case 0:
                    TextFunctions.SlowPrint(" It contains a prime ");
                    TextFunctions.SlowPrint("Lump Of Coal! ", "purple");
                    break;
                case 1:
                    TextFunctions.SlowPrint(" It contains a cool ");
                    TextFunctions.SlowPrint("Stick! ", "yellow");
                    break;
                case 2:
                    TextFunctions.SlowPrint(" It contains a heroic ");
                    TextFunctions.SlowPrint("Stone! ", "brown");
                    break;
                case 3:
                    TextFunctions.SlowPrint(" It contains a Majestic ");
                    TextFunctions.SlowPrint("NOTHING! ", "red");
                    break;
                case 4:
                    TextFunctions.SlowPrint(" It contains a OP ");
                    TextFunctions.SlowPrint("Gigga Blade! ", "green");
                    CharacterSheet.weapon = new Weapon("Very Big Super Blade", 10, 1);
                    TextFunctions.SlowPrint("Time to steamroll some mobs! Hurray!");
                    break;
            }
        }
    }
}