using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class UsableEntity : MonoBehaviour
{
    public string entityUseText = "Take Baby: \"Nicky\""; // Talk to: Doctor // Swap babies.
    public GameObject pickupEntity;
    public List<string> dialogue;

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

        if (playerInZone == true && GameManager.playerController.inDialogue == false && Input.GetButtonDown("Use")) {
            GameManager.playerController.inDialogueCountdown = 1f;
            GameManager.gameManagerObj.GetComponent<GameManager>().CreateDialogue(dialogue);
        }
    }
}
