using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
/*
* Author: Alexis Clay Drain
*/
public class GameManager : MonoBehaviour
{
    public static GameObject gameManagerObj;
    public static FreeLookCam freeLookCam;
    
    public static GameObject babyExchangeMenu;
    public static GameObject babyExchangeGrid;
    public static GameObject babyGrid;
    //public static GameObject journal;
    public static GameObject useTutorial;
    public static Text useTutorialText;
    public static GameObject takeBabyTutorial;
    public static GameObject exitJournalTutorial;
    public static PlayerController playerController;
    public static GameObject dialogueTextObj;
    public static Text dialogueText;

    public List<BabyStats> babyInventory = new List<BabyStats>();
    public BabyStats babyBlank;
    public BabyStats babyColette;
    public BabyStats babyNicky;
    public BabyStats babyBoyone;
    public GameObject babyUI;

    public GameObject currentDialogueParents;
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
        freeLookCam = GameObject.Find("FreeLookCameraRig").GetComponent<FreeLookCam>();
        babyExchangeMenu = GameObject.Find("Canvas/BabyExchangeMenu");
        babyExchangeGrid = GameObject.Find("Canvas/BabyExchangeMenu/BabyExchangeGrid");
        babyGrid = GameObject.Find("Canvas/BabyGrid");
        useTutorial = GameObject.Find("Canvas/Use");
        useTutorialText = useTutorial.transform.Find("Use/Desc").GetComponent<Text>();
        takeBabyTutorial = GameObject.Find("Canvas/TakeBackBaby");
        exitJournalTutorial = GameObject.Find("Canvas/ExitJournal");
        //journal = GameObject.Find("Canvas/Journal");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        dialogueTextObj = GameObject.Find("Canvas/Dialogue");
        dialogueText = GameObject.Find("Canvas/Dialogue").GetComponent<Text>();
    }
    private void Start() {
        useTutorial.SetActive(false);
        takeBabyTutorial.SetActive(false);
        exitJournalTutorial.SetActive(false);
        babyExchangeMenu.SetActive(false);
        //journal.SetActive(false);

        dialogueTextObj.SetActive(false);
        dialogueText.text = "";
    }
    
    public void Update() {
        if (babyExchangeMenu.activeSelf == true && Input.GetKeyDown(KeyCode.Escape)) {
            CloseBabyExchange();
        }
        //if(Input.GetKeyDown(KeyCode.F2)) {
        //    Screen.fullScreen = !Screen.fullScreen;
       // }
    }
    
    public void ShowBabyExchange() {
        freeLookCam.enabled = false;
        babyExchangeMenu.SetActive(true);
    }
    public void CloseBabyExchange() {
        freeLookCam.enabled = true;
        babyExchangeMenu.SetActive(false);
    }

    public void CreateDialogue(List<string> newDialogue, GameObject dialogueParents) {
        currentDialogueParents = dialogueParents;
        currentDialogueMessage = 0;
        playerController.inDialogue = true;
        currentDialogue = newDialogue;

        dialogueTextObj.SetActive(true);
        GameManager.dialogueText.text = currentDialogue[0];
        switch (GameManager.dialogueText.text) {
                case "Colette":
                AdvanceDialogue();
                break;
                case "Nicky":
                AdvanceDialogue();
                break;
                case "Boy One":
                AdvanceDialogue();
                break;
                case "Any":
                AdvanceDialogue();
                break;
                case "Blank":
                AdvanceDialogue();
                break;
            }
        }
    public void AdvanceDialogue() {
        currentDialogueMessage += 1;

        if(currentDialogueMessage >= currentDialogue.Count) {
            GameManager.dialogueText.text = "";
            dialogueTextObj.SetActive(false);
            playerController.inDialogue = false;
        } else {
            // ALEXIS - Execute dialogue functions here
            switch (currentDialogue[currentDialogueMessage]) {
                case "-pu colette":
                // code block
                var baby = Instantiate(babyColette);
                babyInventory.Add(baby);
                baby.PutInUI();
                AdvanceDialogue();
                break;
                case "-pu nicky":
                var baby2 = Instantiate(babyNicky);
                babyInventory.Add(baby2);
                baby2.PutInUI();
                AdvanceDialogue();
                break;
                case "-pu boyone":
                var baby3 = Instantiate(babyBoyone);
                babyInventory.Add(baby3);
                baby3.PutInUI();
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
                case "-baby-exchange":
                ShowBabyExchange();
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
