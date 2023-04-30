using JetBrains.Annotations;
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
    public GameObject currentBabyInWorld;
    public BabyStats currentBaby;
    //public GameObject pickupEntity;
    public List<string> dialogue;
    public Color dialogueColor;

    public List<string> giveBaby1;
    public List<string> giveBaby2;
    public List<string> giveBaby3;

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
    public void TakeBaby() {
        currentBabyInWorld.SetActive(true);
        currentBabyInWorld.GetComponent<SpriteRenderer>().sprite = currentBaby.babySprite;
        CreateDialogue();
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
        if (progressRequired == GameManager.gameProgressCheckpoint && playerInZone == true && GameManager.playerController.inDialogue == false
            && GameManager.babyExchangeMenu.activeSelf == false && Input.GetButtonDown("Use")) {

            CreateDialogue();
        }
    }
    public void CreateDialogue() {
        GameManager.gameManagerObj.GetComponent<GameManager>().currentDialogueParents = gameObject;
        GameManager.playerController.inDialogueCountdown = 1f;
        GameManager.dialogueText.color = dialogueColor;

        if (currentBaby == null || currentBaby.babyName == "Blank") {
            GameManager.gameManagerObj.GetComponent<GameManager>().CreateDialogue(dialogue, gameObject);
        } else if (currentBaby.babyName == giveBaby1[0]) {
            GameManager.gameManagerObj.GetComponent<GameManager>().CreateDialogue(giveBaby1, gameObject);
        } else if (currentBaby.babyName == giveBaby2[0]) {
            GameManager.gameManagerObj.GetComponent<GameManager>().CreateDialogue(giveBaby2, gameObject);
        } else if (currentBaby.babyName != null) {
            GameManager.gameManagerObj.GetComponent<GameManager>().CreateDialogue(giveBaby3, gameObject);
        }
    }
}
