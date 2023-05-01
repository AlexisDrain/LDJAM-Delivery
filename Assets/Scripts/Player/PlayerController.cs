using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

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
    public AudioClip flapAudioClip;

    [Header("Checks")]
    public bool inDialogue = false;
    public float inDialogueCountdown = 0f;

    private Rigidbody myRigidbody;
    public SpriteRenderer mySpriteRenderBabies;
    public SpriteRenderer mySpriteRenderNoBabies;
    private Transform particles;
    void Awake () {
        myRigidbody= GetComponent<Rigidbody>();
        mySpriteRenderBabies = transform.Find("GraphicBabies").GetComponent<SpriteRenderer>();
        mySpriteRenderNoBabies = transform.Find("GraphicNoBabies").GetComponent<SpriteRenderer>();
        
        particles = transform.Find("Particles");

        mySpriteRenderBabies.enabled = false;
        mySpriteRenderNoBabies.enabled = true;
    }
    private void Update() {
        if(inDialogueCountdown > 0) {
            inDialogueCountdown -= 0.1f;
            return;
        }

        /*
        float direction = Vector3.Dot(transform.forward, myRigidbody.velocity);
        if(direction < -0.1f) {
            mySpriteRender.flipX = false;
            particles.rotation = Quaternion.Euler(0,180f,0);
        } else {
            mySpriteRender.flipX = true;
            particles.rotation = Quaternion.Euler(0, 0f, 0);
        }
        */
        if (Input.GetButtonDown("SwimUp")) {
            //Get Forward face
            //Vector3 desiredDirection = Vector3.Normalize(new Vector3(transform.forward.x, transform.position.y, transform.forward.z));
            Vector3 dir = Camera.main.transform.forward;

            myRigidbody.AddForce(swimUpForce * dir, ForceMode.Impulse);
            myRigidbody.AddForce(swimUpForce * Vector3.up * 1.5f, ForceMode.Impulse);
            particles.GetComponent<ParticleSystem>().Play();
            GameManager.SpawnLoudAudio(flapAudioClip);
            mySpriteRenderNoBabies.GetComponent<Animator>().SetTrigger("FlapNoBabies");
            mySpriteRenderBabies.GetComponent<Animator>().SetTrigger("FlapBabies");
        }

        if (inDialogue) {
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

        //if (Input.GetButton("SwimUp")) {
        //    myRigidbody.AddForce(swimUpForce * Vector3.up, ForceMode.Force);
        //} else if (Input.GetButton("SwimDown")) {
        //    myRigidbody.AddForce(swimDownForce * Vector3.down, ForceMode.Force);
        //}

        float strafe = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");
        if (strafe != 0f) {
            myRigidbody.AddForce(-strafe * horizontalForce * transform.right, ForceMode.Force);

            if (strafe < -0.1f) {
                mySpriteRenderBabies.flipX = true;
                mySpriteRenderNoBabies.flipX = true;
                //particles.rotation = Quaternion.Euler(0, 0f, 0);
            } else {
                mySpriteRenderBabies.flipX = false;
                mySpriteRenderNoBabies.flipX = false;
                //particles.rotation = Quaternion.Euler(0, 180f, 0);
            }
        }
        if (forward != 0f) {
            myRigidbody.AddForce(-forward * horizontalForce * transform.forward, ForceMode.Force);
        }
        if (myRigidbody.velocity.y > verticalMaxSpeed) {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, verticalMaxSpeed, myRigidbody.velocity.z);
        }
        /*
        if (myRigidbody.velocity.y < -verticalMaxSpeed) {
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, -verticalMaxSpeed, myRigidbody.velocity.z);
        }
        if (myRigidbody.velocity.x > horizontalMaxSpeed) {
            myRigidbody.velocity = new Vector3(horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
        }
        if (myRigidbody.velocity.x < -horizontalMaxSpeed) {
            myRigidbody.velocity = new Vector3(-horizontalMaxSpeed, myRigidbody.velocity.y, myRigidbody.velocity.z);
        */
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x * horizonDrag, myRigidbody.velocity.y * swimUpDownDrag, myRigidbody.velocity.z * horizonDrag);
    }
}
