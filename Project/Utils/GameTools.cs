namespace ConsoleAdventure.Project.Utils
{
  class GameTools
  {
    public int parseDirection(string direction)
    {
      switch (direction)
      {
        case "up":
          return 0;
        case "down":
          return 1;
        case "north":
          return 2;
        case "south":
          return 3;
        case "west":
          return 4;
        case "east":
          return 5;
        default:
          return -1;
      }
    }
  }
}