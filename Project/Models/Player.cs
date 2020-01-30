using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Player : IPlayer, IEntity
  {
    public List<IItem> Inventory { get; set; } = new List<IItem>();
    public int Health { get; set; } = 3;
    public List<IItem> Loot { get; set; } = new List<IItem>();
    public string Name { get; set; } = "self";
  }
}