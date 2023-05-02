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

    private static Pool pool_LoudAudioSource;
    public static GameObject babyExchangeMenu;
    public static GameObject babyExchangeGrid;
    public static GameObject babyGrid;
    public static GameObject journal;
    public static JournalEntries journalEntries;
    public static GameObject useTutorial;
    public static Text useTutorialText;
    public static GameObject takeBabyTutorial;
    public static GameObject exitJournalTutorial;
    public static GameObject newJournalTutorial;
    public static GameObject babyEnding;
    public static PlayerController playerController;
    public static GameObject dialogueTextObj;
    public static Text dialogueText;

    public List<BabyStats> babyInventory = new List<BabyStats>();
    [Header("Baby Objects")]
    public BabyStats babyBlank;
    public BabyStats babyColette;
    public BabyStats babyNicky;
    public BabyStats babyBoyone;
    public BabyStats babyRadish;
    public BabyStats babyLush;
    public BabyStats babyJacob;
    public GameObject babyUI;
    [Header("Baby Final Parent")]
    public EndingScore babyColetteScore;
    public EndingScore babyNickyScore;
    public EndingScore babyBoyoneScore;
    public EndingScore babyRadishScore;
    public EndingScore babyLushScore;
    public EndingScore babyJacobScore;

    public GameObject currentDialogueParents;
    public List<string> currentDialogue;
    public AudioClip journalOpenSFX;
    public AudioClip journalCloseSFX;
    public AudioClip creditsMusic;

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

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        babyExchangeMenu = GameObject.Find("Canvas/BabyExchangeMenu");
        babyExchangeGrid = GameObject.Find("Canvas/BabyExchangeMenu/BabyExchangeGrid");
        babyGrid = GameObject.Find("Canvas/BabyGrid");
        useTutorial = GameObject.Find("Canvas/Use");
        useTutorialText = useTutorial.transform.Find("Use/Desc").GetComponent<Text>();
        takeBabyTutorial = GameObject.Find("Canvas/TakeBackBaby");
        exitJournalTutorial = GameObject.Find("Canvas/ExitJournal");
        newJournalTutorial = GameObject.Find("Canvas/NewJournalEntry");
        journal = GameObject.Find("Canvas/Journal");
        journalEntries = journal.transform.Find("JournalText").GetComponent<JournalEntries>();
        babyEnding = GameObject.Find("Canvas/BabyEnding");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        dialogueTextObj = GameObject.Find("Canvas/Dialogue");
        dialogueText = GameObject.Find("Canvas/Dialogue").GetComponent<Text>();
    }
    private void Start() {
        useTutorial.SetActive(false);
        takeBabyTutorial.SetActive(false);
        exitJournalTutorial.SetActive(false);
        // newJournalTutorial.SetActive(false);
        babyExchangeMenu.SetActive(false);
        babyEnding.SetActive(false);
        journalEntries.AddEntry("-I should speak with the Hospital's Doctor to start my job. (<color=#00FF29>Green exclamation</color> Mark).\n", "");
        journal.SetActive(false);

        dialogueTextObj.SetActive(false);
        dialogueText.text = "";

        Time.timeScale = 0f;
        freeLookCam.enabled = false;
    }
    public void StartGame() {
        Time.timeScale = 1f;
        freeLookCam.enabled = true;
    }
    public void FinishGame() {
        transform.Find("Music").GetComponent<AudioSource>().PlayWebGL(creditsMusic);
        babyEnding.SetActive(true);
        freeLookCam.enabled = false;
        gameProgressCheckpoint = 0;
    }
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Update() {
        if (babyExchangeMenu.activeSelf == true && Input.GetKeyDown(KeyCode.Escape)) {
            CloseBabyExchange();
        }
        
        //if(Input.GetKey(KeyCode.F2) && Input.GetKeyDown(KeyCode.F3)) {
        //    babyEnding.SetActive(!babyEnding.activeSelf);
        //}

        if(journal.activeSelf == true) {
            if (Input.GetButtonDown("Journal") || Input.GetButtonDown("ExitJournal")) {
                CloseJournal();
            }
        } else {
            if (Input.GetButtonDown("Journal") && playerController.inDialogue == false) {
                ShowJournal();
                newJournalTutorial.SetActive(false);
            }
        }

        //if(Input.GetKeyDown(KeyCode.F2)) {
        //    Screen.fullScreen = !Screen.fullScreen;
       // }
    }


    public void ShowJournal() {
        Time.timeScale = 0f;
        freeLookCam.enabled = false;
        exitJournalTutorial.SetActive(true);
        journal.SetActive(true);
        SpawnLoudAudio(journalOpenSFX);
    }
    public void CloseJournal() {
        Time.timeScale = 1f;
        freeLookCam.enabled = true;
        exitJournalTutorial.SetActive(false);
        journal.SetActive(false);
        SpawnLoudAudio(journalCloseSFX);
    }
    public void ShowBabyExchange() {
        Time.timeScale = 0f;
        freeLookCam.enabled = false;
        babyExchangeMenu.SetActive(true);
    }
    public void CloseBabyExchange() {
        Time.timeScale = 1f;
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
                case "Lush":
                AdvanceDialogue();
                break;
                case "Jacob":
                AdvanceDialogue();
                break;
                case "Radish":
                AdvanceDialogue();
                break;
            }
        }
    public void AdvanceDialogue() {
        currentDialogueMessage += 1;
        //print("start dialogue");

        if (currentDialogueMessage >= currentDialogue.Count) {
            //print("end dialogue");
            GameManager.dialogueText.text = "";
            dialogueTextObj.SetActive(false);
            playerController.inDialogue = false;
            playerController.inDialogueCountdown = 1f;
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
                case "-pu lush":
                var baby4 = Instantiate(babyLush);
                babyInventory.Add(baby4);
                baby4.PutInUI();
                AdvanceDialogue();
                break;
                case "-pu radish":
                var baby5 = Instantiate(babyRadish);
                babyInventory.Add(baby5);
                baby5.PutInUI();
                AdvanceDialogue();
                break;
                case "-pu jacob":
                var baby6 = Instantiate(babyJacob);
                babyInventory.Add(baby6);
                baby6.PutInUI();
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
                case "-endgame":
                GameManager.gameManagerObj.GetComponent<GameManager>().FinishGame();
                break;
                case "-baby-exchange":
                ShowBabyExchange();
                break;
                case "-journalEntries0":
                journalEntries.AddEntry("-Colette's parents live on Hive & Sundance.\n", "");
                AdvanceDialogue();
                break;
                case "-journalEntries1":
                journalEntries.AddEntry("-Nicky's parents live on the rooftop of a purple building.\n", "");
                AdvanceDialogue();
                break;
                case "-journalEntries2":
                journalEntries.AddEntry("-Boy One's parents are unknown, but nearby.\n", "");
                AdvanceDialogue();
                break;
                case "-journalEntries3":
                journalEntries.AddEntry("-Nicky's parent wants a blue baby. Maybe find a replacement?\n", "NickyBaby");
                AdvanceDialogue();
                break;
                case "-journalEntries4":
                journalEntries.AddEntry("-Lush's parents can be found by the puzzle \"What makes green?\"\n", "right");
                AdvanceDialogue();
                break;
                case "-journalEntries5":
                journalEntries.AddEntry("-Jacob's parents are divorced. I have to pick who gets him.\n", "right");
                AdvanceDialogue();
                break;
                case "-journalEntries6":
                journalEntries.AddEntry("-Radish's parents are... whoever can conceive a non-binary child.\n", "right");
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
    public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, float newVolume = 1f) {

        AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
        audioObject.PlayWebGL(newAudioClip, newVolume);
        return audioObject;
        // audio object will set itself to inactive after done playing.
    }
}
