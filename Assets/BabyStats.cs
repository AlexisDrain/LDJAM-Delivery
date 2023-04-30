using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Author: Alexis Clay Drain
*/
[CreateAssetMenu(fileName = "Baby", menuName = "ScriptableObjects/Baby", order = 1)]
public class BabyStats : ScriptableObject
{

    public string babyName;
    public Color babyColor;
    public Sprite babySprite;
    // public string babyGender;
    // public GameObject babyUI;
    public void PutInUI() {
        var newBabyUI = Instantiate(GameManager.gameManagerObj.GetComponent<GameManager>().babyUI, GameManager.babyGrid.transform);
        newBabyUI.GetComponent<Button>().enabled = false;
        newBabyUI.name = babyName;
        BabyStats newBabyStats = ScriptableObject.CreateInstance<BabyStats>();
        newBabyUI.GetComponent<BabyUIScript>().myBabyStats = newBabyStats;
        newBabyUI.GetComponent<BabyUIScript>().myBabyStats.babyName = babyName;
        newBabyUI.GetComponent<BabyUIScript>().myBabyStats.babyColor = babyColor;
        newBabyUI.GetComponent<BabyUIScript>().myBabyStats.babySprite = babySprite;
        newBabyUI.transform.Find("BabyIcon").GetComponent<Image>().sprite = babySprite;
        newBabyUI.transform.Find("BabyName").GetComponent<Text>().text = babyName;
        newBabyUI.transform.Find("BabyName").GetComponent<Text>().color = babyColor;
        var newBabyUIExchange = Instantiate(GameManager.gameManagerObj.GetComponent<GameManager>().babyUI, GameManager.babyExchangeGrid.transform);
        newBabyUIExchange.GetComponent<Button>().enabled = true;
        newBabyUIExchange.name = babyName;
        newBabyUIExchange.GetComponent<BabyUIScript>().myBabyStats = newBabyStats;
        newBabyUIExchange.GetComponent<BabyUIScript>().myBabyStats.babyName = babyName;
        newBabyUIExchange.GetComponent<BabyUIScript>().myBabyStats.babyColor = babyColor;
        newBabyUIExchange.GetComponent<BabyUIScript>().myBabyStats.babySprite = babySprite;
        newBabyUIExchange.transform.Find("BabyIcon").GetComponent<Image>().sprite = babySprite;
        newBabyUIExchange.transform.Find("BabyName").GetComponent<Text>().text = babyName;
        newBabyUIExchange.transform.Find("BabyName").GetComponent<Text>().color = babyColor;
        /*
        GameObject baby = GameObject.Instantiate(babyUI, GameManager.babyGrid.transform);
        baby.GetComponent<Button>().enabled = false;
        baby.name = babyName;
        GameObject babyExchange = GameObject.Instantiate(babyUI, GameManager.babyExchangeGrid.transform);
        babyExchange.GetComponent<Button>().enabled = true;
        babyExchange.name = babyName;
        */
    }
    public void GiveBaby() {
        /*
        CopyBabyStats();
        Destroy(GameManager.babyGrid.transform.Find(babyName).gameObject);
        Destroy(GameManager.babyExchangeGrid.transform.Find(babyName).gameObject);

        GameManager.gameManagerObj.GetComponent<GameManager>().CloseBabyExchange();
        GameManager.gameManagerObj.GetComponent<GameManager>().currentDialogueParents.GetComponent<UsableEntity>().TakeBaby();
        */
    }

    public void CopyBabyStats() {
        BabyStats saveBaby = GameManager.gameManagerObj.GetComponent<GameManager>().currentDialogueParents.GetComponent<UsableEntity>().currentBaby;
        saveBaby.babyName = babyName;
        saveBaby.babyColor = babyColor;
        saveBaby.babySprite = babySprite;
        // saveBaby.babyUI = babyUI;
    }
}
