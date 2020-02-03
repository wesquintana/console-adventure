using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      Player player = new Player();
      CurrentPlayer = player;
      KeyItem key = new KeyItem("Key", "the Key of Doors", "Unlock");
      Weapon sword = new Weapon("Sword", "The Legendary Sword of Testing", 2);
      Room startRoom = new Room("public static void", $"An empty dark room with a corridor leading north to some sort of pit and a black and bright green room to the west. Also there's a testing sword on the ground for some reason.", new List<IItem>() { sword });
      CurrentRoom = startRoom;
      CurrentRoom.Entities.Add(player);
      Room binaryRoom = new Room("Binary Room", "This room has strings of green ones and zeroes flowing in every direction across its black walls. There is a room that seems to be flickering in and out of existence to the west, but now probably isn't the best time to enter it.");
      LockedRoom finalRoom = new LockedRoom("Final Room", "This is where it all ends.", "Key");
      Room thePit = new Room("The Pit", "At the center of this room is seemingly bottomless pit, and above it another seemingly endless hole from which strange cubes, discs, and orbs fall out into the lower hole. You wonder what its purpose is. Also there's a key on the ground apparently", new List<IItem>() { key });
      binaryRoom.addDirection(finalRoom, 4);
      startRoom.addDirection(binaryRoom, 4);
      startRoom.addDirection(thePit, 2);
    }
    public Game()
    {
      Setup();
    }
  }
}