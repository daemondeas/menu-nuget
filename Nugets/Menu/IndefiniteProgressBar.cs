namespace Menu;

public class IndefiniteProgressBar
{
    private readonly string _message;
    private readonly (int CursorLeft, int CursorTop) _pointer;
    private readonly string[] _dots;
    private int _i;

    public IndefiniteProgressBar(string message)
    {
        _message = message;
        _pointer = (Console.CursorLeft, Console.CursorTop);
        _dots = new[] { ".    ", "..   ", "...  ", ".... ", "....." };
    }

    public bool Running { get; set; } = true;

    private void PrintProgress()
    {
        Console.CursorLeft = _pointer.CursorLeft;
        Console.CursorTop = _pointer.CursorTop;
        Console.Write($"{_message}{_dots[_i++]}");

        _i %= _dots.Length;
    }

    public Thread ProgressLoop()
    {
        return new Thread(PrintProgressLoop);
    }

    private void PrintProgressLoop()
    {
        while (Running)
        {
            PrintProgress();
            Thread.Sleep(100);
        }
    }
}