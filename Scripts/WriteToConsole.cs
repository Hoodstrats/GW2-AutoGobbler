/* https://twitter.com/HoodStrats || https://github.com/Hoodstrats */

using System;

/// <summary>
/// if the messages need to be customized a bit more
/// </summary>
static class WriteToConsole
{
    public static void Print(string line, ConsoleColor color, bool centered = false)
    {
        Console.ForegroundColor = color;

        if (centered)
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);

        Console.WriteLine(line);
        Console.ResetColor();
    }
}
