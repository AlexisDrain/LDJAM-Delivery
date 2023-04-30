using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
* Author: Alexis Clay Drain
*/
public class GameManager : MonoBehaviour
{
    public static GameObject gameManagerObj;
    public static GameObject journal;
    public static GameObject useTutorial;
    public static Text useTutorialText;
    public static GameObject exitJournalTutorial;
    public static PlayerController playerController;
    public static GameObject dialogueTextObj;
    public static Text dialogueText;

    public List<GameObject> babyInventory = new List<GameObject>();
    public List<string> currentDialogue;

    private int currentDialogueMessage = 0;

    void Awake() {
        gameManagerObj = gameObject;
        useTutorial = GameObject.Find("Canvas/Use");
        useTutorialText = useTutorial.transform.Find("Use/Desc").GetComponent<Text>();
        exitJournalTutorial = GameObject.Find("Canvas/ExitJournal");
        journal = GameObject.Find("Canvas/Journal");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        dialogueTextObj = GameObject.Find("Canvas/Dialogue");
        dialogueText = GameObject.Find("Canvas/Dialogue").GetComponent<Text>();
    }
    private void Start() {
        useTutorial.SetActive(false);
        exitJournalTutorial.SetActive(false);
        journal.SetActive(false);

        dialogueTextObj.SetActive(false);
        dialogueText.text = "";
    }
    public void CreateDialogue(List<string> newDialogue) {
        currentDialogueMessage = 0;
        playerController.inDialogue = true;
        currentDialogue = newDialogue;

        dialogueTextObj.SetActive(true);
        GameManager.dialogueText.text = currentDialogue[0];
    }
    public void AdvanceDialogue() {
        currentDialogueMessage += 1;

        if(currentDialogueMessage >= currentDialogue.Count) {
            GameManager.dialogueText.text = "";
            dialogueTextObj.SetActive(false);
            playerController.inDialogue = false;
        } else {
            GameManager.dialogueText.text = currentDialogue[currentDialogueMessage];
        }
    }
}
