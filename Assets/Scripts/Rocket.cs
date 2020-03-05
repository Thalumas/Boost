using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustPower = 120f;
    [SerializeField] float rotationSpeed = 120f;
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
        HandleThrust();
        HandleRotation();
    }

    private void HandleThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(thrustVector);
            if (!engineSound.isPlaying) {
                engineSound.Play();
            }
        } else {
            engineSound.Stop();
        }
    }

    private void HandleRotation() {

        if (Input.GetKey(KeyCode.LeftArrow) & !Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) & !Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
        }
   }

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Safe":
                print("Safe collision");
                break;
            default:
                print("You  Dead!!!!!");
                break;
        }
    }

}
