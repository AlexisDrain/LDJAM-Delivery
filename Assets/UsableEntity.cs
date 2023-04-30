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
    public List<string> takeBackBabyDialogue;

    private bool playerInZone;
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.CompareTag("Player")) {
            playerInZone = true;
            GameManager.useTutorial.SetActive(true);
            GameManager.useTutorialText.text = entityUseText;

            if (currentBaby != null && currentBaby.babyName != "Blank") {
                GameManager.takeBabyTutorial.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            playerInZone = false;
            GameManager.useTutorial.SetActive(false);
            GameManager.takeBabyTutorial.SetActive(false);
        }
    }
    public void ParentTakeBaby() {
        currentBabyInWorld.SetActive(true);
        currentBabyInWorld.GetComponent<SpriteRenderer>().sprite = currentBaby.babySprite;
        GameManager.gameManagerObj.GetComponent<GameManager>().babyInventory.RemoveAll(x=>x.babyName==currentBaby.babyName);
        Destroy(GameManager.babyExchangeGrid.transform.Find(currentBaby.babyName).gameObject);
        Destroy(GameManager.babyGrid.transform.Find(currentBaby.babyName).gameObject);
        CreateDialogue();
        OnTriggerEnter(GameManager.playerController.GetComponent<Collider>()); // to see dialogue options again
    }
    public void PlayerTakeBaby() {

        var baby = Instantiate(currentBaby);
        baby.PutInUI();
        GameManager.gameManagerObj.GetComponent<GameManager>().babyInventory.Add(baby);
        currentBabyInWorld.SetActive(false);
        currentBaby = GameManager.gameManagerObj.GetComponent<GameManager>().babyBlank;

        // dialogue
        GameManager.gameManagerObj.GetComponent<GameManager>().currentDialogueParents = gameObject;
        GameManager.playerController.inDialogueCountdown = 1f;
        GameManager.dialogueText.color = dialogueColor;
        GameManager.gameManagerObj.GetComponent<GameManager>().CreateDialogue(takeBackBabyDialogue, gameObject);
        OnTriggerEnter(GameManager.playerController.GetComponent<Collider>()); // to see dialogue options again

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
        if (GameManager.gameProgressCheckpoint  >= progressRequired && playerInZone == true && GameManager.playerController.inDialogue == false
            && GameManager.babyExchangeMenu.activeSelf == false) {
            if(Input.GetButtonDown("Use")) {
                CreateDialogue();
            } else if(Input.GetButtonDown("TakeBaby") && currentBaby != null && currentBaby.babyName != "Blank") {
                PlayerTakeBaby();
            }
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
