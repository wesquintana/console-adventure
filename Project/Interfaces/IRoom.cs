using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<IItem> Items { get; set; }
    bool Locked { get; set; }
    IRoom[] Directions { get; set; }
    List<IEntity> Entities { get; set; }
  }
}
