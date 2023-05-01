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
    public bool hasReturnToHospital1 = false;
    public List<string> allEntries;

    public void Start() {
        //AddEntry("-I should speak with the Hospital's Doctor to start my job. (<color=#00FF29>Green exclamation</color> Mark).\n", "");
    }

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
        if (special == "ReturnToHospital1") {
            if (hasReturnToHospital1 == false) {
                hasReturnToHospital1 = true;
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
