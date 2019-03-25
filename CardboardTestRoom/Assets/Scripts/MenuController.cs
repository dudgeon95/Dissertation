using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    
    //Get the menu object
    public GameObject BackMenu;
    //Get the save data button;
    public GameObject DataButton;
    public GameObject confirmText;
    void Update()
    {
        if(StaticClass.puzzleComplete && StaticClass.cubeComplete)
        {
            DataButton.SetActive(true);
        }
    }

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
        StaticClass.PuzzleIncrement = 0.0f;
        //Reset the progression
        StaticClass.puzzleProgression = 1.0f;
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

    
    //Write the information to a file
    public void WriteToFile()
    {
        string d = DateTime.Now.ToString();
        //print(d);
        string FILE_PATH = Application.persistentDataPath +"/DataFile.txt";

        StreamWriter sr = File.CreateText(FILE_PATH);
        sr.WriteLine(d);
        sr.WriteLine("Up angle: " + StaticClass.Up + " Degrees");
        sr.WriteLine("Down angle: " + StaticClass.Down + " Degrees");
        sr.WriteLine("Right angle: " + StaticClass.Right + " Degrees");
        sr.WriteLine("Left angle:" + StaticClass.Left + " Degrees");
        sr.WriteLine("Puzzle 1 time: " + StaticClass.Puzzle1 + " Seconds");
        sr.WriteLine("Puzzle 2 time: " + StaticClass.Puzzle2 + " Seconds");
        sr.WriteLine("Puzzle 3 time: " + StaticClass.Puzzle3 + " Seconds");
        sr.WriteLine("Cube time: " + StaticClass.Cube + " Seconds");
        sr.Close();

        string contents = StaticClass.Up.ToString("F") + "," + StaticClass.Down.ToString("F") + "," + StaticClass.Left.ToString("F") + "," + StaticClass.Right.ToString("F")
            + "," + StaticClass.Puzzle1.ToString("F") + "," + StaticClass.Puzzle2.ToString("F") + "," + StaticClass.Puzzle3.ToString("F") + "," + StaticClass.Cube.ToString("F");
        string FILE_PATH2 = Application.persistentDataPath + "/DataFile.csv";
        File.AppendAllText(FILE_PATH2, Environment.NewLine + contents);
        confirmText.SetActive(true);
        print("Wrote to file");
    }
}
