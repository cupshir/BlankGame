using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Battle
    {

        // Execute Battle
        public static Tuple<Player, Monster> ExecuteBattle(Player player, Monster mob)
        {
            string battleStage = "fight";
            string battleTitle = "";
            string content = "";

            do
            {
                Console.Clear();

                if (HealthCheck(player, mob))
                {
                    player = MobDefeated(player, mob);
                    Player.IncreaseXP(player, mob.xpWorth);
                    break;
                }

                if (battleStage == "attack")
                {
                    battleTitle = SetBattleTitle(player, mob);
                    UI.DrawTitleBar(battleTitle);

                    UI.DrawMainArea(content);

                    UI.DrawActionBar("Auto Battle Engaged (sit back and enjoy doing nothing)");

                    Thread.Sleep(1500);

                    content = "";
                    content = content + AttackMob(player, mob);
                    content = content + AttackPlayer(mob, player);

                }
                else
                {
                    battleTitle = SetBattleTitle(player, mob);
                    UI.DrawTitleBar(battleTitle);

                    UI.DrawMainArea(content);

                    UI.DrawActionBar("Battle");

                    string result = Console.ReadLine();

                    switch (result.ToLower())
                    {
                        case "test":
                            mob.Hitpoints = 0;
                            break;

                        case "test2":
                            player.Hitpoints = 0;
                            break;

                        case "attack":
                            battleStage = "attack";
                            content = "";
                            content = content + AttackMob(player, mob);
                            content = content + AttackPlayer(mob, player);
                            break;

                        case "run":
                            battleStage = "exit";
                            break;

                        case "help":
                            content = "";
                            content = BattleHelp();
                            break;
                        default:
                            content = "";
                            break;
                    }
                }
                
            } while (battleStage != "exit");
            
            return Tuple.Create(player, mob);
        }

        // Create title for battle screen
        private static string SetBattleTitle(Player player, Monster mob)
        {
            string battleTitle = player.Name + " (" + player.Hitpoints + ") VS " + mob.Name + " (" + mob.Hitpoints + ")";
            return battleTitle;
        }

        // Draw battle screen title
        private static void DisplayBattleTitle(string title)
        {
            UI.DisplayCenterText(title);
            UI.DrawLine(120);
        }

        // Execute player attacking mob
        private static string AttackMob(Player player, Monster mob)
        {
            string content = "";
            int damage = 0;
            bool miss = CheckMiss(player.Agility, mob.Agility);
            if (!miss)
            {
                content = content + player.Name + " swings and misses!\n\n";
            }
            else
            {
                damage = CalculateDamage(player.AttackPower);
                if (damage > 0)
                {
                    mob.Hitpoints = mob.Hitpoints - damage;
                    content = content + player.Name + " hits " + mob.Name + " for " + damage + " damage!\n\n";
                }
                else
                {
                    content = content + player.Name + " hits " + mob.Name + " for 0 damage!\n\n";
                }
            }
            
            return content;
        }

        //Execute mob attacking player
        private static string AttackPlayer(Monster mob, Player player)
        {
            string content = "";
            int damage = 0;
            bool miss = CheckMiss(mob.Agility, player.Agility);
            if (miss)
            {
                content = content + mob.Name + " swings and misses!\n\n";

            }
            else
            {
                damage = CalculateDamage(mob.AttackPower);
                if (damage > 0)
                {
                    player.Hitpoints = player.Hitpoints - damage;
                    content = content + mob.Name + " hits " + player.Name + " for " + damage + " damage!\n\n";
                }
                else
                {
                    content = content + mob.Name + " hits " + player.Name + " for 0 damage!\n\n";
                }
            }


            return content;
        }

        // Calculte if swing misses
        private static Boolean CheckMiss(int attackerAgility, int defenderAgility)
        {
            Random rng = new Random();
            int attackerAttempt = rng.Next(0, attackerAgility);
            int defenderAttempt = rng.Next(0, defenderAgility);
            if (attackerAttempt > defenderAttempt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Check Health of mob and health of player.
        private static Boolean HealthCheck(Player player, Monster mob)
        {
            if (mob.Hitpoints <= 0)
            {
                return true;
            }
            else if (player.Hitpoints <= 0)
            {
                PlayerDefeated();
            }
            return false;
        }

        // Execute Player Defeated
        private static void PlayerDefeated()
        {
            Console.Clear();
            string battleTitle = "You loss!!!";
            UI.DrawTitleBar(battleTitle);
            UI.DrawMainArea("The monster has slain you...you suck!!!\n\nMaybe find a better weapon?\n\nOr buy a healing rock?!?");
            Thread.Sleep(5000);
            System.Environment.Exit(1);
        }

        // Execute Mob Defeated
        private static Player MobDefeated(Player player, Monster mob)
        {
            Console.Clear();
            string battleTitle = "You won!!!";
            UI.DrawTitleBar(battleTitle);
            UI.DrawMainArea("The monster has been slain!!!\n\nLike a desperate degenerate, you collect its shit.");
            if (mob.Name != "Uber Boss")
            {
                IEnumerable<Item> hasShit = player.Inventory.Where(p => p.Name == "Shit");
                if (hasShit.Count() == 1)
                {
                    Item shit = hasShit.Single();
                    shit.Quantity = shit.Quantity + 1;
                    player.Inventory.Remove(shit);
                    player.Inventory.Add(shit);
                }
                else
                {
                    player.Inventory.Add(Item.CreateItem(name: "Shit", description: "Smelly piece of..."));
                }
            }

            Thread.Sleep(2000);
            return player;
        } 

        // Calculate Damage
        private static int CalculateDamage(int attackPower)
        {
            int damage = 1;
            damage = damage * attackPower;
            return damage;

        }

        // Display Battle Commands
        private static string BattleHelp()
        {
            string content = "";

            //content = content + "\n";
            content = content + "Commands\n\n";
            content = content + "Attack\n";
            content = content + "Run\n";
            content = content + "Help\n";

            return content;
        }


    }
}
