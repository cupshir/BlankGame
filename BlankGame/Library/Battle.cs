using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankGame
{
    public class Battle
    {
        public static Tuple<Player, Monster> StartBattle(Player player, Monster mob)
        {
            string battleStage = "fight";
            string battleTitle = "";

            do
            {
                if(HealthCheck(player, mob))
                {
                    break;
                }

                battleTitle = SetBattleTitle(player, mob);
                DisplayBattleTitle(battleTitle);
                UI.DisplayActionBar("Battle");
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
                        AttackMob(player, mob, battleTitle);
                        AttackPlayer(mob, player);
                        UI.DisplayActionBar("Battle");
                        break;

                    case "run":
                        Console.Clear();
                        Console.SetCursorPosition(0, 10);
                        UI.DisplayCenterText("You have fled in terror like a little bitch!");
                        battleStage = "exit";
                        break;

                    case "help":
                        BattleHelp();
                        break;
                    default:
                        Console.Clear();
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

        private static void AttackMob(Player player, Monster mob, string battleTitle)
        {
            int damage = 0;
            bool miss = CheckMiss(player);
            if (miss)
            {
                Console.Clear();
                battleTitle = SetBattleTitle(player, mob);
                DisplayBattleTitle(battleTitle);
                Console.SetCursorPosition(0, 10);
                UI.DisplayCenterText(player.Name + " swings and misses!");
              
            }
            else
            {
                damage = CalculateDamage(player);
                int mitigatedDamage = CalculateMitigatedDamage(mob);
                damage = damage - mitigatedDamage;
                if (damage > 0)
                {
                    mob.Hitpoints = mob.Hitpoints - damage;
                    Console.Clear();
                    battleTitle = SetBattleTitle(player, mob);
                    DisplayBattleTitle(battleTitle);
                    Console.SetCursorPosition(0, 10);
                    UI.DisplayCenterText(player.Name + " hits " + mob.Name + " for " + damage + " damage!");
                }
                else
                {
                    Console.Clear();
                    battleTitle = SetBattleTitle(player, mob);
                    DisplayBattleTitle(battleTitle);
                    Console.SetCursorPosition(0, 10);
                    UI.DisplayCenterText(player.Name + " hits " + mob.Name + " for 0 damage!");
                }
            }
            
        }

        private static void AttackPlayer(Monster mob, Player player)
        {
            int damage = 0;
            bool miss = CheckMiss(player);
            if (!miss)
            {
                damage = CalculateDamage(mob);
                int mitigatedDamage = CalculateMitigatedDamage(player);
                damage = damage - mitigatedDamage;
                if (damage > 0)
                {
                    player.Hitpoints = player.Hitpoints - damage;
                    Console.WriteLine();
                    UI.DisplayCenterText(mob.Name + " hits " + player.Name + " for " + damage + " damage!");
                }
                else
                {
                    Console.WriteLine();
                    UI.DisplayCenterText(mob.Name + " hits " + player.Name + " for 0 damage!");
                }
            }
            else
            {
                Console.WriteLine();
                UI.DisplayCenterText(mob.Name + " swings and misses!");

            }
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

        private static int CalculateDamage(Player player)
        {
            int damage = 1;
            damage = damage * player.AttackPower;
            return damage;

        }

        private static int CalculateDamage(Monster mob)
        {
            int damage = 1;
            damage = damage * mob.AttackPower;
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

        private static void BattleHelp()
        {
            Console.Clear();
            UI.DisplayCenterText("Commands");
            UI.DrawLine(120);
            UI.DisplayCenterText("Attack");
            UI.DisplayCenterText("Run");
            UI.DisplayCenterText("Help");

            UI.DisplayActionBar("Battle");

        }


    }
}
