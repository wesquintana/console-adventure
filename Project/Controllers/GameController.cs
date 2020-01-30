using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();
    private bool _running = true;

    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      Console.Write(@" __          __  _                            _          _   _            _____        _                                 
 \ \        / / | |                          | |        | | | |          |  __ \      | |                                
  \ \  /\  / /__| | ___ ___  _ __ ___   ___  | |_ ___   | |_| |__   ___  | |  | | __ _| |_ __ _ ___ _ __   __ _  ___ ___ 
   \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \  | __| '_ \ / _ \ | |  | |/ _` | __/ _` / __| '_ \ / _` |/ __/ _ \
    \  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) | | |_| | | |  __/ | |__| | (_| | || (_| \__ \ |_) | (_| | (_|  __/
     \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/   \__|_| |_|\___| |_____/ \__,_|\__\__,_|___/ .__/ \__,_|\___\___|
                                                                                                   | |                   
                                                                                                   |_|                   ");
      Console.WriteLine("\nYou feel a buzz run from your head to your toes as you are created.\nYou do not yet know your primary function, but only a handful of subroutines: go, look, and take.");
      while (_running)
      {
        if (_gameService.Messages.Contains("You have died!"))
        {
          Console.Clear();
          Print();
          bool stillFighting = true;
          while (stillFighting)
          {
            Console.WriteLine("Press enter to restart or q to quit");
            ConsoleKey c = Console.ReadKey(false).Key;
            switch (c)
            {
              case ConsoleKey.Enter:
                _gameService.Reset();
                stillFighting = false;
                Console.Clear();
                break;
              case ConsoleKey.Q:
                Environment.Exit(0);
                break;
              default:
                Console.Clear();
                break;
            }
          }
        }
        _gameService.Look();
        Print();
        GetUserInput();
      }
    }

    //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
    public void GetUserInput()
    {
      Console.WriteLine("What would you like to do?");
      string input = Console.ReadLine().ToLower() + " ";
      Console.Clear();
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();
      switch (command)
      {
        case "stop":
          _running = false;
          break;
        case "restart":
          _gameService.Reset();
          break;
        case "go":
          _gameService.Go(option);
          break;
        case "take":
          _gameService.TakeItem(option);
          break;
        case "inventory":
          _gameService.Inventory();
          break;
        case "help":
          _gameService.Help();
          break;
        case "look":
          _gameService.Look();
          break;
        case "attack":
          _gameService.Attack(option);
          break;
        case "use":
          _gameService.UseItem(option);
          break;
        default:
          Console.WriteLine("What?");
          break;
      }
      //NOTE this will take the user input and parse it into a command and option.
      //IE: take silver key => command = "take" option = "silver key"

    }

    //NOTE this should print your messages for the game.
    private void Print()
    {
      foreach (string message in _gameService.Messages)
      {
        Console.WriteLine(message);
      }
      _gameService.Messages.Clear();
    }

  }
}