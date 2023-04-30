using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class UsableEntity : MonoBehaviour
{
    public int progressRequired = 0;
    private bool enableObject = false;

    public string entityUseText = "Take Baby: \"Nicky\""; // Talk to: Doctor // Swap babies.
    //public GameObject pickupEntity;
    public List<string> dialogue;
    public Color dialogueColor;

    private bool playerInZone;
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.CompareTag("Player")) {
            playerInZone = true;
            GameManager.useTutorial.SetActive(true);
            GameManager.useTutorialText.text = entityUseText;
        }
    }
    private void OnTriggerExit(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            playerInZone = false;
            GameManager.useTutorial.SetActive(false);
        }
    }
    public void Update() {

        // enable based on progress
        if (progressRequired == GameManager.gameProgressCheckpoint) {
            enableObject = true;
        } else {
            enableObject = false;
        }
        if (GetComponent<BoxCollider>().enabled == false && enableObject == true) {
            // enable object
            GetComponent<BoxCollider>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (GetComponent<BoxCollider>().enabled == true && enableObject == false) {
            GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        // check input
        if (progressRequired == GameManager.gameProgressCheckpoint && playerInZone == true && GameManager.playerController.inDialogue == false && Input.GetButtonDown("Use")) {
            GameManager.playerController.inDialogueCountdown = 1f;
            GameManager.dialogueText.color = dialogueColor;
            GameManager.gameManagerObj.GetComponent<GameManager>().CreateDialogue(dialogue);
        }
    }
}
