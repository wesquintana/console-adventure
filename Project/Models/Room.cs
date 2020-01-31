using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IItem> Items { get; set; } = new List<IItem>();
    public bool Locked { get; set; }
    //locked 
    public IRoom[] Directions { get; set; } = new IRoom[6];
    public List<IEntity> Entities { get; set; } = new List<IEntity>();
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
    }
    public Room(string name, string description, List<IItem> items)
    {
      Name = name;
      Description = description;
      Items = items;
    }
    public void addDirection(IRoom room, int direction)
    {
      switch (direction % 2)
      {
        case 0:
          this.Directions[direction] = room;
          room.Directions[direction + 1] = this;
          break;
        case 1:
          this.Directions[direction] = room;
          room.Directions[direction - 1] = this;
          break;
        default:
          Console.WriteLine("What did you do?");
          break;
      }
    }
  }
}