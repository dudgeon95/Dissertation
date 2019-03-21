using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class SimonSaysController : MonoBehaviour {
    public GameObject[] objects;
    public GameObject center;
    GameObject activeObject;
    GameObject LookedAt;
    public GameObject StartMenu;
    public GameObject BackMenu;
    public Text timer;

    public AudioClip MusicClip;
    public AudioSource MusicSource;

    public HeadMovement Movement;

    bool gameActive = false;

    bool lookAtActiveObject;

    bool WaitforUser;

    bool BackAtCenter = false;

    public int maxRounds;
    int progressCount = 0;

    string direction;

    float simonTimer;
    float centerToBlockTimer;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
        center.SetActive(false);
        MusicSource.clip = MusicClip;
        print("Active");
        Debug.Log("Active");
	}
	
	// Update is called once per frame
	void Update () {
        if(gameActive)
        {
            LookedAt = Movement.DirectionRaycast();
            simonTimer += Time.deltaTime;
            PlayGame();
        }
        timer.text = simonTimer.ToString("F");


    }

    void PlayGame()
    {
        if(progressCount < maxRounds)
        { 
            if (!WaitforUser)
            {
                NextDirection();
                print("Next Direction");
                centerToBlockTimer = 0.0f;
                if(progressCount > 1)
                {
                    moveBlocks(0.25f);
                }
            }
            else if (LookedAt == activeObject)
            {
                print("Look Detected");
                MusicSource.Play();
                print(LookedAt.name);
                activeObject.SetActive(false);
                ResetToCenter();
            }
            else
            {
                if(activeObject.name != "Center" && progressCount > 1)
                {
                    centerToBlockTimer += Time.deltaTime;
                    if(centerToBlockTimer > 5.0f)
                    {
                        moveBlocks(-0.0005f);
                    }
                    print(centerToBlockTimer);
                }
               
                //print("-");
            }
        }
        else
        {
            BackMenu.SetActive(true);
            gameActive = false;
            
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
    //+ve position will move the blocks out, -ve position will move the blocks in
    void moveBlocks(float position)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            switch(objects[i].name)
            {
                case "North":
                    if(objects[i].transform.position.y <= 6  && objects[i].transform.position.y >= 4) //Max position - Min position
                    objects[i].transform.position += new Vector3(0, position, 0);
                    break;
                case "South":
                    if(objects[i].transform.position.y >= 0 && objects[i].transform.position.y <= 2) //Doesnt go below the floor
                    {
                        objects[i].transform.position += new Vector3(0, -position, 0);
                    }
                    break;
                case "West":
                    if (objects[i].transform.position.x >= -3 && objects[i].transform.position.x <= -1) //Max position - Min
                    {
                        objects[i].transform.position += new Vector3(-position, 0, 0);
                    }
                    break;
                case "East":
                    if (objects[i].transform.position.x <= 3 && objects[i].transform.position.x >= 1) //Max position - Min
                    {
                        objects[i].transform.position += new Vector3(position, 0, 0);
                    }
                    break;
            }
        }
    }
}
