using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class BabyStats : MonoBehaviour
{

    public string babyName;
    public string babyGender;
    public Sprite babySprite;
    public GameObject babyUI;
    public void PutInUI() {
        GameObject baby = GameObject.Instantiate(babyUI, GameManager.babyGrid.transform);
    }
}
