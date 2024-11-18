namespace ProjectNet
{
    //the "gamemaster" contains all the game "logic" except combat, its a bit cluttered but mostly due to text
    public class GameMaster
    {
        public static void RunGame()
        {
            //if file is loaded dont bother with touturial and initial generation
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

            //the whole game loop
            while(true)
            {
                Floor floor = CharacterSheet.floor;
                bool monsterDefeated = false;
                Random rand = new();

                //text block
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

                //some usefull variables
                bool playing = true;
                //rest modifier
                string restFactor = "none";
                //heat modifier
                int heatFactor = 1;
                //chest existance, 2/1 is locked or unlocked chest, bool might be better
                int chest = 3;
                //makes sure it dosent look around twice
                bool lookedAround = false;

                //a contained floor exploration
                while (playing)
                {
                    //Determines input, if no input defaults to look around, it has no safety rails or double asks, but you cant really do any "harmfull" misstakes so whatever
                    Console.Write(" ");
                    float action = Predicter.Determine(Console.ReadLine() ?? "look around");
                    Console.WriteLine();

                    //all the actions, put into blocks to spare the code readability
                    switch (action)
                    {
                        case 1:
                            #region go up the stairs
                            //monster must be defeated
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
                            //if monster is alive
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
                            //custom description and some rest modifiers for all possible objects
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
                            //making sure not to clutter console with this again
                            lookedAround = true;
                            break;
                        #endregion
                        case 4:
                            #region rest
                            //switch with diffrent rests depending on objects in the enviroment, hp reconver is random number with a higher ceiling with better furniture
                            switch (restFactor)
                            {
                                //if no "furniture" to rest on get backpain, lowers combat ability
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

                                        //not bool due to initial plans to make it more expansive, kept as is
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
                            //cases for diffrent types of chest, 0 is a open, 1 is locked and 2 is non existant
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
                                    //checks if you have a key
                                    if (CharacterSheet.key)
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

                //if top floor is reached, you win the game if you ascend the stairs
                if (floor.level == 3)
                {
                    Console.Clear();
                    Console.WriteLine();
                    TextFunctions.SlowPrint(" Congratulations! You won the game, hurray!");

                    //fun little indikator of winning the game
                    Settings.rank = "Duke";

                    //saves the new rank and removes the savefile
                    Saver.SaveSettings();
                    Saver.RemoveSave();

                    //a few seconds to appreciate the ending
                    Thread.Sleep(5000);
                    System.Environment.Exit(0);
                }
                else
                {
                    //if not top, generate new floor
                    CharacterSheet.floor = GameGen.GenerateFloor();
                    Saver.SaveGame();
                }
            }
        }

        //all combat logic, could be split to new file but i already have a ton of files
        public static bool RunCombat()
        {
            //gets enemy
            Monster monster = CharacterSheet.floor.opponent;
            Random random = new();
            TextFunctions.SlowPrint(" You engage the ");
            TextFunctions.SlowPrint($"{monster.Name}! ", "red");
            TextFunctions.SlowPrint("What do you do? ");
            Settings.wordCounter = 0;
            Console.WriteLine("\n");
            bool combat = true;

            //combat is running
            while (combat)
            {
                Console.Write(" ");
                //determins if you are doing a slashing or thrusting attack, quite bad model, but i cant be asked to get more data.
                float action = Predicter.CombatDetermine(Console.ReadLine() ?? "null");
                Weapon weapon = CharacterSheet.weapon;
                Console.Write(" ");

                //combat efficiency modifier
                double combatModifier = 1;

                //if slash or thrust
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

                //Checks if attack counter monsters defence type and if weapon counters monster type, spear better against armor sword better against unarmored
                if (monster.DefenseType != weapon.type && monster.DefenseType != action - 1)
                {
                    combatModifier = 2;
                }
                else if (monster.DefenseType != action - 1)
                {
                    combatModifier = 1.5;
                }

                //random damage modifier for fun random numbers, cant be worse than half damage
                float randomRollDMG = random.NextSingle();
                if(randomRollDMG < 0.5F)
                {
                    randomRollDMG = 0.5F;
                }

                //critical hit if 10% is hit, cant have a combat system without crits
                if (randomRollDMG > 0.9F)
                {
                    int damageNum = (int)Math.Round(weapon.damage * 2 * combatModifier);
                    TextFunctions.SlowPrint(" CRITICAL HIT! ");
                    TextFunctions.RowPrint($"You did {damageNum} damage!");
                    monster.HP -= damageNum;
                }

                //normal damage calculation
                else
                {
                    //rounds the whole damage to int
                    int damageNum = (int)Math.Round((weapon.damage * randomRollDMG * combatModifier));
                    
                    //small if for random miss if you have pain
                    bool doDamage = true;
                    if(CharacterSheet.pain > 0 && random.Next(10) > 6)
                    {
                        TextFunctions.RowPrint(" OUCH, your back is acting up, you miss this attack, pity");
                        doDamage = false;
                    }

                    //damage
                    if(doDamage)
                    {
                        TextFunctions.RowPrint($" You hit the monster, for {damageNum} damage!");
                        monster.HP -= damageNum;
                    }
                }

                Settings.wordCounter = 0;
                Console.WriteLine();

                //if monster dies it does not retaliate
                if (monster.HP <= 0)
                {
                    TextFunctions.SlowPrint($" Congratulations! You have slain {monster.Name}!");

                    //50% chance monster drops a key, proof of concept
                    if(random.Next(2) == 1)
                    {
                        Settings.wordCounter = 0;
                        Console.WriteLine();
                        TextFunctions.SlowPrint(" And he also dropped a key? Neat!");
                        CharacterSheet.key = true;
                    }

                    //some stat increments
                    CharacterSheet.strength += 2;
                    CharacterSheet.agility += 1;
                    Settings.wordCounter = 0;
                    Console.WriteLine("\n");
                    return true;
                }
                else
                {
                    //monster damage roll, can roll 0 damage
                    randomRollDMG = random.NextSingle();
                    TextFunctions.RowPrint($" The {monster.Name} attacks!");
                    int damageNum = (int)Math.Round(monster.Damage * randomRollDMG);

                    //removes from higher damages to prevent insta kills
                    if(damageNum > 3)
                    {
                        damageNum -= CharacterSheet.agility;
                    }
                    //chance to dodge enemy attack
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

                    //character died
                    if (CharacterSheet.life <= 0)
                    {
                        Settings.wordCounter = 0;
                        Console.Clear();
                        Console.WriteLine();
                        Thread.Sleep(400);
                        TextFunctions.SlowPrint(" You have failed... Thus the tower has become your final resting place, farewell.", "red");

                        //kills save
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

        //random reward for chests, only real reward is a op sword
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