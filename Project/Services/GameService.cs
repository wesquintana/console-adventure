using System;
using System.Collections.Generic;
using System.Reflection;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using ConsoleAdventure.Project.Utils;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    private IGame _game { get; set; }

    public List<string> Messages { get; set; }
    private GameTools Tools { get; } = new GameTools();
    public GameService()
    {
      _game = new Game();
      Messages = new List<string>();
    }
    public void Go(string direction)
    {
      int indexDirection = Tools.parseDirection(direction);
      if (indexDirection == -1 || _game.CurrentRoom.Directions[indexDirection] == null)
      {
        Messages.Add("You can't go that way.");
        return;
      }
      if (_game.CurrentRoom.Directions[indexDirection].Locked)
      {
        Messages.Add("That room is locked off. Maybe there's a key nearby...");
        return;
      }
      _game.CurrentRoom.Entities.Remove(_game.CurrentPlayer);
      _game.CurrentRoom = _game.CurrentRoom.Directions[indexDirection];
      _game.CurrentRoom.Entities.Add(_game.CurrentPlayer);
    }
    public void UseItem(string itemName)
    {
      IItem item = _game.CurrentPlayer.Inventory.Find(item => item.Name.ToLower() == itemName);
      if (item == null)
      {
        Messages.Add($"You dont have a {itemName}");
        return;
      }
      if (item.GetType().Name != "KeyItem")
      {
        Messages.Add($"{item.Name} can't be used!");
        return;
      }
      Type thisType = this.GetType();
      MethodInfo theMethod = thisType.GetMethod(((KeyItem)item).MethodName);
      object[] parametersArray = new KeyItem[] { (KeyItem)item };
      theMethod.Invoke(this, parametersArray);
    }
    public void Help()
    {
      Messages.Add("inventory, take, go (North,South,East,West,Up,Down), look, stop, use (item), attack (enemy), attack (enemy) with (weapon), restart");
    }

    public void Inventory()
    {
      _game.CurrentPlayer.Inventory.ForEach(item =>
      {
        Messages.Add($"{item.Name}: {item.Description}");
      });
    }
    public void Attack(string playerInput)
    {
      string entityName;
      int spaceIndex = playerInput.IndexOf(" ");
      if (spaceIndex == -1)
      {
        entityName = playerInput;
      }
      else
      {
        entityName = playerInput.Substring(0, spaceIndex);
      }
      IEntity attackedEntity = _game.CurrentRoom.Entities.Find(entity => entity.Name.ToLower() == entityName);
      if (attackedEntity == null)
      {
        Messages.Add("No such thing exists to attack");
        return;
      }
      int weaponIndex = playerInput.IndexOf(" with ");
      if (weaponIndex == -1)
      {
        attackedEntity.Health--;
        if (attackedEntity.Health == 0)
        {
          _game.CurrentRoom.Entities.Remove(attackedEntity);
          if (attackedEntity.Name == "self")
          {
            Messages.Add("You have died!");
            return;
          }
        }
        Messages.Add($"{attackedEntity.Name} took 1 damage and now has {attackedEntity.Health} HP left!");
        return;
      }
      string weaponString = playerInput.Substring(weaponIndex + 5).Trim();
      IItem weapon = _game.CurrentPlayer.Inventory.Find(item => item.GetType().Name == "Weapon" && weaponString == item.Name.ToLower());
      if (weapon == null)
      {
        Messages.Add($"You don't have a {weaponString}.");
        return;
      }
      attackedEntity.Health -= ((Weapon)weapon).Damage;
      if (attackedEntity.Health <= 0)
      {
        _game.CurrentRoom.Entities.Remove(attackedEntity);
        if (attackedEntity.Name == "self")
        {
          Messages.Add("You have died!");
          return;
        }
        Messages.Add("It died!");
      }
      Messages.Add($"{attackedEntity.Name} took {((Weapon)weapon).Damage} from your {weapon.Name} and now has {attackedEntity.Health} left!");
    }
    public void Win()
    {
      Messages.Add("You win!");
    }

    public void Look()
    {
      Messages.Add($"\t\t{_game.CurrentRoom.Name}");
      Messages.Add(_game.CurrentRoom.Description);
    }
    public void Unlock(IItem keyItem)
    {
      // IRoom tempRoom;
      for (int i = 0; i < _game.CurrentRoom.Directions.Length; i++)
      {
        // Console.WriteLine(_game.CurrentRoom.Directions[i].Name);
        if (_game.CurrentRoom.Directions[i] != null)
        {


          if (_game.CurrentRoom.Directions[i].GetType().Name == "LockedRoom" && ((LockedRoom)_game.CurrentRoom.Directions[i]).KeyName == keyItem.Name)
          {
            _game.CurrentRoom.Directions[i].Locked = false;
            _game.CurrentRoom.Description = "This room has strings of green ones and zeroes flowing in every direction across its black walls. There is a room that seems to be flickering in and out of existence to the west, but now it looks like it has stablized.";
            Messages.Add("Unlocked!");
          }
        }
      }
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      Messages.Add(@" __          __  _                            _          _   _            _____        _                                 
 \ \        / / | |                          | |        | | | |          |  __ \      | |                                
  \ \  /\  / /__| | ___ ___  _ __ ___   ___  | |_ ___   | |_| |__   ___  | |  | | __ _| |_ __ _ ___ _ __   __ _  ___ ___ 
   \ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \  | __| '_ \ / _ \ | |  | |/ _` | __/ _` / __| '_ \ / _` |/ __/ _ \
    \  /\  /  __/ | (_| (_) | | | | | |  __/ | || (_) | | |_| | | |  __/ | |__| | (_| | || (_| \__ \ |_) | (_| | (_|  __/
     \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \__\___/   \__|_| |_|\___| |_____/ \__,_|\__\__,_|___/ .__/ \__,_|\___\___|
                                                                                                   | |                   
                                                                                                   |_|                   ");
      Messages.Add("You feel a buzz run from your head to your toes as you are created.\nYou do not yet know your primary function, but only a handful of subroutines: go, look, and take.");
      _game.Setup();
    }

    public void Setup()
    {

    }
    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      IItem realItem = _game.CurrentRoom.Items.Find(item => item.Name.ToLower() == itemName);
      if (realItem == null)
      {
        Messages.Add($"There is no { itemName } in this room.");
        return;
      }
      _game.CurrentRoom.Items.Remove(realItem);
      _game.CurrentPlayer.Inventory.Add(realItem);
      Messages.Add($"You pick up the {itemName}");
    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
  }
}