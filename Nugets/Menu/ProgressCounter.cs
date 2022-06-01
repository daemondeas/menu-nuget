namespace Menu;

public class ProgressCounter
{
    private readonly string _message;
    private readonly int _goal, _length;
    private readonly (int CursorLeft, int CursorTop) _pointer;
    private readonly string[] _dots;
    private int _i;

    public ProgressCounter(string message, int goal, int current = 0)
    {
        _message = message;
        _goal = goal;
        Current = current;
        _pointer = (Console.CursorLeft, Console.CursorTop);
        _length = goal.ToString().Length;
        _dots = new[] { ".    ", "..   ", "...  ", ".... ", "....." };
    }

    public int Current { get; set; }

    private void PrintProgress()
    {
        Console.CursorLeft = _pointer.CursorLeft;
        Console.CursorTop = _pointer.CursorTop;
        Console.Write($"{_message}: {Current.ToString($"D{_length}")}/{_goal}{_dots[_i++]}");

        _i %= _dots.Length;
    }

    public Thread ProgressLoop()
    {
        return new Thread(PrintProgressLoop);
    }

    private void PrintProgressLoop()
    {
        while(Current < _goal)
        {
            PrintProgress();
            Thread.Sleep(100);
        }
    }
}