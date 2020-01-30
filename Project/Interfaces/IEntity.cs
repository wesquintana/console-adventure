using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IEntity
  {
    List<IItem> Loot { get; set; }
    int Health { get; set; }
    string Name { get; set; }
  }
}
