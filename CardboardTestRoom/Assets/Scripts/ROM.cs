using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using TMPro;
using System.IO;

public class ROM : MonoBehaviour {
    private Vector3 angle;
    bool waitForPlayer;
    bool active;
    int progession; //States how far along the ROM is

    public AudioClip MusicClip;
    public AudioSource MusicSource;

    public TextMeshProUGUI InstructionText;
    public GameObject StartButton;
    public GameObject StartMenu;
    public GameObject FinishedMenu;


    // Use this for initialization
    void Start () {
        MusicSource.clip = MusicClip;
        progession = 0;
        waitForPlayer = false;
        active = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        //if(Input.GetButtonDown("Fire1"))
        //{
        //    OnClick();
        //}
        if (active)
        {
            StartROM();
        }
       
    }

    public void OnClick()
    {
        angle = Camera.main.transform.eulerAngles;
        Debug.Log(angle.ToString());
        MusicSource.Play();
    }

    public void SetActive(bool active)
    {
        this.active = active;
        StartButton.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartRoom");
    }

    void StartROM()
    {
        if (progession == 0)
        {
            LookLeft();
            waitForPlayer = true;
        }
        else if (progession == 1)
        {
            LookRight();
            waitForPlayer = true;
        }
        else if (progession == 2)
        {
            LookUp();
            waitForPlayer = true;
        }
        else if (progession == 3)
        {
            LookDown();
            waitForPlayer = true;
        }
        else
        {
            active = false;
            EndROM();
        }


    }

    void LookLeft()
    {
        
        InstructionText.text = "Look Left";
        if (waitForPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                OnClick();
                waitForPlayer = false;
                print("Left angle: " + (360 - angle.y));
                StaticClass.Left = (360 - angle.y);
                progession++;
            }
        }
    }

    void LookRight()
    {
        
        InstructionText.text = "Look Right";
        if (waitForPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                OnClick();
                waitForPlayer = false;
                print("Right angle: " + (angle.y));
                StaticClass.Right = angle.y;
                progession++;
            }
        }
    }

    void LookUp()
    {

        InstructionText.text = "Look Up";
        if (waitForPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                OnClick();
                waitForPlayer = false;
                print("Up angle: " + (360 - angle.x));
                StaticClass.Up = (360 - angle.x);
                progession++;
            }
        }
    }

    void LookDown()
    {

        InstructionText.text = "Look Down";
        if (waitForPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                OnClick();
                waitForPlayer = false;
                print("Down angle: " + (angle.x));
                StaticClass.Down = angle.x;
                progession++;
            }
        }
    }
    void EndROM()
    {
        
        StartMenu.SetActive(false);
        FinishedMenu.SetActive(true);
    }

    
}
