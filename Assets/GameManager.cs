using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
* Author: Alexis Clay Drain
*/
public class GameManager : MonoBehaviour
{
    public static GameObject gameManagerObj;
    public static GameObject babyGrid;
    public static GameObject journal;
    public static GameObject useTutorial;
    public static Text useTutorialText;
    public static GameObject exitJournalTutorial;
    public static PlayerController playerController;
    public static GameObject dialogueTextObj;
    public static Text dialogueText;

    public List<GameObject> babyInventory = new List<GameObject>();
    public GameObject babyColette;

    public List<string> currentDialogue;

    public static int gameProgressCheckpoint = 0;
    /* gameProgressCheckpoint = 0 - have not talked to the doctor - get new babies
     * 1 - talked to the doctor but still have babies
     * 2- talked to the doctor after delivering babies - get new babies
     * 3- talked to the doctor but still have babies
     * 4- finish game
     */

    private int currentDialogueMessage = 0;

    void Awake() {
        gameManagerObj = gameObject;
        babyGrid = GameObject.Find("Canvas/BabyGrid");
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
            switch (currentDialogue[currentDialogueMessage]) {
                case "-pu colette":
                // code block
                babyInventory.Add(babyColette);
                babyColette.GetComponent<BabyStats>().PutInUI();
                AdvanceDialogue();
                break;
                case "-pu nicky":
                babyInventory.Add(babyColette);
                AdvanceDialogue();
                break;
                case "-pu boyone":
                babyInventory.Add(babyColette);
                AdvanceDialogue();
                break;
                case "-set 1":
                GameManager.gameProgressCheckpoint = 1;
                AdvanceDialogue();
                break;
                case "-set 2":
                GameManager.gameProgressCheckpoint = 2;
                AdvanceDialogue();
                break;
                case "-set 3":
                GameManager.gameProgressCheckpoint = 3;
                AdvanceDialogue();
                break;
                case "-set 4":
                GameManager.gameProgressCheckpoint = 4;
                AdvanceDialogue();
                break;
                case "-restart game":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                AdvanceDialogue();
                break;
                // say dialogue
                default:
                GameManager.dialogueText.text = currentDialogue[currentDialogueMessage];
                break;
            }
            
        }
    }
}
