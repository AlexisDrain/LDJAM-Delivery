using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class FinalScore : MonoBehaviour
{
    public List<string> sucessfulNames;
    public string medalName;

    void Start() {
        GetSuccessfulChild();
    }
    public bool GetSuccessfulChild() {

        for (int i = 0; i < sucessfulNames.Count; i++) {
            if (GetComponent<UsableEntity>().currentBaby.babyName == sucessfulNames[i]) {
                return true;
            }
        }
        return false;
    }
}
