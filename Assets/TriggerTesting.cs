using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeting : MonoBehaviour {

    bool isCollided = false;
    private Vector3 startPosition;
    bool up = true;
    // Use this for initialization
    void Start()
    {
        //maxSpeed = 3;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if(isCollided)
        {
            var currentposition = transform.position;
            if (up == true)
            {
                currentposition.y += 0.01f;
                transform.position = currentposition;
                if (transform.position.y >= 2.0f)
                {
                    up = false;
                }
            }
            if (up == false)
            {
                currentposition.y -= 0.01f;
                transform.position = currentposition;
                if (transform.position.y <= 0.5f)
                {
                    up = true;
                }
            }
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        isCollided = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isCollided = false;
    }
}
