namespace Menu;

public class Menu
{
    private readonly List<MenuItem> _menuItems;
    private readonly string _header;
    private int _selection;

    public Menu(List<MenuItem> menuItems, string header)
    {
        _menuItems = menuItems;
        _header = header;
    }

    private int Width => Math.Min(
        Math.Max(_menuItems.Max(i => i.Length) + 5, _header.Length * 2 + 2),
        Console.WindowWidth - 4);

    public int Loop()
    {
        var result = 0;
        while(result > -1)
        {
            Console.Clear();
            PrintMenu();

            result = ReadInput();
        }

        return result;
    }

    private void PrintMenu()
    {
        PrintTop();
        PrintEmptyRow();
        PrintCentred(_header);
        PrintEmptyRow();
        PrintDividerRow();
        PrintEmptyRow();
        foreach(var menuItem in _menuItems)
        {
            PrintMenuItem(menuItem);
        }

        PrintEmptyRow();
        PrintBottom();
    }

    private void PrintRow(char start, char content, char end)
    {
        Console.Write(start);
        PrintRepeatedCharacter(content, Width);
        Console.Write(end);
        Console.Write('\n');
    }

    private void PrintTop()
    {
        PrintRow((char)0x2554, (char)0x2550, (char)0x2557);
    }

    private void PrintBottom()
    {
        PrintRow((char)0x255A, (char)0x2550, (char)0x255D);
    }

    private void PrintEmptyRow()
    {
        PrintRow((char)0x2551, ' ', (char)0x2551);
    }

    private void PrintDividerRow()
    {
        PrintRow((char)0x255F, (char)0x2500, (char)0x2563);
    }

    private void PrintCentred(string text)
    {
        if (text.Length * 2 > Width)
        {
            PrintCentred(text[..(Width / 2)]);
            PrintCentred(text[(Width / 2)..]);
            return;
        }

        Console.Write((char)0x2551);
        PrintRepeatedCharacter(' ', Width / 2 - text.Length);
        PrintWide(text);
        PrintRepeatedCharacter(' ', Width / 2 - text.Length + Width % 2);
        Console.Write((char)0x2551);
        Console.Write('\n');
    }

    private void PrintMenuItem(MenuItem item)
    {
        string? firstRow = null;
        if (item.Name.Length > Width - 4)
        {
            firstRow = item.Name.Substring(0, Width - 4);
        }
        Console.Write((char)0x2551);
        Console.Write(' ');
        Console.Write(_menuItems.IndexOf(item) == _selection ? "->" : "  ");
        Console.Write(' ');
        Console.Write(firstRow ?? item.Name);
        PrintRepeatedCharacter(' ', Width - 4 - (firstRow ?? item.Name).Length);
        Console.Write((char)0x2551);
        Console.Write('\n');
        if (firstRow != null)
        {
            PrintContinuedMenuItem(item.Name.Substring(Width - 4));
        }
    }

    private void PrintContinuedMenuItem(string text)
    {
        if (text.Length > Width - 6)
        {
            PrintContinuedMenuItem(text.Substring(0, Width - 6));
            PrintContinuedMenuItem(text.Substring(Width - 6));
            return;
        }

        Console.Write((char)0x2551);
        PrintRepeatedCharacter(' ', 5);
        Console.Write(text);
        PrintRepeatedCharacter(' ', Width - 5 - text.Length);
        Console.Write((char)0x2551);
        Console.Write('\n');
    }

    private static void PrintRepeatedCharacter(char character, int times)
    {
        for (var i = 0; i < times; i++)
        {
            Console.Write(character);
        }
    }

    private static void PrintWide(string text)
    {
        foreach(var character in text)
        {
            Console.Write(character);
            Console.Write(' ');
        }
    }

    private int ReadInput()
    {
        while (true)
        {
            while (!Console.KeyAvailable) ;
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Enter:
                    return _menuItems[_selection].MenuAction();
                case ConsoleKey.DownArrow:
                    MoveDown();
                    return 0;
                case ConsoleKey.UpArrow:
                    MoveUp();
                    return 0;
            }
        }
    }

    private void MoveDown()
    {
        _selection = Math.Min(++_selection, _menuItems.Count - 1);
    }

    private void MoveUp()
    {
        _selection = Math.Max(--_selection, 0);
    }
}