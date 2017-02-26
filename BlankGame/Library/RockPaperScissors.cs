using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankGame
{
    public class RockPaperScissors
    {
        // Play Rock Paper Scissors
        public static string PlayRockPaperScissors(string playerName, string npcName)
        {
            string content = "";
            string choice = GetPlayerChoice();
            Tuple<string, string> matchOutcome = CalculateRockPaperScissors(choice);
            string npcChoice = matchOutcome.Item2;

            content = content + playerName + " chose " + choice + "\n";
            content = content + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(npcName) + " chose " + npcChoice + "\n\n";

            if (matchOutcome.Item1 == "win")
            {
                content = content + "You win!!!";
            }
            else if (matchOutcome.Item1 == "loss")
            {
                content = content + "You Lose!!!";
            } else if (matchOutcome.Item1 == "draw")
            {
                content = content + "Its a tie!";
            }

            Console.Clear();
            DisplayRockPaperScissors(content, "Results");

            Thread.Sleep(2000);
            return matchOutcome.Item1;

        }
        
        // Display Rock Paper Scissors Sidegame
        public static void DisplayRockPaperScissors(string content, string prompt)
        {
            Console.Clear();
            UI.DrawTitleBar("Rock Paper Scissors");
            UI.DrawMainArea(content);
            UI.DrawActionBar(prompt);
        }

        // Opening screen for Rock Paper Scissors Game
        public static string RockPaperScissorsOptions()
        {
            string content = "\n\n\n";
            content = content + "Choose wisely\n\n";
            content = content + "1) Rock    \n";
            content = content + "2) Paper   \n";
            content = content + "3) Scissors\n";
            return content;
        }
        
        // Get Player Choice
        public static string GetPlayerChoice()
        {
            string choice = "incorrect";
            string content = "";
            do
            {
                content = RockPaperScissorsOptions();

                DisplayRockPaperScissors(content, "Choice");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        choice = "rock";
                        break;
                    case "2":
                        choice = "paper";
                        break;
                    case "3":
                        choice = "scissors";
                        break;
                    default:
                        choice = "incorrect";
                        break;
                }
            } while (choice == "incorrect");

            return choice;
        }

        // Return outcome of Rock Paper Scissors side game
        public static Tuple<string, string> CalculateRockPaperScissors(string playerChoice)
        {
            string matchOutcome = "";
            string npcChoice = RandomChoice();
            
            if (playerChoice != npcChoice)
            {
                switch(playerChoice)
                {
                    case "rock":
                        if (npcChoice == "paper")
                        {
                            matchOutcome = "loss"; 
                        } 
                        else if (npcChoice == "scissors")
                        {
                            matchOutcome = "win";
                        }
                        return Tuple.Create(matchOutcome, npcChoice);
                    case "paper":
                        if (npcChoice == "scissors")
                        {
                            matchOutcome = "loss";
                        }
                        else if (npcChoice == "rock")
                        {
                            matchOutcome = "win";
                        }
                        return Tuple.Create(matchOutcome, npcChoice);
                    case "scissors":
                        if (npcChoice == "rock")
                        {
                            matchOutcome = "loss";
                        }
                        else if (npcChoice == "paper")
                        {
                            matchOutcome = "win";
                        }
                        return Tuple.Create(matchOutcome, npcChoice);
                    default:
                        return Tuple.Create(matchOutcome, npcChoice);
                }

            }
            else
            {
                matchOutcome = "draw";
            }
            return Tuple.Create(matchOutcome, npcChoice);
        }

        // Generate NPC choice
        public static string RandomChoice()
        {
            string choice = "";
            string[] options = new string[3] { "rock", "paper", "scissors" };
            Random rng = new Random();
            int randomChoice = rng.Next(0, 3);
            choice = options[randomChoice];
            
            return choice;
        }
    }
}
