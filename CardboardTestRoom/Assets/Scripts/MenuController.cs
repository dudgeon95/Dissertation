using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    
    //Get the menu object
    public GameObject BackMenu;

  
    //Cause the back menu to appear
    public void LoadBackMenu()
    {
       
        BackMenu.SetActive(true);
            
     }
    //Method to load the puzzle scene
    public void LoadPuzzle()
    {
       
        SceneManager.LoadScene("PuzzleRoom1");
        //Reset the puzzle difficulty 
        StaticClass.PuzzleIncrement = 0;
    }
    //Method to quit the game
    public void QuitGame()
    {
        print("Quit Game");
        Application.Quit();
    }
    //Method to load the main menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartRoom");
    }

    public void LoadSimonSays()
    {
        SceneManager.LoadScene("SimonSays");
    }
}
