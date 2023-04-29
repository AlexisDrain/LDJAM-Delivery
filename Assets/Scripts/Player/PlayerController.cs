using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class PlayerController : MonoBehaviour
{
    public float swimUpForce = 10f;
    public float swimDownForce = 10f;
    public float horizontalForce = 10f;

    public float verticalMaxSpeed = 10f;
    public float horizontalMaxSpeed = 10f;
    public float swimUpDownDrag = 1f;
    public float horizonDrag = 0.9f;

    private Rigidbody myRigidbody;
    void Awake () {
        myRigidbody= GetComponent<Rigidbody>();
    }
    private void Update() {
        
    }
    private void FixedUpdate() {

        transform.LookAt(Camera.main.transform);

        if (Input.GetButton("SwimUp")) {
            myRigidbody.AddForce(swimUpForce * Vector3.up, ForceMode.Force);
        } else if (Input.GetButton("SwimDown")) {
            myRigidbody.AddForce(swimDownForce * Vector3.down, ForceMode.Force);
        }
        float strafe = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");
        if (strafe != 0f) {
            myRigidbody.AddForce(-strafe * horizontalForce * transform.right, ForceMode.Force);
        }
        if (forward != 0f) {
            myRigidbody.AddForce(-forward * horizontalForce * transform.forward, ForceMode.Force);
        }

        if (myRigidbody.velocity.y > verticalMaxSpeed) {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, verticalMaxSpeed, myRigidbody.velocity.z);
        }
        if (myRigidbody.velocity.y < -verticalMaxSpeed) {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, -verticalMaxSpeed, myRigidbody.velocity.z);
        }
        if (myRigidbody.velocity.x > horizontalMaxSpeed) {
            myRigidbody.velocity = new Vector3(horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
        }
        if (myRigidbody.velocity.x < -horizontalMaxSpeed) {
            myRigidbody.velocity = new Vector3(-horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
        }
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x * horizonDrag, myRigidbody.velocity.y * swimUpDownDrag, myRigidbody.velocity.z * horizonDrag);
    }
}
