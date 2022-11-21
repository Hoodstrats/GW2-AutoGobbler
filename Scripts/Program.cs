/* https://twitter.com/HoodStrats || https://github.com/Hoodstrats */

using System;
using System.Threading;
public static class Program
{
    static Timer _timer = null;
    static int _howMuchCandy;
    //static MousePoint[] _mousePoints;
    //these are 32bit integers can prolly make the mouse operations return numerical value
    //then convert back to MouseEventFlag and send to mouse operations
    //static MouseOperations.MouseEventFlags[] _mouseEventFlags;
    //static int[] _mouseEvents;

    static int _mousePosX;
    static int _mousePosY;
    static void Main()
    {
        Console.SetWindowSize(105, 30);
        WriteToConsole.Print("Hoodstrat's Auto Corn Gobbler.\n", ConsoleColor.Yellow, true);
        WriteToConsole.Print("Your wrist will thank you.\n", ConsoleColor.Yellow, true);
        //MouseOperations.SetCursorPosition(30, 50);
        //start prompt method here and depending on choice we record all mousepoints and actions
        //during duration of time (ask for desired duration)
        SetAmountOfCandy();
        //StartTimer();
    }

    private static void GetUserInput()
    {
        Console.WriteLine("Press S to start auto clicker.\n");
        Console.WriteLine("Press R to Record current mouse position.\n");
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
        //else if (pressed.Key == ConsoleKey.R)
        //{
        //    RecordMousePosition();
        //    //StartRecordMouse();
        //}
        else if (pressed.Key == ConsoleKey.Q)
        {
            Environment.Exit(0);
        }
    }

    private static void SetAmountOfCandy()
    {
        Console.WriteLine("So, how much candy do you want to use?\n");
        //zero out our value to avoid any issues
        _howMuchCandy = 0;
        int amount = 0;
        string value = Console.ReadLine();
        if (int.TryParse(value, out amount) && amount > 0 && amount / 3 > 0)
        {
            //essentially how many auto click attempts since it takes 3 for each gobbler use
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

    //get the current mouse position and record it to try and double click there
    private static void RecordMousePosition()
    {
        _mousePosX = MouseOperations.GetCursorPosition().X;
        _mousePosY = MouseOperations.GetCursorPosition().Y;
        Console.WriteLine($"{_mousePosX},{_mousePosY}\n");
        //go back to input
        GetUserInput();
    }

    private static void StartAutoClicker()
    {
        Console.WriteLine("Starting auto clicker.\n");
        //pause for 5 seconds before (avoids timer from instantly firing off a double click)
        Thread.Sleep(5000);
        //randomize the timer value at the end of the AutoClickerLoop
        //in intervals of 6 seconds (can't randomize this here)
        _timer = new Timer(AutoClickLoop, null, 0, 6000);
    }

    //private static void StartRecordMouse()
    //{
    //    Thread.Sleep(5000);
    //    _timer = new Timer(RecordMouseInput, null, 0, 100);
    //}

    //here we're going to add to our arrays probably add a console output with the info too to make sure 
    //private static void RecordMouseInput(object sender)
    //{
    //    _mousePoints.Append(MouseOperations.GetCursorPosition());
    //    //_mouseEvents.Append()
    //}

    private static void AutoClickLoop(object sender)
    {
        //assign current spot if nothing gets recorded
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
        //using https://clickspeedtest.net/double-click-test.php to measure the speed of a normal double click its about 
        //0.11 to 0.15 seconds which translates to 110MS and 150MS
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