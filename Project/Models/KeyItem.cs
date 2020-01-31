namespace ConsoleAdventure.Project.Models
{
  class KeyItem : Item
  {
    public KeyItem(string name, string description, string methodName) : base(name, description)
    {
      MethodName = methodName;
    }
    public string MethodName { get; set; }
  }
}