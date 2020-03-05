using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int thrustPower = 12;
    public int rotationSpeed = 90;
    private Transform initialTransform;
    private Vector3 thrustVector;

    Rigidbody rigidBody;
    AudioSource engineSound;

    // Start is called before the first frame update
    void Start()
    {
        initialTransform = transform;
        rigidBody = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        thrustVector = Vector3.up * thrustPower;
    }

    // Update is called once per frame
    void Update() {
        Process_Input();
    }

    private void Process_Input() {

        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(thrustVector);
            if (!engineSound.isPlaying) { 
                engineSound.Play();
            }
        } else {
            engineSound.Stop();
        }

        if (Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A)) {
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R)) {
            print("Resetting.....");
            transform.localRotation = initialTransform.rotation;
            transform.localPosition = initialTransform.position;
        }
    }
}
