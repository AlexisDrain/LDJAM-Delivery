using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class PlayerController : MonoBehaviour {
    [Header("Move stats")]
    public float swimUpForce = 10f;
    public float swimDownForce = 10f;
    public float horizontalForce = 10f;

    public float verticalMaxSpeed = 10f;
    public float horizontalMaxSpeed = 10f;
    public float swimUpDownDrag = 1f;
    public float horizonDrag = 0.9f;

    [Header("Checks")]
    public bool inDialogue = false;
    public float inDialogueCountdown = 0f;

    private Rigidbody myRigidbody;
    void Awake () {
        myRigidbody= GetComponent<Rigidbody>();
    }
    private void Update() {
        if(inDialogueCountdown > 0) {
            inDialogueCountdown -= 0.1f;
            return;
        }
        if(inDialogue) {
            if(Input.GetButtonDown("Use")) {
                GameManager.gameManagerObj.GetComponent<GameManager>().AdvanceDialogue();
            }
        }
    }
    private void FixedUpdate() {

        transform.LookAt(Camera.main.transform);

        if(inDialogue || GameManager.babyExchangeMenu.activeSelf == true) {
            myRigidbody.velocity = new Vector3(0f,0f,0f);
            return;
        }

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
