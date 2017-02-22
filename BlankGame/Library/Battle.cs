using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Battle
    {
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
                    break;
                }

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
                        content = "";
                        content = content + AttackMob(player, mob, battleTitle);
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
                
            } while (battleStage != "exit");
            
            return Tuple.Create(player, mob);
        }

        private static string SetBattleTitle(Player player, Monster mob)
        {
            string battleTitle = player.Name + " (" + player.Hitpoints + ") VS " + mob.Name + " (" + mob.Hitpoints + ")";
            return battleTitle;
        }

        private static void DisplayBattleTitle(string title)
        {
            UI.DisplayCenterText(title);
            UI.DrawLine(120);
        }

        private static string AttackMob(Player player, Monster mob, string battleTitle)
        {
            string content = "";
            int damage = 0;
            bool miss = CheckMiss(player);
            if (miss)
            {
                content = content + player.Name + " swings and misses!\n\n";
            }
            else
            {
                damage = CalculateDamage(player.AttackPower);
                int mitigatedDamage = CalculateMitigatedDamage(mob);
                damage = damage - mitigatedDamage;
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

        private static string AttackPlayer(Monster mob, Player player)
        {
            string content = "";
            int damage = 0;
            bool miss = CheckMiss(player);
            if (!miss)
            {
                damage = CalculateDamage(mob.AttackPower);
                int mitigatedDamage = CalculateMitigatedDamage(player);
                damage = damage - mitigatedDamage;
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
            else
            {
                content = content + mob.Name + " swings and misses!\n\n";

            }

            return content;
        }

        private static Boolean CheckMiss(Player player)
        {
            int playerAttempt = 50 + player.Agility;

            Random rng = new Random();
            int miss = rng.Next(100);
            if (miss > playerAttempt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Boolean CheckMiss(Monster mob)
        {
            int playerAttempt = 50 + mob.Agility;

            Random rng = new Random();
            int miss = rng.Next(100);
            if (miss > playerAttempt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Boolean HealthCheck(Player player, Monster mob)
        {
            if (mob.Hitpoints <= 0)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 10);
                UI.DisplayCenterText(mob.Name + " has been slain, good job!");
                return true;
            }
            else if (player.Hitpoints <= 0)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 10);
                UI.DisplayCenterText("You have died, you suck!");
                Console.ReadLine();
                System.Environment.Exit(1);
            }
            return false;
        }

        private static int CalculateDamage(int attacker)
        {
            int damage = 1;
            damage = damage * attacker;
            return damage;

        }

        private static int CalculateMitigatedDamage(Monster Mob)
        {
            int mitigatedDamage = 1;
            mitigatedDamage = mitigatedDamage * Mob.DefenseRating;

            return mitigatedDamage;

        }

        private static int CalculateMitigatedDamage(Player player)
        {
            int mitigatedDamage = 1;
            mitigatedDamage = mitigatedDamage * player.DefenseRating;


            return mitigatedDamage;

        }

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
