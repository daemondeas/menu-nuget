namespace Menu;

public class MenuItem
{
    public MenuItem(string name, Func<int> menuAction)
    {
        Name = name;
        MenuAction = menuAction;
    }

    public string Name { get; }
    public Func<int> MenuAction { get; }
    public int Length => Name.Length;
}