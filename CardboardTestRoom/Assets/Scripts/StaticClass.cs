using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    private static float previousTimer;
    private static float puzzleIncrement;
    private static float UpAngle, DownAngle, RightAngle, LeftAngle;
    private static float puzzle1Timer, puzzle2Timer, puzzle3Timer, cubeTimer;
    private static float pProgression;
    private static bool pComplete, cComplete, RComplete;

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
    public static float Puzzle1
    {
        get
        {
            return puzzle1Timer;
        }
        set
        {
            puzzle1Timer = value;
        }
    }
    public static float Puzzle2
    {
        get
        {
            return puzzle2Timer;
        }
        set
        {
            puzzle2Timer = value;
        }
    }
    public static float Puzzle3
    {
        get
        {
            return puzzle3Timer;
        }
        set
        {
            puzzle3Timer = value;
        }
    }
    public static float Cube
    {
        get
        {
            return cubeTimer;
        }
        set
        {
            cubeTimer = value;
        }
    }
    public static float puzzleProgression
    {
        get
        {
            return pProgression;
        }
        set
        {
            pProgression = value;
        }
    }
    public static bool puzzleComplete
    {
        get
        {
            return pComplete;
        }
        set
        {
            pComplete = value;
        }
    }

    public static bool cubeComplete
    {
        get
        {
            return cComplete;
        }
        set
        {
            cComplete = value;
        }
    }

    public static bool ROMComplete
    {
        get
        {
            return RComplete;
        }
        set
        {
            RComplete = value;
        }
    }

}
