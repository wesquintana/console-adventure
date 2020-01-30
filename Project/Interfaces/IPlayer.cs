using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IPlayer
  {
    List<IItem> Inventory { get; set; }
  }
}
