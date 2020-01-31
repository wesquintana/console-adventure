using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Weapon : Item
  {
    public Weapon(string name, string description, int damage) : base(name, description)
    {
      Damage = damage;
    }
    public int Damage { get; set; }
  }
}