using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Process_Input();
    }

    private void Process_Input() {

        if (Input.GetKey(KeyCode.Space)) {
            print("Space Pressed!");
        }

        if (Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D)) {
            print("A Pressed!");
        }

        if (Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A)) {
            print("D Pressed!");
        }

    }
}
