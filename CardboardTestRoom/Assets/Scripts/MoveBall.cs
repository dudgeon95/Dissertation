using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
namespace ZenvaVR
{
    [RequireComponent(typeof(VRInteractiveItem))]
    public class MoveBall : MonoBehaviour
    {
        //VR interactive item component
        VRInteractiveItem vrInteractive;
        bool click;

        // Use this for initialization
        void Start()
        {

        }
        private void Awake()
        {
            vrInteractive = GetComponent<VRInteractiveItem>();

            click = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (click)
            {
                gameObject.transform.position = new Vector3(GetRandomCoordinate(), UnityEngine.Random.Range(1.0f, 2), GetRandomCoordinate());
            }
        }

        private void OnEnable()
        {
            vrInteractive.OnClick += HandleClick;
        }

        private void HandleClick()
        {
            if (click)
            {
                click = false;
            }
            else if (!click) {
                click = true;
            }
            print("teleport");
        }

       

        private float GetRandomCoordinate()
        {
            var coordinate = UnityEngine.Random.Range(-5, 5);
            while (coordinate > -1.5 && coordinate < 1.5)
            {
                coordinate = UnityEngine.Random.Range(-5, 5);
            }
            return coordinate;
        }
    }
}
