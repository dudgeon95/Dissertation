using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SimonSaysController : MonoBehaviour {
    public GameObject[] objects;
    public GameObject center;
    GameObject activeObject;
    GameObject LookedAt;
    public GameObject StartMenu;
    public GameObject BackMenu;

    public HeadMovement Movement;

    bool gameActive = false;

    bool lookAtActiveObject;

    bool WaitforUser;

    bool BackAtCenter = false;

    public int maxRounds;
    int progressCount = 0;

    string direction;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
        center.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(gameActive)
        {
            LookedAt = Movement.DirectionRaycast();
            
            PlayGame();
        }
        
        
    }

    void PlayGame()
    {
        if(progressCount < maxRounds)
        { 
            if (!WaitforUser)
            {
                NextDirection();
                print("Next Direction");
            }
            else if (LookedAt == activeObject)
            {
                print("Look Detected");
                print(LookedAt.name);
                activeObject.SetActive(false);
                ResetToCenter();
            }
            else
            {
                //print("-");
            }
        }
        else
        {
            BackMenu.SetActive(true);
            print("Game Done");
        }
    }

    private void ResetToCenter()
    {
        
        print(LookedAt.name);
        center.SetActive(true);
        activeObject = center;
        if (LookedAt.name == "Center")
        {
            BackAtCenter = true;
            print("Back at center");
            center.SetActive(false);
            progressCount++;
            WaitforUser = false;
        }
    }

    public void toggleActive(bool active)
    {
        gameActive = active;
        StartMenu.SetActive(false);
    }

    void NextDirection()
    {
        
        direction = RandomDirection();
        
        for (int i = 0; i < objects.Length; i++)
        {
            if(objects[i].name == direction)
            {
                activeObject = objects[i];
                activeObject.SetActive(true);
            }
        }
        WaitforUser = true;
    }

    string RandomDirection()
    {
        System.Random rnd = new System.Random();
        int randomInt = rnd.Next(1, 5); //1 - 4 
        print(randomInt);
        //North - 1, South - 2, East - 3, West - 4
        switch (randomInt)
        {
            case 1:
                return "North";
            case 2:
                return "South";
            case 3:
                return "East";
            case 4:
                return "West";
            default:
                return "NotSet";
        }
    }
}
