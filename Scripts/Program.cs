/* https://twitter.com/HoodStrats || https://github.com/Hoodstrats */

using System;
using System.Threading;
public static class Program
{
    static Timer _timer = null;
    static int _howMuchCandy;
    static int _mousePosX;
    static int _mousePosY;
    static void Main()
    {
        Console.SetWindowSize(105, 30);
        WriteToConsole.Print("Hoodstrat's Auto Corn Gobbler.\n", ConsoleColor.Yellow, true);
        WriteToConsole.Print("Your wrist will thank you.\n", ConsoleColor.Yellow, true);
        SetAmountOfCandy();
    }

    private static void GetUserInput()
    {
        Console.WriteLine("Press S to start auto clicker.\n");
        WriteToConsole.Print("Press Q at anytime to Quit.\n", ConsoleColor.Red, false);

        var pressed = Console.ReadKey(true);
        if (pressed.Key == ConsoleKey.S)
        {
            StartAutoClicker();
            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            if (keyPress.KeyChar == 'Q' || keyPress.KeyChar == 'q')
            {
                Environment.Exit(0);
            }
        }
        else if (pressed.Key == ConsoleKey.Q)
        {
            Environment.Exit(0);
        }
    }

    private static void SetAmountOfCandy()
    {
        Console.WriteLine("So, how much candy do you want to use?\n");
        _howMuchCandy = 0;
        int amount = 0;
        string value = Console.ReadLine();
        if (int.TryParse(value, out amount) && amount > 0 && amount / 3 > 0)
        {
            _howMuchCandy = amount / 3;
            Console.WriteLine($"\nAlright looks like you want to use {amount} Candy. That'll be good for {_howMuchCandy} Gobbler uses.\n");
            GetUserInput();
        }
        else
        {
            WriteToConsole.Print("\nNumber values divisible by 3 would be preferable.\n", ConsoleColor.Red, false);
            SetAmountOfCandy();
        }
    }

    private static void StartAutoClicker()
    {
        Console.WriteLine("Starting auto clicker.\n");
        Thread.Sleep(5000);
        _timer = new Timer(AutoClickLoop, null, 0, 6000);
    }

    private static void AutoClickLoop(object sender)
    {
        MousePoint position = new MousePoint();
        if (_mousePosX == 0)
        {
            position = MouseOperations.GetCursorPosition();
        }
        else
        {
            position = new MousePoint(_mousePosX, _mousePosY);
        }
        var rngSleep = new Random();
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown, position);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp, position);
        Thread.Sleep(rngSleep.Next(110, 150));
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown, position);
        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp, position);
        _howMuchCandy--;
        Console.WriteLine($"{_howMuchCandy}.\n");
        if (_howMuchCandy <= 0)
        {
            _timer.Dispose();
            Console.WriteLine("Job done.\n");
            WriteToConsole.Print("Press any key to Exit.\n", ConsoleColor.Red, false);
            Console.ReadLine();
        }
    }
}