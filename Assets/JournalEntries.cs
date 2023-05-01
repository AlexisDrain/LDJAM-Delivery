using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Author: Alexis Clay Drain
*/
public class JournalEntries : MonoBehaviour
{
    public bool hasNickyBabyReplacement = false;
    public List<string> allEntries;
    
    public void AddEntry(string newEntry, string special) {
        if (special == "NickyBaby") {
            if (hasNickyBabyReplacement == false) {
                hasNickyBabyReplacement = true;
                GameManager.newJournalTutorial.SetActive(true);
            } else {
                GameManager.newJournalTutorial.SetActive(true);
                return;
            }
        }

        GetComponent<Text>().text += newEntry;
        GameManager.newJournalTutorial.SetActive(true);
    }
}
