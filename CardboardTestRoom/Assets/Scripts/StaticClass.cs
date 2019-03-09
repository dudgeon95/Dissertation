using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass
{
    private static float previousTimer;
    private static float puzzleIncrement;

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
}
