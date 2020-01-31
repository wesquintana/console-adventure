using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  class LockedRoom : Room, IUnlockable
  {
    public LockedRoom(string name, string description, string keyName) : base(name, description)
    {
      KeyName = keyName;
    }

    public LockedRoom(string name, string description, List<IItem> items, string keyName) : base(name, description, items)
    {
      KeyName = keyName;
    }

    public string KeyName { get; set; }
  }
}