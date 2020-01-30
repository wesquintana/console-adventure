using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Weapon : IItem
  {
    public Weapon(string name, string description, int damage)
    {
      Description = description;
      Name = name;
      Damage = damage;
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Damage { get; set; }
  }
}