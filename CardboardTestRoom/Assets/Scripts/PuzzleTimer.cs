using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleTimer : MonoBehaviour {

    public Text timerText;

    private float elapsed;

   

    private bool active = false;

    void Awake()
    {
        
        elapsed = 0.0f;
        print(StaticClass.Timer.ToString("F"));
    }

    // Update is called once per frame
    void Update ()
    {
        if(active)
        {
            elapsed += Time.deltaTime;
        }
        
        timerText.text = elapsed.ToString("F");
	}

    public void ToggleActive(bool toggle)
    {
        active = toggle;
    }

    public float GetElapsed()
    {
        return elapsed;
    }
}
