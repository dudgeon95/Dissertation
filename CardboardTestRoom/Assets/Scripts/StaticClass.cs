using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    private static float previousTimer;
    private static float puzzleIncrement;
    private static float UpAngle, DownAngle, RightAngle, LeftAngle;

    public static float Timer
    {
        get
        {
            return previousTimer;
        }
        set
        {
            previousTimer = value;
        }
    }
    public static float PuzzleIncrement
    {
        get
        {
            return puzzleIncrement;
        }
        set
        {
            puzzleIncrement = value;
        }
    }

    public static float Up
    {
        get
        {
            return UpAngle;
        }
        set
        {
            UpAngle = value;
        }
    }

    public static float Down
    {
        get
        {
            return DownAngle;
        }
        set
        {
            DownAngle = value;
        }
    }

    public static float Left
    {
        get
        {
            return LeftAngle;
        }
        set
        {
            LeftAngle = value;
        }
    }

    public static float Right
    {
        get
        {
            return RightAngle;
        }
        set
        {
            RightAngle = value;
        }
    }

}
