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

    //The postions based of the angles from  the Rom
    float maxLeft, maxRight, maxUp, maxDown;
    //angles
    float upAngle, downAngle, rightAngle, leftAngle;
	// Use this for initialization
	void Start () {
        //Read in the angles
        upAngle = 50.0f /*StaticClass.Up*/;
        downAngle = 40.0f /*StaticClass.Down*/;
        rightAngle = 60.0f /* StaticClass.Right*/;
        leftAngle = 60.0f /* StaticClass.Left*/;
		for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
            SetMaxPosition(objects[i]);
           
        }
        center.SetActive(false);
        MusicSource.clip = MusicClip;
        print("Active");
        Debug.Log("Active");
        print("Left: " + maxLeft);
        print("Right: " + maxRight);
        print("Up: " + maxUp);
        print("Down: " + maxDown);
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
            StaticClass.Cube = simonTimer;
            StaticClass.cubeComplete = true;
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
                    if(objects[i].transform.position.y <= maxUp  && objects[i].transform.position.y >= 3.5f) //Max position - Min position (Start pos)
                    objects[i].transform.position += new Vector3(0, position, 0);
                    break;
                case "South":
                    if(objects[i].transform.position.y >= maxDown && objects[i].transform.position.y <= 2.5f) //Max position - Min position (Start pos)
                    {
                        objects[i].transform.position += new Vector3(0, -position, 0);
                    }
                    break;
                case "West":
                    if (objects[i].transform.position.x >= maxLeft && objects[i].transform.position.x <= -0.5f) //Max position - Min position (Start pos)
                    {
                        objects[i].transform.position += new Vector3(-position, 0, 0);
                    }
                    break;
                case "East":
                    if (objects[i].transform.position.x <= maxRight && objects[i].transform.position.x >= 0.5f) //Max position - Min position (Start pos)
                    {
                        objects[i].transform.position += new Vector3(position, 0, 0);
                    }
                    break;
            }
        }
    }

    float CalculatePositionFromAngle(float angle, GameObject cube)
    {
        //Convert angle to radians
        angle = angle * Mathf.Deg2Rad;
        return (cube.transform.position.z) * Mathf.Tan(angle);
    }
    void SetMaxPosition(GameObject cube)
    {
        switch (cube.name)
        {
            case "North":
                maxUp = cube.transform.position.y + CalculatePositionFromAngle(upAngle, cube);
                break;
            case "South":
                maxDown = cube.transform.position.y - CalculatePositionFromAngle(downAngle, cube);
                break;
            case "East":
                maxRight = cube.transform.position.x + CalculatePositionFromAngle(rightAngle, cube);
                break;
            case "West":
                maxLeft = cube.transform.position.x - CalculatePositionFromAngle(leftAngle, cube);
                break;

        }
    }
}
