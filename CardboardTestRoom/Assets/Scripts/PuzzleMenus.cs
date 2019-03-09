using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleMenus : MonoBehaviour
{

    //Get the back menu object
    public GameObject BackMenu;
    //Get the start menu object
    public GameObject StartMenu;

    //pieces
    public PieceController[] pieces;

    //Timer
     public PuzzleTimer timer;

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i<pieces.Length; i++)
        {
            pieces[i].BlockPiece(true);
        }
    }

  
    //Cause the start menu to disappear
    public void UnloadStartMenu()
    {
        StartMenu.SetActive(false);
    }

    //Cause the back menu to appear
    public void LoadBackMenu()
    {
       
        BackMenu.SetActive(true);

    }
    //Method to load the main menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartRoom");
        //Reset thhe puzzle timer
        StaticClass.Timer = 0.0f;
    }

    //Unblock the pieces
    public void UnblockPieces()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].BlockPiece(false);
        }
    }

    //Start timer
    public void StartTimer()
    {
        timer.ToggleActive(true);
    }

    //Load puzzle room 2
    public void LoadPuzzle2()
    {
        SceneManager.LoadScene("PuzzleRoom2");
        //Set the difficulty
        if (StaticClass.Timer > 10.0f)
        {
            StaticClass.PuzzleIncrement += 0.5f;
            print("Incremented to: " + StaticClass.PuzzleIncrement.ToString());
        }
    }

    //Load puzzle room 3
    public void LoadPuzzle3()
    {
        SceneManager.LoadScene("PuzzleRoom3");
        //Set the difficulty
        if (StaticClass.Timer > 10.0f)
        {
            StaticClass.PuzzleIncrement += 0.5f;
            print("Incremented to: " + StaticClass.PuzzleIncrement.ToString());
        }
    }
}
