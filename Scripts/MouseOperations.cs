using System;
using System.Runtime.InteropServices;

public class MouseOperations
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010
    }

    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static void SetCursorPosition(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public static void SetCursorPosition(MousePoint point)
    {
        SetCursorPos(point.X, point.Y);
    }

    public static MousePoint GetCursorPosition()
    {
        MousePoint currentMousePoint;
        var gotPoint = GetCursorPos(out currentMousePoint);
        if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
        return currentMousePoint;
    }

    /// <summary>
    /// modified version to pass in the value of where to click with X and Y coords
    /// if nothing gets passed in it uses the mouses current location
    /// </summary>
    /// <param name="value"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static int MouseEvent(MouseEventFlags value, MousePoint where)
    {
        MousePoint position = new MousePoint();
        if (position.X == 0)
        {
            position = GetCursorPosition();
        }
        else
        {
            position = where;
        }

        mouse_event
            ((int)value,
             position.X,
             position.Y,
             0,
             0)
            ;

        //convert the incoming enum into its 32bit integer value
        return (int)value;
    }
}